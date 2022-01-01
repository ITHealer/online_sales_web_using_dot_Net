using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;
using System.Data;
using System.Data.Entity;

namespace BHMTOnline.Controllers
{
    public class DonHangsController : Controller
    {
        private BHMTModel db = new BHMTModel();
        // GET: Donhangs
        // Hiển thị danh sách đơn hàng
        public ActionResult Index()
        {
            //Kiểm tra đang đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            NguoiDung kh = (NguoiDung)Session["use"];
            int maND = kh.MaNguoiDung;
            var donhangs = db.DonDatHangs.Include(d => d.NguoiDung).Where(d => d.MaNguoiDung == maND);
            return View(donhangs.ToList());
        }

        // GET: Donhangs/Details/5
        //Hiển thị chi tiết đơn hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donhang = db.DonDatHangs.Find(id);
            var chitiet = db.ChiTietDonHangs.Include(d => d.SanPham).Where(d => d.MaDDH == id).ToList();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
