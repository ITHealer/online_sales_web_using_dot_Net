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
    public class DonDatHangsController : Controller
    {
        private BHMTModel db = new BHMTModel();

        public ActionResult ChiTiet(int? id)
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

        // GET: Admin/DonDatHangs
        public ActionResult Index(string searchString)
        {
            var links = from l in db.DonDatHangs select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.TinhTrang.ToString().Contains(searchString));
            }
            return View(links);
        }

        // GET: Admin/DonDatHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen");
            return View();
        }

        // POST: Admin/DonDatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDDH,NgayDat,NgayGiao,TinhTrang,DaThanhToan,MaNguoiDung")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen", donDatHang.MaNguoiDung);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen", donDatHang.MaNguoiDung);
            return View(donDatHang);
        }

        // POST: Admin/DonDatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDDH,NgayDat,NgayGiao,TinhTrang,DaThanhToan,MaNguoiDung")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTen", donDatHang.MaNguoiDung);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        public void DelChiTietDonHang(int? id)
        {
            var chitietdondathang = db.ChiTietDonHangs.Where(s => s.MaDDH.ToString().Contains(id.ToString()) );
            foreach (var item in chitietdondathang)
            {
                db.ChiTietDonHangs.Remove(item);
            }
            
            db.SaveChanges();
        }

        // POST: Admin/DonDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DelChiTietDonHang(id);
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(donDatHang);
            db.SaveChanges();
            return RedirectToAction("Index");
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
