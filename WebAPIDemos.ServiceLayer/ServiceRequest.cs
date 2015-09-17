using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Request 
    /// </summary>
    public class ServiceRequest : IServiceRequest
    {
        /// <summary>
        /// This constructor would take any required values - such as current user - that need to be set on all
        /// requests.
        /// </summary>
        public ServiceRequest()
        {

        }

        /// <summary>
        /// This constructor would be used to inherit all properies of the source request 
        /// into this one for when one request chains on to another.
        /// </summary>
        /// <param name="source"></param>
        public ServiceRequest(IServiceRequest source)
        {

        }
    }
}
