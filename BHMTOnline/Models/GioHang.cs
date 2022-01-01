using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHMTOnline.Models
{
    public class GioHang
    {
        private BHMTModel db = new BHMTModel();
        public int iMasp { get; set; }
        public string sTensp { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int Masp)
        {
            iMasp = Masp;
            //Trả về phần tử duy nhất trong danh sách thỏa mãn điều kiện;
            SanPham sp = db.SanPhams.Single(n => n.MaSP == iMasp); 
            sTensp = sp.TenSP;
            sHinhAnh = sp.HinhAnh;
            dDonGia = double.Parse(sp.DonGiaBan.ToString());
            iSoLuong = 1;
        }

    }
}