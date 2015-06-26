using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyDown.WebApp.Models.ViewModel
{
    public class UserRegisterViewModel
    {
        [Required]
        [Display(Name="用户名")]
        public string Username { get; set; }

        [Required]
        [Display(Name="密码")]
        public string  Password { get; set; }

        [Required]
        [Display(Name="密码重复")]
        [Compare("Password",ErrorMessage="与密码不一致!")]
        public string Password2 { get; set; }
    }
}