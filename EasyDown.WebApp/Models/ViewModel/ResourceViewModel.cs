using EasyDown.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyDown.WebApp.Models.ViewModel
{
    public class ResourceViewModel
    {
        public int ID { get; set; }
        public string ResourceName { get; set; }

        public string Time { set; get; }

        public string Username { get; set; }

        public int UserID { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }

        public ResourceViewModel() { }

        public ResourceViewModel(Resource resource) 
        {
            this.ID = resource.ID;
            this.ResourceName = resource.ResourceName;
            this.Time = Helper.Time.ToTimeTip(resource.UploadTime).ToString();
            this.Username = resource.User.UserName;
            this.UserID = resource.UserId;
            this.Description = resource.Description;
            this.Type = resource.Type.ToString();
        }
    }
}