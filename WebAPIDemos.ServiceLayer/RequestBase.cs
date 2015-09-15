using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// core request base class
    /// 
    /// immutable to prevent tampering and to provide clear construction path.
    /// </summary>
    public class RequestBase
    {
        //TODO: think about how to incorporate cancellation into this.
        //Can't add a CancellationToken directly to it - because you can't serialize 
        //one of those over the wire, and you don't actually want to be annotating these types with 
        //serialization attributes - because it's not up to the library to determine how it will be
        //serialized in a downstream client/server pair.


        public RequestBase()
        {
            
        }

        /// <summary>
        /// Allows a new request to 'inherit' values from another.
        /// </summary>
        /// <param name="source">Required.  An existing request from which to inherit properties.</param>
        public RequestBase(RequestBase source)
        {
            
        }

        //TODO: add members for calling user etc.
    }
}
