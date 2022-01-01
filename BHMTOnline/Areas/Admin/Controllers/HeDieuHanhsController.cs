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
    public class HeDieuHanhsController : Controller
    {
        private BHMTModel db = new BHMTModel();

        // GET: Admin/HeDieuHanhs
        public ActionResult Index()
        {
            return View(db.HeDieuHanhs.ToList());
        }

        // GET: Admin/HeDieuHanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeDieuHanh heDieuHanh = db.HeDieuHanhs.Find(id);
            if (heDieuHanh == null)
            {
                return HttpNotFound();
            }
            return View(heDieuHanh);
        }

        // GET: Admin/HeDieuHanhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HeDieuHanhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHDH,TenHDH")] HeDieuHanh heDieuHanh)
        {
            if (ModelState.IsValid)
            {
                db.HeDieuHanhs.Add(heDieuHanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(heDieuHanh);
        }

        // GET: Admin/HeDieuHanhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeDieuHanh heDieuHanh = db.HeDieuHanhs.Find(id);
            if (heDieuHanh == null)
            {
                return HttpNotFound();
            }
            return View(heDieuHanh);
        }

        // POST: Admin/HeDieuHanhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHDH,TenHDH")] HeDieuHanh heDieuHanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heDieuHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(heDieuHanh);
        }

        // GET: Admin/HeDieuHanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeDieuHanh heDieuHanh = db.HeDieuHanhs.Find(id);
            if (heDieuHanh == null)
            {
                return HttpNotFound();
            }
            return View(heDieuHanh);
        }

        // POST: Admin/HeDieuHanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HeDieuHanh heDieuHanh = db.HeDieuHanhs.Find(id);
            db.HeDieuHanhs.Remove(heDieuHanh);
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
