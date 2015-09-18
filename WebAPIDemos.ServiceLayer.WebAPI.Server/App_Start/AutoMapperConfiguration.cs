using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAPIDemos.ServiceLayer.WebAPI.Server.App_Start.AutoMapperConfiguration),
    "Start",
    Order = -100,
    RunInDesigner = false)]

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.App_Start
{
    public static class AutoMapperConfiguration
    {
        public static void Start()
        {
            //ensures that the direct service layer, that we are using as our back-end implementation
            //is initialised correctly.

            AutoMapper.Mapper.Initialize(cfg => {
                ServiceLayer.Direct.AutoMapperConfiguration.Configure(cfg);
            });
        }
    }
}