namespace TTCSN_project_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuanTri")]
    public partial class QuanTri
    {
        [Key]
        [StringLength(30)]
        public string TenDangNhap { get; set; }

        [StringLength(30)]
        public string MatKhau { get; set; }

        [StringLength(40)]
        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(100)]
        public string QueQuan { get; set; }

        public int? GioiTinh { get; set; }

        [StringLength(12)]
        public string sdt { get; set; }
    }
}
