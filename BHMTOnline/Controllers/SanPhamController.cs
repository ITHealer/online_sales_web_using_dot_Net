using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;


namespace BHMTOnline.Controllers
{
    public class SanPhamController : Controller
    {
        private BHMTModel db = new BHMTModel();

        public ActionResult DanhSachSanPhamPartial()
        {
            var ds = db.SanPhams.ToList();
            return PartialView(ds);
        }

        public ActionResult chitiet(int MaSP = 0)
        {
            var chitiet = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }
      
    }
}