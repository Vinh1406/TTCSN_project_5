namespace TTCSN_project_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            CapNhatLuong = new HashSet<CapNhatLuong>();
            NgayNghi = new HashSet<NgayNghi>();
        }

        [Key]
        [StringLength(20)]
        [Required]
        public string MaNhanVien { get; set; }

        [StringLength(20)]
        public string MatKhau { get; set; }

        [StringLength(40)]
        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(100)]
        public string QueQuan { get; set; }

        public int? GioiTinh { get; set; }

        [StringLength(12)]
        public string sdt { get; set; }

        [StringLength(20)]
        public string MaPhongBan { get; set; }

        [StringLength(20)]
        public string MaChucVu { get; set; }

        [StringLength(20)]
        public string TaiKhoan { get; set; }

        public DateTime? DateCreate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapNhatLuong> CapNhatLuong { get; set; }

        public virtual ChucVu ChucVu { get; set; }

        public virtual Luong Luong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NgayNghi> NgayNghi { get; set; }

        public virtual PhongBan PhongBan { get; set; }

        public virtual TongNgayNghi TongNgayNghi { get; set; }
    }
}
