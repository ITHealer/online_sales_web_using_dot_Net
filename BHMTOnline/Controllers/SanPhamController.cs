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

        public ActionResult dellpartial()
        {
            var dell = db.SanPhams.Where(n => n.HangSanXuat.TenHSX == "Dell").ToList();
            return PartialView(dell);
        }
        public ActionResult asuspartial()
        {
            var asus = db.SanPhams.Where(n => n.HangSanXuat.TenHSX == "Asus").ToList();
            return PartialView(asus);
        }

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
        // GET: SanPhams
        //public ActionResult Index()
        //{
        //    var sanPhams = db.SanPhams.Include(s => s.HangSanXuat).Include(s => s.HeDieuHanh);
        //    return View(sanPhams.ToList());
        //}

        //// GET: SanPhams/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sanPham);
        //}

        //// GET: SanPhams/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX");
        //    ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH");
        //    return View();
        //}

        //// POST: SanPhams/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SanPhams.Add(sanPham);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", sanPham.MaHSX);
        //    ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", sanPham.MaHDH);
        //    return View(sanPham);
        //}

        //// GET: SanPhams/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", sanPham.MaHSX);
        //    ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", sanPham.MaHDH);
        //    return View(sanPham);
        //}

        //// POST: SanPhams/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sanPham).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", sanPham.MaHSX);
        //    ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", sanPham.MaHDH);
        //    return View(sanPham);
        //}

        //// GET: SanPhams/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sanPham);
        //}

        //// POST: SanPhams/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    db.SanPhams.Remove(sanPham);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}