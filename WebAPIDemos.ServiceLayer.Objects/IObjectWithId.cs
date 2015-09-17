using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// interface for an object that contains a single discriminating ID property.
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public interface IObjectWithID<TID>
    {
        /// <summary>
        /// The object's ID.
        /// </summary>
        TID ID { get; set; }
    }
}
