using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Models
{

    [DataContract]
    public class ApiServiceResponse<T> : ApiServiceResponse, IServiceResponse<T>
    {
        [DataMember]
        public T Result
        {
            get; set;
        }

        public ApiServiceResponse()
        {
            
        }

        public ApiServiceResponse(IServiceResponse<T> source)
            : base(source)
        {
            Result = source.Result;
        }

        /// <summary>
        /// Allows you to initialise the service response from any other, whilst manually providing the 
        /// Result object, if it needs to be fed in from a different source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="result"></param>
        public ApiServiceResponse(IServiceResponse source, T result)
            : base(source)
        {
            Result = result;
        }
    }
}
