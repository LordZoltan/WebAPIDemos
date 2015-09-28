using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ExampleApp.Models
{
    public class HomeIndexViewModel
    {
        public IServiceResponse LastOperationResponse { get; set; }

        public PagedResult<MyObjectModel> Results { get; set; }
    }
}