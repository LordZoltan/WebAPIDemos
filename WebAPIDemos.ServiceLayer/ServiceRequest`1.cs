using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Standard implementation of the generic IServiceRequest{TArg} interface.
    /// 
    /// This is part of the main service layer so that every consumer can construct
    /// requests to any service layer implementation
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    public class ServiceRequest<TArg> : ServiceRequest, IServiceRequest<TArg>
    {
        public TArg Argument
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new request with the given argument object, optionally using the passed source
        /// request as the source.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="source"></param>
        public ServiceRequest(TArg argument, IServiceRequest source = null)
            : base(source)
        {
            Argument = argument;
        }
    }
}
