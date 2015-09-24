using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIDemos.ServiceLayer.WebAPI.Server.Models;

namespace WebAPIDemos.ServiceLayer
{
    public static class ApiServiceResponseExtensions
    {
        public static ApiServiceResponse ToApiServiceResponse(this IServiceResponse response)
        {
            return new ApiServiceResponse(response);
        }

        public static ApiServiceResponse<T> ToApiServiceResponse<T>(this IServiceResponse<T> response)
        {
            return new ApiServiceResponse<T>(response);
        }
    }
}