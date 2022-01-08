using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;

namespace BHMTOnline.Areas.Admin.Controllers
{
    public class DoanhThuController : Controller
    {
        private BHMTModel db = new BHMTModel();
        // GET: Admin/DoanhThu
        public ActionResult Index()
        {
            var doanhthu = db.ChiTietDonHangs.Sum(n => n.ThanhTien);
            ViewBag.dt = doanhthu;
            return View();
        }
    }
}