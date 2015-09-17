using System;
namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Readonly IServiceResponse base interface
    /// </summary>
    public interface IServiceResponse
    {
        /// <summary>
        /// TODO: Consider using an object like the MVC/Web API modelstate to communicate error messages
        /// 
        /// Even better - consider have two objects - one for application-level errors, and one for user 
        /// errors, so your service layer is responsible for feeding the UI with error messages to the user.
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Simple boolean value indicating result
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Exception, if the operation failed due to an exception occuring.  Note - this doesn't mean that service operations never throw exceptions,
        /// it simply means that downstream operations throwing exceptions can be handled as part of normal service request/response flow.
        /// 
        /// IT IS NOT expected that exceptions occuring behind the REST API will be bubbled back to the client over the wire - exceptions simply do
        /// not serialize well over the wire, and realistically you don't want them to either.
        /// </summary>
        Exception Exception { get;  }
    }
}
