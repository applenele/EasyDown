using EasyDown.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyDown.WebApp.Controllers
{
    public class ReplyController : BaseController
    {
        //
        // GET: /Reply/

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(string content,int rid)
        {
            if (CurrentUser == null)
            {
                return Content("nouser");
            }
            Reply reply = new Reply { Content = content, ResourceID = rid, UserID = CurrentUser.ID, Ptime = DateTime.Now };
            db.Replies.Add(reply);
            int result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

    }
}
