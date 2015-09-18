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
    }
}
