using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Models
{
    public class PagedResultModel : PagedResult
    {
        public Uri PreviousPage { get; set; }
        public Uri NextPage { get; set; }
        //possibly add first page...
    }
}