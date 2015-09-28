using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Models
{
    public class PagedResultModel<T> : PagedResultModel
    {
        public IEnumerable<T> PageResults { get; set; }
    }
}