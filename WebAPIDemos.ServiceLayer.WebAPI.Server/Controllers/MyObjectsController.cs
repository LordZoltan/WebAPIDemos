using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Controllers
{
    //server-side implementation of MyObjectService
    public class MyObjectsController : ApiController
    {
        /// <summary>
        /// ALERT!!!! THIS SHOULD BE INJECTED!!!!
        /// </summary>
        private readonly IMyObjectService _myObjectService = new WebAPIDemos.ServiceLayer.Direct.MyObjectService();

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
        public async Task<HttpResponseMessage> Get(int id)
        {
            /*
            the choice of return type for action methods is important, depending on what you need to communicate back to your
            clients.  A really good article on is http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/action-results
            which demonstrates the benefits and pitfalls of each approach.

            I'm opting for the reasonably generic HttpResponseMessage here, because it enables me to create responses of any
            status code, and include a body to serialize.  This enables me to respond with the correct status code.

            An arguably better approach would be to return Task<IHttpActionResult> and then use the various classes in 
            System.Web.Http.Results to create the correct results.  The ones you use the most can be surfaced as methods 
            in a base class API controller.

            The actual object that we send in the response body is set to our public model response type so that we can control the 
            data that's sent back.  The inner result type of the response, here, is set to our servicelayer object
            type because we *know* that it's suitable for serialization.  But for the outer response object itself, we
            use a model type defined in this project only, that implements the interface (not that it actually has to,
            but it does mean that you can more easily keep the type up to date as it changes).

            In the situation where the servicelayer object needs to be shaped different for serialization (a real
            possibility), then you can change the inner type and use either AutoMapper, or custom code, to map to a
            different result type for the response.*/

            //-----------------------------------------------

            /*shouldn't always use await for things like this - in this case, we could use ContinueWith() with 
            continuation options (usually one for RanToCompletion - i.e. no exception; and one for the others, 
            i.e. cancelled or failed or whatever).  The primary difference is performance and memory usage, however,
            in practise, async/await for these types of things is a good start.  Moving the code to .ContinueWith 
            is a task that can be undertaken later.  There are good guides out there for this.*/

            //TODO: make this either AutoMapped or simply create an extension method to create our response object
            var result = new Models.ApiServiceResponse<MyObject>(await _myObjectService.GetMyObject(id.AsServiceRequest()));
            HttpStatusCode statusCode = HttpStatusCode.OK;
            if(!result.Success)
            {
                //if there's an exception and/or error message - then it's InternalServerError
                //if there's neither of those, then it's 404
                if (result.HasError)
                    statusCode = HttpStatusCode.InternalServerError;
                else
                    statusCode = HttpStatusCode.NotFound;
            }
            return Request.CreateResponse(statusCode, result);
        }

        private Uri ToFullUri(string uriPartial)
        {
            //this is not production ready, btw, it's just good enough for this site
            return new Uri(new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority)), uriPartial);
        }

        /// <summary>
        /// standard way of doing creation, of course - a post to the resource representing the collection of objects.
        /// </summary>
        /// <param name="newObject"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post(MyObject newObject)
        {
            //Just demonstrating that here you can leverage Web APIs model binding to
            //perform extended validation on the objects that are coming into your API,
            
            //clearly - you could take ModelState and feed it into an unsuccessful response object
            //that you send back to the client along with the 400.
            //note - also - that, if you're intending to use these features of Web API, then
            //you should bind to an API model type that is annotated with DataAnnotations, and
            if(!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Models.ApiServiceResponse<int>());
            }

            var result = new Models.ApiServiceResponse<int>(await _myObjectService.InsertMyObject(newObject.AsServiceRequest()));

            //'Created' is the appropriate status code result for a Post (or potentially a Put)
            HttpStatusCode statusCode = HttpStatusCode.Created;
            HttpResponseMessage toReturn = null;
            //I smell a pattern here, that needs to be refactored....
            if(!result.Success)
            {
                //tricky, this - if not created, but there's no error - then we'll say there's no response because
                //because if the operation is successful, the client is expecting a response body with an ID
                if (result.HasError)
                    statusCode = HttpStatusCode.InternalServerError;
                else
                    statusCode = HttpStatusCode.NoContent; 
            }
            else
            {
                //the correct way to respond to a successful POST, in HTTP, is to return 201 CREATED , and the URI for the object 
                //in the 'Location' response header, luckily, we have routing to help with that ;)

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
