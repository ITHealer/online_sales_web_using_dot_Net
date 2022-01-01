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
    public class QuyensController : Controller
    {
        private BHMTModel db = new BHMTModel();

        // GET: Admin/Quyens
        public ActionResult Index()
        {
            return View(db.Quyens.ToList());
        }

        // GET: Admin/Quyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quyen quyen = db.Quyens.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        // GET: Admin/Quyens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Quyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDQuyen,TenQuyen")] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Quyens.Add(quyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quyen);
        }

        // GET: Admin/Quyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quyen quyen = db.Quyens.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        // POST: Admin/Quyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDQuyen,TenQuyen")] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quyen);
        }

        // GET: Admin/Quyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quyen quyen = db.Quyens.Find(id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }

        // POST: Admin/Quyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quyen quyen = db.Quyens.Find(id);
            db.Quyens.Remove(quyen);
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
