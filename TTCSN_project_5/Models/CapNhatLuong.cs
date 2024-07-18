namespace TTCSN_project_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapNhatLuong")]
    public partial class CapNhatLuong
    {
        [Key]
        [StringLength(20)]
        public string MaCapNhatLuong { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? LuongTruoc { get; set; }

        public int? LuongSau { get; set; }

        [StringLength(300)]
        public string GhiChu { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
