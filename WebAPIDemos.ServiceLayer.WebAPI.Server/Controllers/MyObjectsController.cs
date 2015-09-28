using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPIDemos.ServiceLayer.WebAPI.Server.Models;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Controllers
{
    //server-side implementation of MyObjectService
    public class MyObjectsController : ApiController
    {
        /// <summary>
        /// ALERT!!!! THIS SHOULD BE INJECTED!!!!
        /// </summary>
        private readonly IMyObjectService _myObjectService = new WebAPIDemos.ServiceLayer.Direct.MyObjectService();

        #region helper methods

        /// <summary>
        /// Selects the correct HTTP status code for a given ApiServiceResponse object based on success/fail (optionally with error).
        /// </summary>
        /// <param name="response"></param>
        /// <param name="successCode">The status code to use for success. Default is 200 (OK)</param>
        /// <param name="unsuccessfulCode">The status code to use for fail.  Default is 404 (NotFound)</param>
        /// <param name="errorCode">The status code to use fail with error.  Default is 500 (InternalServerError)</param>
        /// <returns>An HttpStatusCode value.</returns>
        private static HttpStatusCode GetStatusCodeForResponse(ApiServiceResponse response,
            HttpStatusCode successCode = HttpStatusCode.OK, HttpStatusCode unsuccessfulCode = HttpStatusCode.NotFound, HttpStatusCode errorCode = HttpStatusCode.InternalServerError)
        {
            HttpStatusCode statusCode = successCode;
            if (!response.Success)
            {
                if (response.HasError)
                    statusCode = errorCode;
                else
                    statusCode = unsuccessfulCode;
            }
            return statusCode;
        }

        /// <summary>
        /// Takes the passed authority-relative url and makes it 
        /// </summary>
        /// <param name="uriPartial"></param>
        /// <returns></returns>
        private Uri ToFullUri(string uriPartial)
        {
            //this is not quite production ready, it's just good enough for this site
            return new Uri(new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority)), uriPartial);
        }
        #endregion

        /// <summary>
        /// note here - we don't expect a ServiceRequest object in the signature of the method - because that would mean 
        /// serialising the whole request object from the request - which is not very RESTful.  We don't need that.  Any ambient
        /// request information outside of the primary argument (which will always be a single instance of a type) can be 
        /// passed using request headers.  Some of which will be HTTP-spec, some of which will be custom, depending on the
        /// value.  This means that our service client will need to know how to read in a request, and transmit the additional
        /// information to the server.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> Get([FromUri]PagedQuery query, int? id = null)
        {
            /*
            the choice of return type for action methods is important, depending on what you need to communicate back to your
            clients.  A really good article on is http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/action-results
            which demonstrates the benefits and pitfalls of each approach.

            I'm opting for the reasonably generic HttpResponseMessage here, because it enables me to create responses of any
            status code, and include a body of any type to be serialized.  This enables me to respond with the correct status code.

            An arguably better approach would be to return Task<IHttpActionResult> and then use the various classes in 
            System.Web.Http.Results to create the correct results.  The ones you use the most can be surfaced as methods 
            in a base class API controller.*/

            //-----------------------------------------------

            //if there's an ID, return the object
            //if there's no ID, then use the pagedquery
            if (id != null)
            {
                return GetSingle(id.Value);
            }
            else
            {
                return Query(query);
            }
        }

        /// <summary>
        /// private handler method for getting a single object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> GetSingle(int id)
        {
            /*
            The actual object that we send in the response body is set to our public model response type 
            (via the ToApiServiceResponse extension method overload) so that we can control the 
            data that's sent back.  The inner result type of the response, here, is set to our servicelayer object
            type because we *know* that it's suitable for serialization.  But for the outer response object itself, we
            use a model type defined in this project only, that implements the interface (not that it actually has to,
            but it does mean that you can more easily keep the type up to date as it changes).

            In the situation where the servicelayer object needs to be shaped different for serialization (a real
            possibility), then you can change the inner type and use either AutoMapper, or custom code, to map to a
            different result type for the response.  Which is what we do in the Query method, below. */

            var result = (await _myObjectService.GetMyObject(id.AsServiceRequest())).ToApiServiceResponse();
            return Request.CreateResponse(GetStatusCodeForResponse(result), result);
        }

        private async Task<HttpResponseMessage> Query(PagedQuery query)
        {
            var innerResponse = (await _myObjectService.QueryMyObjects(query.AsServiceRequest()));

            var resultModel = Mapper.Map < PagedResultModel<MyObjectModel>>(innerResponse.Result);

            if (innerResponse.Success && resultModel != null)
            {
                //set next/previous links for restful friendliness
                if (query.Page > 1 && resultModel.PageCount > 1)
                    resultModel.PreviousPage = ToFullUri(Url.Route("DefaultApi", new { Page = query.Page - 1, PageSize = query.PageSize }));
                if (query.Page < resultModel.PageCount) //using 1-based paging
                    resultModel.NextPage = ToFullUri(Url.Route("DefaultApi", new { Page = query.Page + 1, PageSize = query.PageSize }));
            }

            var toReturn = innerResponse.ToApiServiceResponse(resultModel);
            return Request.CreateResponse(GetStatusCodeForResponse(toReturn), toReturn);
        }

        /// <summary>
        /// standard way of doing creation, of course - a post to the resource representing the collection of objects.
        /// </summary>
        /// <param name="newObject"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post(MyObjectModel newObject)
        {
            //Just demonstrating that here you can leverage Web APIs model binding to
            //perform extended validation on the objects that are coming into your API,
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Models.ApiServiceResponse<int>()
                {
                    //clearly the error message would be better as a dictionary of strings.
                    ErrorMessage = string.Join("\n", ModelState.Select(ms => string.Format("{0}: {1}", ms.Key, ms.Value)))
                });
            }

            var result = (await _myObjectService.InsertMyObject(Mapper.Map<MyObject>(newObject).AsServiceRequest())).ToApiServiceResponse();

            //'Created' is the appropriate status code result for a Post (or potentially a Put)
            HttpStatusCode statusCode = GetStatusCodeForResponse(result, successCode: HttpStatusCode.Created, unsuccessfulCode: HttpStatusCode.NoContent);
            HttpResponseMessage toReturn = null;
            if (statusCode == HttpStatusCode.Created)
            {
                //the correct way to respond to a successful POST, in HTTP, is to return 201 CREATED , and the URI for the object 
                //in the 'Location' response header, luckily, we have routing to help with that - although we should abstract away
                //the routing behind a component, or method, so we can more easily refactor if we change the route name or whatever.

                //we will also, however, return a content body, because that's what our contract wants.  It's not required, though:
                //the REST response headers actually give us everything we need to rehydrate our server response on the client.
                //The only slight complication is that if we don't include the object ID as a value, then the client will have to 
                //crack it out from the returned Uri, which is not optimal (because the server should be free to alter its resource
                //hierarchy at any point).

                //you have to be careful with sending response bodies - not all status codes are permitted to include them (even if 
                //a given client appears to process it) - the key issue here being that any request not conforming to the HTTP spec
                //could be mangled by intermediate proxies etc before they get to the endpoint or back to the client.
                toReturn = Request.CreateResponse(statusCode, result);

                //stored local here just for debugging
                var locationUri = ToFullUri(Url.Route("DefaultApi", new { id = result.Result }));
                //set the location field
                toReturn.Headers.Location = locationUri;
            }

            return toReturn ?? Request.CreateResponse(statusCode);
        }
    }
}
