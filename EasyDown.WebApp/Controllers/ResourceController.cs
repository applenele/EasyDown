using EasyDown.Entity;
using EasyDown.WebApp.Helper;
using EasyDown.WebApp.Models.ViewModel;
using EasyDown.WebApp.SignalR;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyDown.WebApp.Controllers
{
    public class ResourceController : BaseController
    {
        //
        // GET: /Resource/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(new SelectListItem { Text = "Word", Value = "0", Selected = false });
            typeList.Add(new SelectListItem { Text = "Xls", Value = "1", Selected = false });
            typeList.Add(new SelectListItem { Text = "PPT", Value = "2", Selected = false });
            typeList.Add(new SelectListItem { Text = "视屏", Value = "3", Selected = false });
            typeList.Add(new SelectListItem { Text = "音乐", Value = "4", Selected = false });
            typeList.Add(new SelectListItem { Text = "图片", Value = "5", Selected = false });
            typeList.Add(new SelectListItem { Text = "压缩", Value = "6", Selected = false });
            typeList.Add(new SelectListItem { Text = "其他", Value = "7", Selected = false });
            ViewBag.TypeList = typeList;

            return View();
        }

        #region 上传
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult doUpload(ResourceUploadViewModel model, HttpPostedFileBase file,HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Entity.User user = CurrentUser;
                    var fileName = Path.Combine(Request.MapPath("~/Upload"), DateHelper.GetTimeStamp() + Path.GetFileName(file.FileName));
                    try
                    {
                        file.SaveAs(fileName);
                        Resource resource = new Resource { ResourceName = model.ResourceName, Description = model.Description, TypeAsInt = model.ResourceType, UploadTime = DateTime.Now, Path = DateHelper.GetTimeStamp() + Path.GetFileName(file.FileName), UserId = user.ID };

                        if (file1 != null)
                        {
                            System.IO.Stream stream = file1.InputStream;
                            byte[] buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, (int)stream.Length);
                            stream.Close();
                            resource.Icon = buffer;


                            db.Resources.Add(resource);
                            db.SaveChanges();
                            ResourceViewModel vResource = new ResourceViewModel(resource);

                            var context = GlobalHost.ConnectionManager.GetHubContext<ResourceHub>();//得到Signalr context 
                            context.Clients.All.GetNew(vResource);  //将新上传的资源广播到全部客户端
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "上传资源截图！");
                        }
                       
                    }
                    catch
                    {
                        ModelState.AddModelError("", "上传文件出错");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "上传文件出错");
                }
            }
            else
            {
                ModelState.AddModelError("", "上传文件出错");
            }
            return View(model);
        } 
        #endregion


        #region 得到资源
        /// <summary>
        /// 得到资源
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult getResources(int page,string key)
        {
            List<Resource> resources = new List<Resource>();
            resources = db.Resources.OrderByDescending(r => r.UploadTime).Where(r=>r.ResourceName.Contains(key)).Skip(page * 10).Take(10).ToList();
            List<ResourceViewModel> vResources = new List<ResourceViewModel>();
            foreach (var resource in resources)
            {
                vResources.Add(new ResourceViewModel(resource));
            }
            return Json(vResources);
        }
        
        #endregion


        #region 下载
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult Download(int id)
        {
            Resource resource = new Resource();
            resource = db.Resources.Find(id);
            var path = Server.MapPath("~/Upload/" + resource.Path);
            return File(path, "1", Url.Encode(resource.Path));
        } 
        #endregion


        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            Resource resource = new Resource();
            resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
            int result = await db.SaveChangesAsync();
            if (result > 0)
            {
                var path = Server.MapPath("~/Upload/" + resource.Path);
                System.IO.File.Delete(path);
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        } 
        #endregion

        #region 显示资源
        /// <summary>
        /// 显示资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Show(int id)
        {
            Resource resource = new Resource();
            resource = await db.Resources.FindAsync(id);
            ViewBag.resource = new ResourceShowViewModel(resource);
            return View();
        } 
        #endregion

        #region 展示截图
        /// <summary>
        /// 展示截图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult ShowIcon(int id)
        {
            Resource resource = new Resource();
            resource = db.Resources.Find(id);
            return File(resource.Icon, "image/jpg");
        }
        
        #endregion
    }

}
