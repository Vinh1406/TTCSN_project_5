using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTCSN_project_5.Models
{
    public class InformationLuong
    {
        public string MaNhanVien { get; set; }
        public int LuongCoBan { get; set; }
        public double HeSoLuong { get; set; }
        public int TienPhat { get; set; }
        public int TienThuong { get; set; }
        public int PhuCap { get; set; }
        public double Thue { get; set; }

        public string GhiChu { get; set; }
    }
}