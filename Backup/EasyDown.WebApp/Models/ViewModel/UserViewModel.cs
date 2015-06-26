using EasyDown.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyDown.WebApp.Models.ViewModel
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string  Username { get; set; }

        public Role Role { get; set; }

        public List<Resource> Resources { get; set; }

        public byte[] Picture { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            this.ID = user.ID;
            this.Username = user.UserName;
            this.Role = user.Role;
            this.Resources = user.Resources.OrderByDescending(u=>u.UploadTime).ToList();
            this.Picture = user.Picture;
        }
    }
}