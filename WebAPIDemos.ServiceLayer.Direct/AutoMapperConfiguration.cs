using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer.Direct
{
    /// <summary>
    /// Global configuration for our AutoMapped types used by the Direct service layer implementation
    /// </summary>
    public static class AutoMapperConfiguration
    {
        public static void Configure(AutoMapper.IConfiguration mappingConfiguration)
        {
            mappingConfiguration.CreateMap<MyEntity, MyObject>().ReverseMap();
        }
    }
}
