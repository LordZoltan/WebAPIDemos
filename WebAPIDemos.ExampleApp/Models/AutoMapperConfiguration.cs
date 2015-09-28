using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ExampleApp.Models
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
            //reversible maps for types that pass out of the API and back in again.
            mappingConfiguration.CreateMap<MyObjectModel, MyObject>().ReverseMap();

            //hoping this will work - as we need to map generic paged results of service layer types to 
            //paged results of our model types.
            mappingConfiguration.CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}