using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TTCSN_project_5.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CapNhatLuong> CapNhatLuong { get; set; }
        public virtual DbSet<ChucVu> ChucVu { get; set; }
        public virtual DbSet<Luong> Luong { get; set; }
        public virtual DbSet<NgayNghi> NgayNghi { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<PhongBan> PhongBan { get; set; }
        public virtual DbSet<QuanTri> QuanTri { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TongNgayNghi> TongNgayNghi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapNhatLuong>()
                .Property(e => e.MaCapNhatLuong)
                .IsUnicode(false);

            modelBuilder.Entity<CapNhatLuong>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<ChucVu>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<Luong>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NgayNghi>()
                .Property(e => e.MaNgayNghi)
                .IsUnicode(false);

            modelBuilder.Entity<NgayNghi>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaPhongBan)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasOptional(e => e.Luong)
                .WithRequired(e => e.NhanVien);

            modelBuilder.Entity<NhanVien>()
                .HasOptional(e => e.TongNgayNghi)
                .WithRequired(e => e.NhanVien);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.MaPhongBan)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.sdtLienHe)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<TongNgayNghi>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);
        }
    }
}
