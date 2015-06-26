﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyDown.WebApp.Models.ViewModel
{
    public class UserEditViewModel
    {
        [Required]
        public int ID { get; set; }

        [Display(Name="用户名")]
        public string Username { get; set; }

        [Display(Name="原始密码")]
        public string Password { get; set; }

        [Display(Name="新密码")]
        public string PasswordNew { set; get; }


        [Display(Name="新密码重复")]
        [Compare("PasswordNew",ErrorMessage="两次输入新密码不一致!")]
        public string  PasswordNew2 { get; set; }
    }
}