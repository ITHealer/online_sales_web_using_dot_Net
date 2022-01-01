namespace BHMTOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DonDatHangs = new HashSet<DonDatHang>();
        }

        [Key]
        public int MaNguoiDung { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(11)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        public int IDQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
