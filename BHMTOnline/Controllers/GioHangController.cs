using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHMTOnline.Models;
namespace BHMTOnline.Controllers
{
    public class GioHangController : Controller
    {
        private BHMTModel db = new BHMTModel();
        // GET: GioHang
        // Lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            // Ép Session["GioHang"] thành một list.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tạo list giỏ hàng (Session["GioHang"])
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //Thêm vào giỏ hàng, truyền vào id sản phẩm
        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            // Truy xuất sản phẩm có iMasp trong bảng sản phẩm.
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy ra session giỏ hàng, từ phương thức lấy giỏ hàng ở trên
            List<GioHang> lstGioHang = LayGioHang();
            // Kiểm tra sp này đã tồn tại trong Session["GioHang"] chưa
            // GioHang là phương thức trong class GioHang.cs ở Models
            GioHang sanpham = lstGioHang.Find(n => n.iMasp == iMasp);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasp);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp, chỉnh sửa giỏ hàng cập nhật số lượng
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //Kiểm tra masp
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            // Nếu sản phẩm có trong list thì xóa sản phẩm đó đi
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMasp == iMaSP);

            }
            // Nếu giỏ hàng không còn sản phẩm nào thì chuyển về trang chủ
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng; GioHangPartial sẽ gọi Action này để xem giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng, hiện thị ở chỗ giỏ hàng đã thêm tổng số bn sp.
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        // Tạo GioHangPartial
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            //ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        private void CapNhatSoLuong(int iMaSP, int sl)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            sp.SoLuong -= sl;
            db.SaveChanges();
        }

        #region
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            // Kiểm tra đã đăng nhập hay chưa
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            // Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            NguoiDung kh = (NguoiDung)Session["use"];
            List<GioHang> gh = LayGioHang();
            ddh.MaNguoiDung = kh.MaNguoiDung;
            ddh.NgayDat = DateTime.Now;
            // 1: đang chờ xác nhận; 0: là đã xác nhận rồi (đã giao)
            ddh.TinhTrang = 1;
            Console.WriteLine(ddh);
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng. Khi cần xem chi tiết đơn hàng
            foreach (var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                double thanhtien = item.iSoLuong * (double)item.dDonGia;
                ctDH.MaDDH = ddh.MaDDH;
                ctDH.MaSP = item.iMasp;
                ctDH.SoLuong = item.iSoLuong;
                // Cập nhật lại số lượng sản phẩm còn trong kho
                CapNhatSoLuong(item.iMasp, item.iSoLuong);

                ctDH.DonGia = (double)item.dDonGia;
                ctDH.ThanhTien = (double)thanhtien;
                db.ChiTietDonHangs.Add(ctDH);
            }
            db.SaveChanges();
            // Clear giỏ hàng sau khi đã đặt hàng; HttpContext có tác dụng trong 1 request
            HttpContext.Session.Remove("GioHang");
            // Gọi đến action Index của controller DonHangs.
            return RedirectToAction("Index", "DonHangs");
        }
        #endregion
    }
}