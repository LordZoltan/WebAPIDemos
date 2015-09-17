using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// Generic response interface for service operations that return an object.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IServiceResponse<TResult> : IServiceResponse
    {
        /// <summary>
        /// The object result for this response.
        /// </summary>
        TResult Result { get; }
    }
}
