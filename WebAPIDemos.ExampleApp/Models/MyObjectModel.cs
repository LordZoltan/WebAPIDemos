using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPIDemos.ExampleApp.Models
{
    public class MyObjectModel
    {
        [Display(Name="ID")]
        public int Id { get; set; }
        [Display(Name="Object Name")]
        [Required]
        public string Name { get; set; }
    }
}