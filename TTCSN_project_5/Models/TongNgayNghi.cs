namespace TTCSN_project_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TongNgayNghi")]
    public partial class TongNgayNghi
    {
        [Key]
        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Required]
        [StringLength(40)]
        public string HoTen { get; set; }

        public int? TongSoNgayNghi { get; set; }

        [StringLength(300)]
        public string GhiChu { get; set; }

        public DateTime? DateCreate { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
