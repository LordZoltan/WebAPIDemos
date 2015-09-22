using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.Response
{
    /// <summary>
    /// Implementation of the IServiceResponse{TResult} interface
    /// 
    /// This is a suggested implementation for non-Web API services (because it's not explicitly serializable).
    /// 
    /// Note - if TResult is 'string' then, in order to create a success instance, you'll need to 
    /// call var a = new ServiceResponse<string>("s", true); - otherwise the code will automatically
    /// bind to the single-parameter constructor, which defaults to creating a failed response.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class ServiceResponse<TResult> : ServiceResponse, IServiceResponse<TResult>
    {
        public TResult Result { get; private set; }

        /// <summary>
        /// Creates a new failed response with the provided error message
        /// </summary>
        /// <param name="errorMessage"></param>
        public ServiceResponse(string errorMessage = null, Exception exception = null)
            : base(errorMessage: errorMessage, exception: exception)
        {

        }

        /// <summary>
        /// Creates a new successful or failed response with the given result and, optional, error message.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="success"></param>
        /// <param name="errorMessage"></param>
        /// <param name="exception"></param>
        public ServiceResponse(TResult result, bool success = true, string errorMessage = null, Exception exception = null)
            : base(success: success, errorMessage: errorMessage, exception: exception)
        {
            Result = result;
        }
    }
}
