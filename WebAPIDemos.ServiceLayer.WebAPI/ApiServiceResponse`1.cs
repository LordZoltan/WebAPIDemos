using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.WebAPI
{
    public class ApiServiceResponse<TResult> : ApiServiceResponse, IServiceResponse<TResult>
    {
        public TResult Result
        {
            get;
            set;
        }
    }
}
