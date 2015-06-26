using EasyDown.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EasyDown.WebApp.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        #region 跳到登陆界面
        /// <summary>
        /// 跳到登陆界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return Redirect("/");
            }
            return View();
        }
        #endregion


        #region 执行登陆功能
        /// <summary>
        /// 执行登陆功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                Entity.User user = new Entity.User();
                model.Password = Helper.Encryt.GetMD5(model.Password);
                user = db.Users.Where(u => u.UserName == model.Username && u.Password == model.Password).SingleOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "用户名或密码错误！");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码输入错误，请重新输入");
            }
            return View(model);
        }
        #endregion


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Entity.User user1 = new Entity.User();
                user1 = db.Users.Where(u => u.UserName == model.Username).SingleOrDefault();
                if (user1 != null)
                {
                    ModelState.AddModelError("", "用户名已有人使用");
                }
                else
                {
                    Entity.User user = new Entity.User { UserName = model.Username, Password = Helper.Encryt.GetMD5(model.Password), Role = EasyDown.Entity.Role.User };
                    db.Users.Add(user);
                    int result = await db.SaveChangesAsync();
                    if (result > 0)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "添加用户失败!");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "用户名或密码输入不正确！");
            }
            return View(model);
        }


        public async Task<ActionResult> Show(int id)
        {
            Entity.User user = new Entity.User();
            user = await db.Users.FindAsync(id);
            ViewBag.user = new UserViewModel(user);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            Entity.User user = await db.Users.FindAsync(id);
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserEditViewModel model, HttpPostedFileBase file)
        {
            Entity.User user1 = await db.Users.FindAsync(model.ID);
            ViewBag.user = user1;

            if (ModelState.IsValid)
            {
                Entity.User user = await db.Users.FindAsync(model.ID);

                user.UserName = model.Username;
                FormsAuthentication.SignOut();

                FormsAuthentication.SetAuthCookie(model.Username, false);
                if (file != null)
                {
                    System.IO.Stream stream = file.InputStream;
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    stream.Close();
                    user.Picture = buffer;
                }
                if (!string.IsNullOrEmpty(model.Password))
                {
                    if (!user.Password.Equals(Helper.Encryt.GetMD5(model.Password)))
                    {
                        ModelState.AddModelError("", "原始密码输入不正确");
                    }
                    else
                    {
                        user.Password = Helper.Encryt.GetMD5(model.PasswordNew);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Show/" + model.ID);
                    }
                }
                else
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Show/" + model.ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "修改的信息输入错误!");
            }
            return View(model);
        }


        /// <summary>
        ///  注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ShowPicture(int id)
        {
            Entity.User user = new Entity.User();
            user = db.Users.Find(id);
            return File(user.Picture, "image/jpg");
        }

    }

}
