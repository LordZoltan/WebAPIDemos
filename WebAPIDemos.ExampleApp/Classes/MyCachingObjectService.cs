using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ExampleApp.Classes
{
    public class MyCachingObjectService : IMyObjectService
    {
        private readonly IMyObjectService _inner;

        public MyCachingObjectService(IMyObjectService inner)
        {
            _inner = inner;
        }


        public System.Threading.Tasks.Task<IServiceResponse<MyObject>> GetMyObject(IServiceRequest<int> id)
        {
            //do caching 

            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<IServiceResponse<int>> InsertMyObject(IServiceRequest<MyObject> obj)
        {
            return _inner.InsertMyObject(obj);
        }
    }
}