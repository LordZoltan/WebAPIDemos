using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server.Models
{
    /// <summary>
    /// Analogue of our MyObject class from the ServiceLayer.Objects project
    /// Allows us to add model validation to be used Web API before feeding it into the business layer
    /// </summary>
    public class MyObjectModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}