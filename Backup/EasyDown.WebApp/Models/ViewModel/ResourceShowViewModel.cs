using EasyDown.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyDown.WebApp.Models.ViewModel
{
    public class ResourceShowViewModel
    {
        public int ID { get; set; }
        public string ResourceName { get; set; }

        public string Time { set; get; }

        public string Username { get; set; }

        public EasyDown.Entity.Type Type { get; set; }

        public string Description { get; set; }
        public int UserID { get; set; }

        public ResourceShowViewModel() { }

        public List<Reply> Replies { get; set; }

        public ResourceShowViewModel(Resource resource) 
        {
            this.ID = resource.ID;
            this.ResourceName = resource.ResourceName;
            this.Time = resource.UploadTime.ToString();
            this.Username = resource.User.UserName;
            this.UserID = resource.UserId;
            this.Replies = resource.Replies.OrderByDescending(r => r.Ptime).ToList();
            this.Type = resource.Type;
            this.Description = resource.Description;
        }
    }
}