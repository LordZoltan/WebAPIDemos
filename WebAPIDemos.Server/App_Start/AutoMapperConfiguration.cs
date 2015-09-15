using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAPIDemos.Server.App_Start.AutoMapperConfiguration),
    "Start",
    Order = -100,
    RunInDesigner = false)]

namespace WebAPIDemos.Server.App_Start
{
    public static class AutoMapperConfiguration
    {
        public static void Start()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                ServiceLayer.Direct.AutoMapperConfiguration.Configure(cfg);
            });
        }
    }
}