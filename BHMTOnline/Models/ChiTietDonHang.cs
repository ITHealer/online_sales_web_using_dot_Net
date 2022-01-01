namespace BHMTOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public int SoLuong { get; set; }

        public double DonGia { get; set; }
        public double? ThanhTien { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
