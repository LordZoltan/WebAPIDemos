using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Reusable extensions to create service requests from standard objects - to ease the process of consuming 
    /// the service layer from application code.
    /// </summary>
    public static class ServiceRequestExtensions
    {
        //there are many more extension methods that could go in here - but for now I'm just putting in the ones used by the code.

        /// <summary>
        /// Creates an IServiceRequest{T} instance from the object on which the method is called, optionally using another request
        /// as the source of other request information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IServiceRequest<T> AsServiceRequest<T>(this T argument, IServiceRequest source = null)
        {
            //IN THE REAL WORLD - if you are pulling the current user from the current thread identity, and
            //passing the user information over in the request, then you'd use this version of the extension
            //method to pull the user out of the thread and attach it to the request automatically.

            return new ServiceRequest<T>(argument, source);
        }
    }
}
