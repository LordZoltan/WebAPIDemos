using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Models
{
    /// <summary>
    /// Automapper configuration for the Models namespace in this project
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// All mapping here should be between any model types and the service layer objects.
        /// </summary>
        /// <param name="mappingConfiguration"></param>
        public static void Configure(AutoMapper.IConfiguration mappingConfiguration)
        {
            mappingConfiguration.CreateMap<MyObjectModel, MyObject>().ReverseMap();
        }
    }
}