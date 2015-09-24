using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using WebAPIDemos.ServiceLayer;
using WebAPIDemos.ServiceLayer.RequestValidation;
using WebAPIDemos.ServiceLayer.Response;    //re-using the ready-made response objects and extensions to help implement the service

namespace WebAPIDemos.ExampleApp.Classes
{
    /// <summary>
    /// This class demonstrates how you can decorate any service implementation, wrapping it to provide 
    /// additional functionality that's application-specific.
    /// 
    /// In this case, the implementation will cache MyObject instances that are pulled through the GetMyObject
    /// method in the web application's cache object for a period of time - chaining through to the inner service
    /// in order to retrieve it the first time.
    /// </summary>
    public class MyCachingObjectService : IMyObjectService
    {
        private readonly IMyObjectService _inner;

        public MyCachingObjectService(IMyObjectService inner)
        {
            _inner = inner;
        }

        //note - for testability - a better implementation of this service would accept an IServiceCache abstraction
        //(name is actually irrelevant - it'd be a proprietary type for this solution),
        //and then one implementation of that would operate over the HostingEnvironment.Cache; making it
        //possible to isolate this class in a unit test with a mocked IServiceCache implementation.

        public async System.Threading.Tasks.Task<IServiceResponse<MyObject>> GetMyObject(IServiceRequest<int> id)
        {
            id.MustNotBeNull("id");
            string cacheKey = MyObjectCacheKey(id.Argument);
            //do caching 
            MyObject cached = (MyObject)HostingEnvironment.Cache[cacheKey];
            if (cached != null)
                return cached.AsSuccessfulResponse();

            //try and retrieve the object from the inner service.  The logic here is if we get a successful response, then 
            //we will cache it.  Otherwise we will simply return the response.  So, crucially, failed lookups are never cached in
            //this implementation - which, in practise, might not be desirable.
            var response = await _inner.GetMyObject(id);

            if (response.Success)
                HostingEnvironment.Cache.Insert(cacheKey, cached, null, DateTime.UtcNow.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);

            return response;
        }

        private static string MyObjectCacheKey(int id)
        {
            return string.Format("#MyObject{0}", id);
        }

        public System.Threading.Tasks.Task<IServiceResponse<int>> InsertMyObject(IServiceRequest<MyObject> obj)
        {
            //this doesn't need to be async/await because we're simply chaining the call through to
            //the inner implementation's task method.
            return _inner.InsertMyObject(obj);
        }


        public System.Threading.Tasks.Task<IServiceResponse<PagedResult<MyObject>>> QueryMyObjects(IServiceRequest<PagedQuery> query)
        {
            //no caching here
            //although - what we could do is check whether any of the objects coming back are in the cache and, if they are,
            //then we replace them for when we do individual gets ONLY
            return _inner.QueryMyObjects(query);
        }
    }
}