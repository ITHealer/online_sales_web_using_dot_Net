using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;

namespace BHMTOnline.Areas.Staff.Controllers
{
    public class ChiTietDonHangsController : Controller
    {
        private BHMTModel db = new BHMTModel();

        // GET: Staff/ChiTietDonHangs
        public ActionResult Index(string searchString)
        {
            var links = from l in db.ChiTietDonHangs select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                //Có thể search theo ngày giao ?
                links = links.Where(s => s.MaDDH.ToString().Contains(searchString));
            }
            return View(links);
        }

        // GET: Staff/ChiTietDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: Staff/ChiTietDonHangs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaDDH = new SelectList(db.DonDatHangs, "MaDDH", "MaDDH");
        //    ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
        //    return View();
        //}

        // POST: Staff/ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaDDH,MaSP,SoLuong,DonGia,ThanhTien")] ChiTietDonHang chiTietDonHang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ChiTietDonHangs.Add(chiTietDonHang);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaDDH = new SelectList(db.DonDatHangs, "MaDDH", "MaDDH", chiTietDonHang.MaDDH);
        //    ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
        //    return View(chiTietDonHang);
        //}

        // GET: Staff/ChiTietDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDDH = new SelectList(db.DonDatHangs, "MaDDH", "MaDDH", chiTietDonHang.MaDDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // POST: Staff/ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDDH,MaSP,SoLuong,DonGia,ThanhTien")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDDH = new SelectList(db.DonDatHangs, "MaDDH", "MaDDH", chiTietDonHang.MaDDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // GET: Staff/ChiTietDonHangs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
        //    if (chiTietDonHang == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chiTietDonHang);
        //}

        //// POST: Staff/ChiTietDonHangs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
        //    db.ChiTietDonHangs.Remove(chiTietDonHang);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
