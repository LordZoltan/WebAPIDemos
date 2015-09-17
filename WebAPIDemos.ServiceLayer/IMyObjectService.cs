using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Core service interface.
    /// 
    /// All services should:
    /// a) Return Task{T} where T is IServiceResponse
    /// b) Accept one argument that is IServiceRequest
    /// 
    /// In the case of a function that would usually accept multiple arguments, a request type should be created
    /// that rolls up those arguments into one type - and then the generic request interface can be used to
    /// wrap that object as an IServiceRequest.
    /// </summary>
    public interface IMyObjectService
    {
        /// <summary>
        /// Fetches an object with the given ID (wrapped in the IServiceRequest member)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IServiceResponse<MyObject>> GetMyObject(IServiceRequest<int> id);
        /// <summary>
        /// Inserts a new object into the back-end data store with the values provided.
        /// 
        /// The response will contain the new object's ID if the operation is successful.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<IServiceResponse<int>> InsertMyObject(IServiceRequest<MyObject> obj);

        //Task<IQueryable<MyObject>> All();
    }
}
