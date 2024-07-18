namespace TTCSN_project_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Luong")]
    public partial class Luong
    {
        [Key]
        [StringLength(20)]
        public string MaNhanVien { get; set; }

        public int? LuongCoBan { get; set; }

        public double? HeSoLuong { get; set; }

        public int? TienPhat { get; set; }

        public int? TienThuong { get; set; }

        public int? PhuCap { get; set; }

        public double? Thue { get; set; }

        public DateTime? DateCreate { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
