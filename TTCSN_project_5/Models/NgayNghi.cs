namespace TTCSN_project_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NgayNghi")]
    public partial class NgayNghi
    {
        [Key]
        [StringLength(20)]
        public string MaNgayNghi { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNghiDate { get; set; }

        [StringLength(300)]
        public string LyDo { get; set; }

        [StringLength(300)]
        public string GhiChu { get; set; }

        public DateTime? DateCreate { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
