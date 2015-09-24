using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebAPIDemos.ServiceLayer.RequestValidation;

namespace WebAPIDemos.ServiceLayer.WebAPI
{
    /// <summary>
    /// Implementation of the IMyObjectService that wraps around the Web API
    /// </summary>
    public class MyObjectService : IMyObjectService
    {
        private readonly IHttpRequestManager _requestManager;

        public MyObjectService(IHttpRequestManager requestManager)
        {
            if (requestManager == null) throw new ArgumentNullException();
            _requestManager = requestManager;
        }

        public async System.Threading.Tasks.Task<IServiceResponse<MyObject>> GetMyObject(IServiceRequest<int> id)
        {
            // so we use the same validation mechanism that's used over in the Direct service implementation in
            // ../WebAPIDemos.ServiceLayer.Direct/MyObjectService.cs
            id.MustNotBeNull("id");
            //note - I'm using await here to handle the implicit casting of ApiServiceResponse<T> to IServiceResponse<T>
            return await _requestManager.Get<ApiServiceResponse<MyObject>>(string.Format("api/MyObjects/{0}", id.Argument.ToString()), id);
        }



        public async System.Threading.Tasks.Task<IServiceResponse<int>> InsertMyObject(IServiceRequest<MyObject> obj)
        {
            obj.MustNotBeNull("obj");
            return await _requestManager.Send<MyObject, ApiServiceResponse<int>>("api/MyObjects", HttpMethod.Post, obj);
        }


        public async System.Threading.Tasks.Task<IServiceResponse<PagedResult<MyObject>>> QueryMyObjects(IServiceRequest<PagedQuery> query)
        {
            query.MustNotBeNull("query");
            
            //this method of query strings from object content can be refactored.  However, it will have to be a more complex solution
            //that is completely generic and handles nested objects in the same way that the server libraries expect to see.
            //for now, this merely demonstrates the core principle.
            Dictionary<string, string> queryKeyValues = new Dictionary<string, string>();
            //some system that relies on a similar mechanism to Model Binding should be used to convert to strings.  That uses
            //the TypeDescriptor.GetConverter mechanism, so we couuld do the same.
            queryKeyValues["Page"] = Convert.ToString(query.Argument.Page);
            queryKeyValues["PageSize"] = Convert.ToString(query.Argument.PageSize);
            string queryString = null;
            using (var tempContent = new FormUrlEncodedContent(queryKeyValues))
            {
                //why use this?  It handles the url encoding - and doesn't require System.Web (another popular
                //solution uses HttpUtility.ParseQueryString - but client code has no business referencing System.Web).
                queryString = await tempContent.ReadAsStringAsync();
            }

            return await _requestManager.Get<ApiServiceResponse<PagedResult<MyObject>>>(string.Concat("api/MyObjects?", queryString), query);
        }
    }
}
