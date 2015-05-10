using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataLinq;
using BLL;

namespace Firewood.Controllers
{
    public class ActController : Controller
    {
        BLL_Act actBLL = new BLL_Act();

        [HttpGet]
        public ActionResult Index()
        {
            ViewData.Model = actBLL.SelectAllAct();
            return View();
        }
    }
}
