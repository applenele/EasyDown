using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyDown.WebApp.Models.ViewModel
{
    public class ResourceUploadViewModel
    {
        [Required]
        [Display(Name="资源名称")]
        public string ResourceName { get; set; }

        [Display(Name="资源类型")]
        public int ResourceType { get; set; }

        [Required]
        [Display(Name="描述")]
        public string Description { get; set; }

    }
}