using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;
using Microsoft.SharePoint.Client;
using PagedList;

namespace BHMTOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private BHMTModel db = new BHMTModel();

        // GET: Admin/Home
        public ActionResult Index(int? page, string SearchString)
        {
            var sp = new List<SanPham>();
            if (SearchString != null)
            {
                page = 1;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                sp = db.SanPhams.Where(n => n.TenSP.Contains(SearchString)).ToList();
            }
            else
            {
                sp = db.SanPhams.OrderBy(x => x.MaSP).ToList();
            }

            if (page == null) page = 1;

            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

        }

        // GET: Admin/Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/Home/Create
        public ActionResult Create()
        {
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX");
            ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH");
            return View();
        }

        // POST: Admin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham, HttpPostedFileBase img)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/HinhAnhSP"), _FileName);
                        HinhAnh.SaveAs(_path);
                        sanPham.HinhAnh = _FileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }

            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", sanPham.MaHSX);
            ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", sanPham.MaHDH);
            return View(sanPham);
        }

        // GET: Admin/Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", sanPham.MaHSX);
            ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", sanPham.MaHDH);
            return View(sanPham);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham, HttpPostedFileBase HinhAnh, System.Web.Mvc.FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnh != null)
                    {
                        string _FileName = Path.GetFileName(HinhAnh.FileName);
                        string _path = Path.Combine(Server.MapPath("~/HinhAnhSP"), _FileName);
                        HinhAnh.SaveAs(_path);
                        sanPham.HinhAnh = _FileName;
                        _path = Path.Combine(Server.MapPath("~/HinhAnhSP"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path); // xóa hình cũ

                    }
                    else
                        sanPham.HinhAnh = form["oldimage"];
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }

            ViewBag.MaHSX = new SelectList(db.HangSanXuats, "MaHSX", "TenHSX", sanPham.MaHSX);
            ViewBag.MaHDH = new SelectList(db.HeDieuHanhs, "MaHDH", "TenHDH", sanPham.MaHDH);
            return View(sanPham);
        }

        // GET: Admin/Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
