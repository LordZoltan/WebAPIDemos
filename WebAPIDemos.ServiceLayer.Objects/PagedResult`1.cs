using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    public class PagedResult<T> : PagedResult, IEnumerable<T>
    {
        public IEnumerable<T> PageResults { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            //if you enumerate the object directly, we hide the null (because null enumerables are evil).
            return (PageResults ?? Enumerable.Empty<T>()).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
