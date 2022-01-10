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
            //Kiểm tra đang đăng nhập và hiển thị nội dung đơn hàng
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            NguoiDung kh = (NguoiDung)Session["use"];
            int maND = kh.MaNguoiDung;
            // Sử dụng Include để tham chiếu đến bảng người dùng để lấy các trường thông tin.
            // Thay vì dùng 2 câu truy vấn thì có thể gộp thành một.
            var donhangs = db.DonDatHangs.Include(d => d.NguoiDung).Where(d => d.MaNguoiDung == maND);
            return View(donhangs.ToList());
        }

        // GET: DonDatHangs/Delete/5
        // Tìm đơn hàng cần xóa và hiển thị 1 view thông tin của đơn hàng cần xóa
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

        // Nếu xóa đơn hàng thực hiện xóa all mã đơn hàng có trong chi tiết đơn hàng trước
        public void DelChiTietDonHang(int? id)
        {
            var chitietdondathang = db.ChiTietDonHangs.Where(s => s.MaDDH.ToString().Contains(id.ToString()));
            foreach (var item in chitietdondathang)
            {
                db.ChiTietDonHangs.Remove(item);
            }

            db.SaveChanges();
        }

        // Nếu hủy đơn hàng thì cập nhật số lượng của sp; khi tình trạng 1 mới được hủy đơn
        public void CapNhatSoLuongKhiHuyDon(int? id)
        {
            var chitietdondathang = db.ChiTietDonHangs.Where(s => s.MaDDH.ToString().Contains(id.ToString()));
            foreach (var item in chitietdondathang)
            {
                int masp = item.MaSP;
                int sl = item.SoLuong;
                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == masp);
                sp.SoLuong += sl;
            }
            db.SaveChanges();
        }

        // POST: Admin/DonDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {           
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if(donDatHang.TinhTrang.ToString() == "1")
            {
                CapNhatSoLuongKhiHuyDon(id);
                DelChiTietDonHang(id);
                // Sau khi cập nhật số lượng và xóa chi tiết đơn hàng, ta xóa đơn hàng
                db.DonDatHangs.Remove(donDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }   
            return RedirectToAction("Index");
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
