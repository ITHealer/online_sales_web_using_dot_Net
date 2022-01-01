namespace BHMTOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HeDieuHanh")]
    public partial class HeDieuHanh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HeDieuHanh()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int MaHDH { get; set; }

        [StringLength(20)]
        public string TenHDH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
