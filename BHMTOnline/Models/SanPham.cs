namespace BHMTOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        public int MaSP { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        public int SoLuong { get; set; }

        public double DonGiaNhap { get; set; }

        public double DonGiaBan { get; set; }

        [StringLength(50)]
        public string HinhAnh { get; set; }

        [StringLength(50)]
        public string ManHinh { get; set; }

        [StringLength(50)]
        public string CPU { get; set; }

        [StringLength(50)]
        public string Ram { get; set; }

        [StringLength(50)]
        public string DoHoa { get; set; }

        [StringLength(50)]
        public string OCung { get; set; }

        [StringLength(50)]
        public string Pin { get; set; }

        [StringLength(30)]
        public string XuatXu { get; set; }

        public int? NamRaMat { get; set; }

        public int MaHSX { get; set; }

        public int MaHDH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual HangSanXuat HangSanXuat { get; set; }

        public virtual HeDieuHanh HeDieuHanh { get; set; }
    }
}
