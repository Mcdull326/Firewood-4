using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataLinq;
using BLL;

namespace Firewood.Controllers
{
    public class HomeController : Controller
    {
        BLL_User userBLL = new BLL_User();

        #region 判断是否登录
        public ActionResult IsLog()
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
                        }
                    }
                }
            }
            return View();
        }
        #endregion 判断是否登录

        public ActionResult Index()
        {
            return IsLog();
        }
    }
}
