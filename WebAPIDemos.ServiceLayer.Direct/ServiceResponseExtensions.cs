using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.Direct
{
    /// <summary>
    /// This extension class is internal as only code within the direct service 
    /// implementation should be using it to construct responses in this way using the 
    /// response types that we're using in this library.
    /// </summary>
    internal static class ServiceResponseExtensions
    {
        /// <summary>
        /// Constructs a new successful response, using the object on which it is invoked as the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="errorMessage">Optional error message to include, if something did go wrong, 
        /// but the operation could still be treated as successful</param>
        /// <returns></returns>
        internal static IServiceResponse<T> AsSuccessfulResponse<T>(this T result, string errorMessage = null)
        {
            return new ServiceResponse<T>(result, success: true);
        }

        /// <summary>
        /// Constructs a new failed response, using the object on which it is invoked as the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static IServiceResponse<T> AsFailedResponse<T>(this T result, string errorMessage = null)
        {
            return new ServiceResponse<T>(result, success: false, errorMessage: errorMessage);
        }

        /// <summary>
        /// Constructs a new strongly typed generic failed response from an exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static IServiceResponse<T> AsFailedResponse<T>(this Exception exception, string errorMessage = null)
        {
            return new ServiceResponse<T>(errorMessage, exception);
        }

        /// <summary>
        /// Constructs a new base ServiceResponse.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static IServiceResponse AsFailedResponse(this Exception exception, string errorMessage = null)
        {
            return new ServiceResponse(success: false, errorMessage: errorMessage, exception: exception);
        }
    }
}
