using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL;
using DAL;
using DataLinq;
using Firewood.Models;

namespace Firewood.Controllers
{
    public class UserController : Controller
    {
        BLL_User userBLL = new BLL_User();

        #region 判断是否登录
        public ActionResult IsLog(string view)
        {
            if (Session["User"] == null)
            {
                if (Request.Cookies["UserLog"] != null)//session过期，查看cookie是否存在
                {
                    string[] message = Request.Cookies["UserLog"].Value.Split('+');
                    if (message.Length == 3)
                    {
                        string worknum = message[1];
                        string password = message[2];
                        user user = userBLL.Login(worknum, password);
                        if (user != null)
                        {
                            Session["User"] = user;//写入session
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
                                return Redirect("/Firewood/User/Login");
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
                            return Redirect("/Firewood/User/Login");
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
                        return Redirect("/Firewood/User/Login");
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

        /// <summary>
        /// 显示登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return IsLog("");
        }

        /// <summary>
        /// 显示注册页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        /// <summary>
        /// 显示验证学号页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckNum()
        {
            return View("CheckNum");
        }

        /// <summary>
        /// 显示重置密码的页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPwd()
        {
            if (Session["CheckNum"] == null)
            {
                return Redirect("/Firewood/User/CheckNum");
            }
            else
            {
                ViewBag.UserName = userBLL.GetNameByNum(Session["CheckNum"].ToString());
                return View("ResetPwd");
            }
        }

        /// <summary>
        /// 显示个人中心页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Center()
        {
            return IsLog("Center");
        }
        /// <summary>
        /// 显示完善个人信息页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdateInfo()
        {
            user user = (user)Session["User"];

            if (user != null)
            {
                ViewBag.usernum = user.uNum;
                ViewBag.username = user.uName;
                ViewBag.usermail = user.uMail;
                ViewBag.usertel = user.uTel;
                ViewBag.usergrade = user.uGrade;
                ViewBag.usersex = user.uSex;
                ViewBag.truename = user.trueName;
                return View("UpdateInfo");
            }
            else
            {
                return Redirect("/Firewood/User/Login");
            }
        }

        #endregion 显示各种页面

        #region 登录、注册、验证学号、重置密码
        /// <summary>
        /// 学号和上网密码登录
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsLogin(string usernum, string password)
        {
            user user = userBLL.Login(usernum, password);
            if (user != null)
            {
                Session["User"] = user;//将user写入session

                HttpCookie cookieUser = new HttpCookie("UserLog");
                cookieUser.Value = user.uName+"+"+usernum + "+" + Md5.MD5_encrypt(password);
                cookieUser.Expires = System.DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookieUser);

                Response.StatusCode = 200;
                return Json(new { username = user.uName });
            }
            else
            {
                Response.StatusCode = 401;
                return Json(new { message = "学号或密码错误！" });
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsRegister(RegisterForm reg)
        {
            string usernum = reg.usernum;
            string username = reg.username;
            string usermail = reg.usermail;
            string usertel = reg.usertel;

            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { message = "邮箱或手机号填写错误！" });
            }
            else if (userBLL.IsUserNumExist(usernum))
            {
                Response.StatusCode = 402;
                return Json(new { message = "该学号已被注册" });
            }
            else if (!userBLL.CheckNum(usernum, reg.webpwd))
            {
                Response.StatusCode = 402;
                return Json(new { message = "学号或上网密码错误" });
            }
            else if (userBLL.IsUserNameExist(username))
            {
                Response.StatusCode = 402;
                return Json(new { message = "该昵称已被注册" });
            }
            else if (userBLL.IsUserMailExist(usermail))
            {
                Response.StatusCode = 402;
                return Json(new { message = "该邮箱已被注册" });
            }
            else if (userBLL.IsUserTelExist(usertel))
            {
                Response.StatusCode = 402;
                return Json(new { message = "该手机号已被注册" });
            }
            else if (!reg.password1.Equals(reg.password2))
            {
                Response.StatusCode = 402;
                return Json(new { message = "密码不一致" });
            }
            else
            {
                user user = new user();
                user.uNum = usernum;
                user.uName = username;
                user.uPwd = reg.password1;
                user.uMail = usermail;
                user.uTel = usertel;

                if (userBLL.Register(user))
                {
                    Session["User"] = user;

                    HttpCookie cookieUser = new HttpCookie("UserLog");
                    cookieUser.Value = username + "+" + usernum + "+" + Md5.MD5_encrypt(user.uPwd);
                    cookieUser.Expires = System.DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookieUser);

                    Response.StatusCode = 200;
                    return Json(new { message = "注册成功" });
                }
                else
                {
                    Response.StatusCode = 500;
                    return Json(new { message = "注册异常" });
                }
            }
        }

        /// <summary>
        /// 验证学号
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="webpwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsCheckNum(string usernum, string webpwd)
        {
            if (userBLL.IsUserNumExist(usernum))
            {
                if (userBLL.CheckNum(usernum, webpwd))
                {
                    Session["CheckNum"] = usernum;
                    Response.StatusCode = 200;
                    return Json(new { message = "验证成功" });
                }
                else
                {
                    Response.StatusCode = 401;
                    return Json(new { message = "验证失败，学号或上网密码错误！" });
                }
            }
            else
            {
                Response.StatusCode = 402;
                return Json(new { message = "该学号还未注册，请先注册！" });
            }

        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="password1"></param>
        /// <param name="password2"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsResetPwd(string password1, string password2)
        {
            if (password1.Equals(password2))
            {
                if (userBLL.UpdateUserPwd(Session["CheckNum"].ToString(), password1))
                {
                    Response.StatusCode = 200;
                    return Json(new { message = "重置密码成功！" });
                }
                else
                {
                    Response.StatusCode = 500;
                    return Json(new { message = "重置密码异常！" });
                }
            }
            else
            {
                Response.StatusCode = 401;
                return Json(new { message = "密码不一致！" });
            }
        }

        #endregion 登录、注册、验证学号、重置密码

        #region 退出、身份切换
        /// <summary>
        /// 用户安全退出，返回Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Exit()
        {
            if (Session["User"] != null)
            {
                Session.Clear();
            }
            if (Request.Cookies["UserLog"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserLog");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return Redirect("/Firewood");
        }

        /// <summary>
        /// 身份切换
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReLogin()
        {
            if (Session["User"] != null)
            {
                Session.Clear();
            }
            if (Request.Cookies["UserLog"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserLog");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return Redirect("/Firewood/User/Login");
        }
        #endregion 退出、身份切换

        #region 完善个人信息

        [HttpPost]
        public ActionResult IsUpdateInfo(UpdateInfoForm update)
        {
            string usernum = update.usernum;
            string usermail = update.usermail;
            string usertel = update.usertel;

            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { message = "邮箱或手机号填写错误！" });
            }
            if (userBLL.IsUserMailExist(usermail) && !userBLL.SelectMailByNum(usernum).Equals(usermail))
            {
                Response.StatusCode = 402;
                return Json(new { message = "该邮箱已被注册！" });
            }
            else if (userBLL.IsUserTelExist(usertel) && !userBLL.SelectTelByNum(usernum).Equals(usertel))
            {
                Response.StatusCode = 402;
                return Json(new { message = "该手机号已被注册！" });
            }
            else
            {
                user user = new user();
                user.uNum = update.usernum;
                user.uMail = usermail;
                user.uTel = usertel;
                user.uGrade = update.usergrade;
                user.uSex = Int32.Parse(update.usersex);
                user.trueName = update.truename;

                if (userBLL.UpdateUserInfo(user))
                {
                    Response.StatusCode = 200;
                    return Json(new { message = "完善信息成功！" });
                }
                else
                {
                    Response.StatusCode = 500;
                    return Json(new { message = "完善信息异常！" });
                }
            }
        }
        #endregion 完善个人信息
    }
}