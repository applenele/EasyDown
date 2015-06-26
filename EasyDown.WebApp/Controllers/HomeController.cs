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
            List<Resource> words = new List<Resource>();
            List<Resource> videos = new List<Resource>();
            List<Resource> musics = new List<Resource>();
            resources = db.Resources.OrderByDescending(r => r.UploadTime).Take(5).ToList();

            words = db.Resources.Where(r => r.TypeAsInt == 0).OrderByDescending(r => r.UploadTime).ToList();
            videos = db.Resources.Where(r => r.TypeAsInt == 3).OrderByDescending(r => r.UploadTime).ToList();
            musics = db.Resources.Where(r => r.TypeAsInt == 4).OrderByDescending(r => r.UploadTime).ToList();

            ViewBag.resources = resources;
            ViewBag.Words = words;
            ViewBag.Videos = videos;
            ViewBag.Musics = musics;
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
