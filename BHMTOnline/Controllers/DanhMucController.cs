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
    public class DanhMucController : Controller
    {
        private BHMTModel db = new BHMTModel();

        public ActionResult DanhmucPartial()
        {
            var danhmuc = db.HangSanXuats.ToList();
            return PartialView(danhmuc);
        }

        // GET: DanhMuc
        public ActionResult Index()
        {
            return View(db.HangSanXuats.ToList());
        }

        // Phân loại sản phẩm theo hãng sản xuất
        public ActionResult Category(int? idMaHSX)
        {
            var category = db.SanPhams.Where(n => n.HangSanXuat.MaHSX == idMaHSX).ToList();
            if (category == null)
            {
                return HttpNotFound();
            }
            return PartialView(category);
        }

        //Chi tiết khi chọn sp theo danh mục
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

        // GET: DanhMuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            if (hangSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(hangSanXuat);
        }

        // GET: DanhMuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DanhMuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHSX,TenHSX")] HangSanXuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.HangSanXuats.Add(hangSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangSanXuat);
        }

        // GET: DanhMuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            if (hangSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(hangSanXuat);
        }

        // POST: DanhMuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHSX,TenHSX")] HangSanXuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangSanXuat);
        }

        // GET: DanhMuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            if (hangSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(hangSanXuat);
        }

        // POST: DanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangSanXuat hangSanXuat = db.HangSanXuats.Find(id);
            db.HangSanXuats.Remove(hangSanXuat);
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
