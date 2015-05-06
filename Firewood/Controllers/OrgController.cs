using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;

using BLL;
using DAL;
using DataLinq;
using Firewood.Models;

namespace Firewood.Controllers
{
    public class OrgController : Controller
    {
        BLL_Org orgBLL = new BLL_Org();

        #region 判断是否登录
        public ActionResult IsLog(string view)
        {
            if (Session["Org"] == null)
            {
                if (Request.Cookies["OrgLog"] != null)//session过期，查看cookie是否存在
                {
                    string[] message = Request.Cookies["OrgLog"].Value.Split('+');
                    if (message.Length == 2)
                    {
                        string orgname = message[0];
                        string password = message[1];
                        org org = orgBLL.Login(orgname, password);
                        if (org != null)
                        {
                            Session["Org"] = org;//写入session
                            if (view.Equals(""))
                            {
                                return Redirect("/Firewood");
                            }
                            else
                            {
                                return View(view);
                            }
                        }
                        else
                        {
                            if (view.Equals(""))
                            {
                                return View();
                            }
                            else
                            {
                                return Redirect("/Firewood/Org/Login");
                            }
                        }
                    }
                    else
                    {
                        if (view.Equals(""))
                        {
                            return View();
                        }
                        else
                        {
                            return Redirect("/Firewood/Org/Login");
                        }
                    }
                }
                else
                {
                    if (view.Equals(""))
                    {
                        return View();
                    }
                    else
                    {
                        return Redirect("/Firewood/Org/Login");
                    }
                }
            }
            else
            {
                if (view.Equals(""))
                {
                    return Redirect("/Firewood");
                }
                else
                {
                    return View(view);
                }
            }
        }
        #endregion 判断是否登录

        #region 显示各种页面
        [HttpGet]
        public ActionResult Index()
        {
            return IsLog("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return IsLog("");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        #endregion 显示各种页面

        #region 社团/组织登录、注册
        /// <summary>
        /// 社团/组织登录
        /// </summary>
        /// <param name="orgname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsLogin(string orgname, string password)
        {
            org org = orgBLL.Login(orgname, password);
            if (org != null)
            {
                Session["Org"] = org;//将user写入session

                HttpCookie cookieUser = new HttpCookie("OrgLog");
                cookieUser.Value = orgname + "+" + Md5.MD5_encrypt(password);
                cookieUser.Expires = System.DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookieUser);

                Response.StatusCode = 200;
                return Json(new { username = orgname });
            }
            else
            {
                Response.StatusCode = 401;
                return Json(new { message = "名称或密码错误！" });
            }
        }

        [HttpPost]
        public ActionResult IsRegister(OrgRegForm orgform, HttpPostedFileBase orgpic)
        {
            string orgname = orgform.orgname.Trim();
            string orgpwd = orgform.password1.Trim();

            if (!ModelState.IsValid || orgpic == null)
            {
                Response.StatusCode = 400;
                return Content("<script type='text/javascript'>alert('字段长度不合理！');history.go(-1);</script>");
            }
            else if(orgBLL.IsNameExist(orgname))
            {
                Response.StatusCode = 402;
                return Content("<script type='text/javascript'>alert('该名称已被注册！');history.go(-1);</script>");
            }
            else if (!orgpwd.Equals(orgform.password2))
            {
                Response.StatusCode = 402;
                return Content("<script type='text/javascript'>alert('密码不一致！');history.go(-1);</script>");
            }
            else
            {
                string path = GetPath(orgname);
                string filename = path + DateTime.Now.Ticks + ".png";
                try
                {
                    using (var stream = orgpic.InputStream)
                    {
                        Image img = Image.FromStream(stream);
                        var bmp = ResizeImg(img);
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(path);
                        bmp.Save(filename, ImageFormat.Png);
                    }
                }
                catch
                {
                    Response.StatusCode = 400;
                    return new EmptyResult();
                }

                org org = new org();
                org.OrgName = orgname;
                org.OrgPwd = orgpwd;
                org.OrgPic = path;
                org.OrgPrincipal = orgform.orgprincipal.Trim();
                org.OrgTel = orgform.orgtel.Trim();
                org.OrgDepartment = orgform.orgdepartment.Trim();
                org.OrgIntroduction = orgform.orgintro.Trim();
                org.OrgContact = orgform.orgcontact.Trim();
                if (orgBLL.Register(org))//上传成功
                {
                    Response.StatusCode = 200;
                    return Content("<script type='text/javascript'>alert('上传成功！');self.location='../UpLoad/Index';</script>");
                }
                else
                {
                    Response.StatusCode = 500;
                    return Content("<script type='text/javascript'>alert('注册异常！');self.location='../UpLoad/Index';</script>");
                }
            }
        }
        #endregion 社团/组织登录、注册

        #region 私有成员
        private Bitmap ResizeImg(Image input)
        {
            if (input.Width > 1600 || input.Height > 1600)
            {
                if (input.Width > input.Height)
                {
                    return new Bitmap(input, 1600, input.Height * 1600 / input.Width);
                }
                else
                {
                    return new Bitmap(input, input.Width * 1600 / input.Height, 1600);
                }
            }
            return new Bitmap(input);
        }
        private string GetPath(string orgname)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Server.MapPath("/FirewoodImages/OrgImg/"));
            sb.Append(orgname);
            sb.Append(@"/");
            return sb.ToString();
        }
        #endregion

    }
}
