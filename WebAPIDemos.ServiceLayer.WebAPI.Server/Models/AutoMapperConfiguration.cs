﻿using System;
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
            //reversible maps for types that pass out of the API and bback in again.
            mappingConfiguration.CreateMap<MyObjectModel, MyObject>().ReverseMap();

            //non-reversible maps for types that are only ever passed out of the API
            mappingConfiguration.CreateMap(typeof(PagedResult<>), typeof(PagedResultModel<>));
            
        }
    }
}