using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.WebAPI
{
    /// <summary>
    /// Client-side implementation of the base response interface - this allows any code within this library to 
    /// write to it directly via internal set accessors on the properties.
    /// 
    /// Why?  because it simplifies the process of altering deserialized responses with success/fail or http exceptions.
    /// 
    /// Other than that - the class should be simple because it needs to be created by Json.Net by deserialization.
    /// 
    /// The class should probably be internal - like the Direct library's response.
    /// </summary>
    public class ApiServiceResponse : IServiceResponse
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            set;
        }

        /// <summary>
        /// We expose the underlying HttpRequestMessage that was sent to the server
        /// </summary>
        public HttpRequestMessage ServerRequest { get; set; }

        /// <summary>
        /// We also expose the underlying HttpResponseMessage that was received from the server.
        /// </summary>
        public HttpResponseMessage ServerResponse { get; set; }
    }
}
