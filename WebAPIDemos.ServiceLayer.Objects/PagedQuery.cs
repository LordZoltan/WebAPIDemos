using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    public class PagedQuery
    {
        public const int DefaultPageSize = 20;

        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedQuery()
        {
            Page = 1;
            PageSize = DefaultPageSize;
        }
    }
}
