using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;
using WebAPIDemos.ServiceLayer.RequestValidation; //need this to validate requests using the extension methods

namespace WebAPIDemos.ServiceLayer.Direct
{
    /// <summary>
    /// Implementation of the IMyObjectService interface.
    /// 
    /// There is a big decision to be made about disposal of connections etc if adopting a layered architecture like this.
    /// 
    /// This service implementation assumes that the code knows how to create its repo instances, and 
    /// therefore enforces the rule that each operation controls the lifetime of the connections to the repo - hence all operations
    /// are contained with using(){} statements.
    /// 
    /// 
    /// 
    /// There are reasons why you WOULDN'T want to do this - especially if trying to feed back 'proper' IQueryable
    /// instances to consuming code (because the connection, therefore, needs to live longer than the method call
    /// that produces it).  One option: don't expose IQueryables.
    /// 
    /// However...
    /// 
    /// One option is to make the whole service IDisposable also, and keep one self-created instance of the repo per instance 
    /// of the service, which is disposed of when the service is disposed.  This is a good alternative, however, it still doesn't quite
    /// solve the issue of IQueryables etc - all you've done is push the lifetime issue back a bit further to your service layer instead
    /// of your repo layer.
    /// 
    /// The last option (and a typical pattern used today) is to use dependency injection - and pass
    /// the repo instance to this service when it is constructed, and not have the service itself as disposable.
    /// On its own, this isn't enough, however, as you still need to decide when to dispose the repo connection.  This is where the 
    /// unit of work pattern comes in, and is often implemented in web applications using IOC containers that use 'lifetime scopes'
    /// that last as long as a web request on the server.  A new request is received, and a scope is created.  When a repo instance is
    /// constructed, it is tracked in the scope - and only one is constructed for that request.  When the request ends, that scope is
    /// disposed - thus disposing any disposable objects that were created when that scope was live.
    /// 
    /// This is not a trivial behaviour to implement on your own - but there are numerous IOC containers out there that are able to do it
    /// natively for Asp.Net as whole, or MVC/Web API individually.
    /// 
    /// My own library (Rezolver, found on github) cannot do it out of the box for Asp.Net &lt= 4, but does work for Asp.Net vNext.
    /// It can easily be extended to supported for earlier Asp.Net versions, however.
    /// </summary>
    public class MyObjectService : IMyObjectService
    {

        //this implementation is 

        public Task<IServiceResponse<MyObject>> GetMyObject(IServiceRequest<int> id)
        {
            id.MustNotBeNull("id");

            return Task.Factory.StartNew(() => {
                var repo = new ExampleRepo();
                return Mapper.Map<MyObject>(repo.MyEntities.Fetch(id.Argument)).AsSuccessfulResponse();
            });
        }

        public Task<IServiceResponse<int>> InsertMyObject(IServiceRequest<MyObject> obj)
        {
            obj.MustNotBeNull("obj");

             return Task.Factory.StartNew(() => {
                 var repo = new ExampleRepo();
                 
                 var toInsert = Mapper.Map<MyEntity>(obj.Argument);
                 try
                 {
                     repo.MyEntities.Insert(toInsert);
                 }
                 catch(Exception ex)
                 {
                     //yes - catching all exceptions is not good - but this is just demonstrating how you might use the exception to
                     //generate a failed response that automatically has the exception on it.

                     //IN SOME CASES, your service layer operations will bubble their exceptions out, of course - it all depends 
                     //on how you want to handle it.
                     return ex.AsFailedResponse<int>();
                 }

                 return toInsert.Id.AsSuccessfulResponse();
             });
        }
    }
}
