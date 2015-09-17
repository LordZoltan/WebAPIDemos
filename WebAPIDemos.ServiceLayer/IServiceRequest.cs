using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// core request interface 
    /// 
    /// note - the default implementation for this interface is found in this library to ensure that all consumers, 
    /// regardless of the back-end implementation being used, can feed requests into the service implementation in the same 
    /// way.
    /// </summary>
    public interface IServiceRequest
    {
        //TODO: think about how to incorporate cancellation into this.
        //Can't add a CancellationToken directly to it - because you can't serialize 
        //one of those over the wire, and you don't actually want to be annotating these types with 
        //serialization attributes - because it's not up to the library to determine how it will be
        //serialized in a downstream client/server pair.

        //TODO: add members for 'calling user' etc.
    }
}
