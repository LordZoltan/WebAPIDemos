using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Generic request interface for an operation that takes single instance of one argument type 
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    public interface IServiceRequest<TArg> : IServiceRequest
    {
        /// <summary>
        /// The argument for the request
        /// </summary>
        TArg Argument { get; }
    }
}
