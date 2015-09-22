using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.Response
{
    /// <summary>
    /// Provides a ready method for creating success/failed responses from objects by service implementations.
    /// 
    /// Note that the Web API controllers DO NOT USE this for creating their returned responses - because they have 
    /// to be serializable.
    /// </summary>
    public static class ServiceResponseExtensions
    {
        /// <summary>
        /// Constructs a new successful response, using the object on which it is invoked as the result.
        /// 
        /// Note - this extension method uses the built-in ServiceResponse{T} type, which should not be used for serialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="errorMessage">Optional error message to include, if something did go wrong, 
        /// but the operation could still be treated as successful</param>
        /// <returns></returns>
        public static IServiceResponse<T> AsSuccessfulResponse<T>(this T result, string errorMessage = null)
        {
            return new ServiceResponse<T>(result, success: true);
        }

        /// <summary>
        /// Constructs a new failed response, using the object on which it is invoked as the result.
        /// 
        /// Note - this extension method uses the built-in ServiceResponse{T} type, which should not be used for serialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IServiceResponse<T> AsFailedResponse<T>(this T result, string errorMessage = null)
        {
            return new ServiceResponse<T>(result, success: false, errorMessage: errorMessage);
        }

        /// <summary>
        /// Constructs a new strongly typed generic failed response from an exception
        /// 
        /// Note - this extension method uses the built-in ServiceResponse{T} type, which should not be used for serialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IServiceResponse<T> AsFailedResponse<T>(this Exception exception, string errorMessage = null)
        {
            return new ServiceResponse<T>(errorMessage, exception);
        }

        /// <summary>
        /// Constructs a new base ServiceResponse.
        /// 
        /// Note - this extension method uses the built-in ServiceResponse{T} type, which should not be used for serialization.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IServiceResponse AsFailedResponse(this Exception exception, string errorMessage = null)
        {
            return new ServiceResponse(success: false, errorMessage: errorMessage, exception: exception);
        }
    }
}
