using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyDown.Entity;

namespace EasyDown.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            List<Resource> resources = new List<Resource>();
            resources = db.Resources.OrderByDescending(r => r.UploadTime).Take(5).ToList();
            ViewBag.resources = resources;
            return View();
        }

        public string Test()
        {
            Entity.User user = new User { UserName = "admin", Password = "123456", RoleAsInt = 1 };
            db.Users.Add(user);
            db.SaveChanges();
            return "ok";
        }

    }
}
