using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer
{
    public class PagedResult
    {
        /// <summary>
        /// Total number of results to be paged over.
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// This page number
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Number of pages available of this size
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// The number of results in this page.
        /// </summary>
        public int Count { get; set; }
    }
}
