using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BHMTOnline.Models
{
    // DbContext quản lý các thực thể trong bộ nhớ để biết nó có được đồng bộ với dòng tương ứng trong CSDL hay k
    public partial class BHMTModel : DbContext
    {
        public BHMTModel()
            : base("name=BHMTModel")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<HeDieuHanh> HeDieuHanhs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Entity<Name> là một delegate
            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDonHangs) // Cấu hình quan hệ 1-nhiều or nhiều-nhiều
                .WithRequired(e => e.DonDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangSanXuat>()
                .Property(e => e.TenHSX)
                .IsUnicode(false); // Cấu hình thuộc tính chuỗi có thể chứa các ký tự Unicode hay k

            modelBuilder.Entity<HangSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.HangSanXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HeDieuHanh>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.HeDieuHanh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.DonDatHangs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.NguoiDungs)
                .WithRequired(e => e.Quyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.CPU)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Ram)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.DoHoa)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.OCung)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Pin)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);
        }
    }
}
