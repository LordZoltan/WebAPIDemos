using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    /// <summary>
    /// using a class for business objects that are exposed through the servicelayer.
    /// 
    /// this is not without its issues, and is the most simplistic solution.  However in
    /// many cases it will work.
    /// </summary>
    public class MyObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
