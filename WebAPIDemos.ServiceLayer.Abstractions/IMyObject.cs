using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.Abstractions
{
    /// <summary>
    /// using a base class for business objects that are exposed through the servicelayer.
    /// </summary>
    public class MyObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
