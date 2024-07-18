using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTCSN_project_5.Models
{
    public class InformationNhanVien
    {
        public string MaNhanVien { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string GioiTinh { get; set; }
        public string sdt { get; set; }
        public string PhongBan { get; set; }
        public string ChucVu { get; set; }

        public string TaiKhoan { get; set; }
    }
}