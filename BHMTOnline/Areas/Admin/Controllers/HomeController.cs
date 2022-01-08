using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;
using PagedList;

namespace BHMTOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private BHMTModel db = new BHMTModel();

        // GET: Admin/Home
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.SanPhams.OrderBy(x => x.MaSP);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,ManHinh,CPU,Ram,DoHoa,OCung,Pin,XuatXu,NamRaMat,MaHSX,MaHDH")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
