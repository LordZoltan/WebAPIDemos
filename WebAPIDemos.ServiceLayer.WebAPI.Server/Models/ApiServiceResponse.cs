using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Models
{
    //unlike the service layer response types, we allow much more control to calling code 
    //in creating this, and its generic derived object - because it's more like a DTO
    //whereas, in the service layer code, there's more logic.

    [DataContract]
    public class ApiServiceResponse : IServiceResponse
    {
        [DataMember]
        public string ErrorMessage
        {
            get; set;
        }

        [DataMember]
        public bool Success
        {
            get; set;
        }

        /// <summary>
        /// Exception that occurred - note - this is excluded from the data contract so that it is not serialized over the wire.
        /// </summary>
        public Exception Exception
        {
            get; set;
        }

        public ApiServiceResponse() { }

        public ApiServiceResponse(IServiceResponse source)
        {
            if (source == null) throw new ArgumentNullException("source");

            ErrorMessage = source.ErrorMessage;
            Success = source.Success;
            Exception = source.Exception;
        }

        /// <summary>
        /// A boolean indicating whether the response has/is an error (regardless of success - since an operation can fail, but not be an error)
        /// </summary>
        internal bool HasError
        {
            get
            {
                return Exception != null || ErrorMessage != null;
            }
        }
    }
}
