using System;
using System.Collections.Generic;
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
    }
}
