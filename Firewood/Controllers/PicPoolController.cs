using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;

using BLL;

namespace Firewood.Controllers
{
    public class PicPoolController : Controller
    {
        BLL_Act actBLL = new BLL_Act();
        BLL_Org orgBLL = new BLL_Org();

        static Cache cache = HttpRuntime.Cache;
        // GET: PicPool
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">活动的guid</param>
        /// <param name="size">300x600 0x0是原图</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get(string type, string id, string size)
        {
            var width = Int32.Parse(size.Split('x')[0]);
            var height = Int32.Parse(size.Split('x')[1]);
            Image img = GetImg(type,id);
            Bitmap bmp;

            if (img == null) return HttpNotFound();

            if (width == 0 || height == 0)
            {
                bmp = new Bitmap(img);
            }
            else
            {
                bmp = new Bitmap(img, width, height);
            }

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);

            var result = ms.ToArray();
            ms.Dispose();
            Response.AddHeader("Cache-Control", "cache, store, must-revalidate");
            return File(result, "image/png");
        }

        [HttpGet]
        public ActionResult GetByWidth(string type, string id, string width)
        {
            Image img = GetImg(type,id);
            Bitmap bmp;
            var newWidth = Int32.Parse(width);

            if (img == null) return HttpNotFound();

            if (newWidth == 0)
            {
                bmp = new Bitmap(img);
            }
            else
            {
                var height = newWidth * img.Width / img.Height;
                bmp = new Bitmap(img, newWidth, height);
            }

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);

            var result = ms.ToArray();
            ms.Dispose();
            Response.AddHeader("Cache-Control", "cache, store, must-revalidate");
            return File(result, "image/png");
        }

        [HttpGet]
        public ActionResult GetByHeight(string type, string id, string height)
        {
            Image img = GetImg(type,id);
            Bitmap bmp;
            var newHeight = Int32.Parse(height);

            if (img == null) return HttpNotFound();

            if (newHeight == 0)
            {
                bmp = new Bitmap(img);
            }
            else
            {
                var width = newHeight * img.Width / img.Height;
                bmp = new Bitmap(img, width, newHeight);
            }

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);

            var result = ms.ToArray();
            ms.Dispose();
            Response.AddHeader("Cache-Control", "cache, store, must-revalidate");
            return File(result, "image/png");
        }

        public Image GetImg(string type, string actid)
        {
            Image img;

            if (cache[actid] != null)
            {
                img = cache[actid] as Image;
                return img;
            }
            else
            {
                string path="";

                if (type.Equals("0"))//活动海报
                {
                    path = actBLL.SelectPathByActID(new Guid(actid));
                }
                else if (type.Equals("1"))//社团logo
                {
                    path = orgBLL.SelectPathByOrgID(new Guid(actid));
                }

                if (!System.IO.File.Exists(path)) return null;
                else
                {
                    img = Bitmap.FromFile(path);
                    cache.Add(actid, img, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                    return img;
                }
            }
        }
    }
}
