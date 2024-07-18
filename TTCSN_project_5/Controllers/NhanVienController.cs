using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using TTCSN_project_5.Models;

namespace TTCSN_project_5.Controllers
{
    public class NhanVienController : Controller
    {
        Model1 db = new Model1();
        bool cfDelete;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNhanVien()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var pb = db.PhongBan.ToList();
            var cv = db.ChucVu.ToList();
            ViewBag.listpb = pb;
            ViewBag.listcv = cv;
            ViewBag.lgcv = cv.Count();
            ViewBag.lg = pb.Count();
            ViewBag.listMaNhanVien = db.NhanVien.Select(nv => nv.MaNhanVien);
            ViewBag.listTaiKhoan = db.NhanVien.Select(nv => nv.TaiKhoan);
            return View();
        }
        [HttpGet]
        public ActionResult UpdateNhanVien(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            //var user = db.NhanViens.Where(n => n.MaNhanVien == id).FirstOrDefault();
            var currentNhanVien = db.NhanVien.Where(n => n.MaNhanVien == id).FirstOrDefault();
            var pb = db.PhongBan.ToList();
            var cv = db.ChucVu.ToList();
            ViewBag.listpb = pb;
            ViewBag.listcv = cv;
            ViewBag.lgcv = cv.Count();
            ViewBag.lg = pb.Count();
            var currentPhongBan= db.PhongBan.Where(n => n.MaPhongBan == currentNhanVien.MaPhongBan).FirstOrDefault();
            var currentPositionPhongBan = pb.IndexOf(currentPhongBan);
            var tempPhongBan = pb[currentPositionPhongBan];
            pb[currentPositionPhongBan] = pb[0];
            pb[0] = tempPhongBan;
            var currentChucVu = db.ChucVu.Where(n => n.MaChucVu == currentNhanVien.MaChucVu).FirstOrDefault();
            var currentPositionChucVu = cv.IndexOf(currentChucVu);
            var tempChucVu = cv[currentPositionChucVu];
            cv[currentPositionChucVu] = cv[0];
            cv[0] = tempChucVu;
            db.SaveChanges();
            return View(currentNhanVien);
        }
        [HttpGet]
        public ActionResult RemoveNhanVien(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var currentNhanVien = db.NhanVien.Where(n => n.MaNhanVien == id).FirstOrDefault();
            var nvluong = db.Luong.Where(n => n.MaNhanVien == id).FirstOrDefault();
            var nvNgayNghi = db.NgayNghi.Where(n => n.MaNhanVien == id).ToList();
            var nvtongngaynghi = db.TongNgayNghi.Where(n => n.MaNhanVien == id).FirstOrDefault();
            var nvDesLuong = db.CapNhatLuong.Where(n => n.MaNhanVien == id).ToList();

            if (nvNgayNghi != null)
            {
                for (int i = 0; i < nvNgayNghi.Count(); i++)
                {   
                    db.NgayNghi.Remove(nvNgayNghi[i]);
                    db.SaveChanges();
                }
            }
            if (nvDesLuong != null)
            {
                for(int i = 0; i < nvDesLuong.Count(); i++)
                { 
                    db.CapNhatLuong.Remove(db.CapNhatLuong.Where(n => n.MaNhanVien == id).FirstOrDefault());
                    db.SaveChanges();
                }
            }
            if (nvtongngaynghi != null)
                db.TongNgayNghi.Remove(nvtongngaynghi);
            if (nvluong != null)
                db.Luong.Remove(nvluong);
            db.NhanVien.Remove(currentNhanVien);
            db.SaveChanges();
            return Redirect("/Home/QLNhanVien");
        }
        public ActionResult SaveUpDateNhanVien(InformationUpdateNhanVien updatenv)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            string id = updatenv.MaNhanVien;
            var nv = db.NhanVien.Where(n => n.MaNhanVien == id).FirstOrDefault();
            nv.MatKhau = updatenv.MatKhau;
            nv.HoTen = updatenv.HoTen;
            nv.NgaySinh = updatenv.NgaySinh;
            nv.QueQuan = updatenv.QueQuan;
            nv.sdt = updatenv.sdt;

            var currentPB = db.PhongBan.Where(n => n.TenPhongBan == updatenv.PhongBan).FirstOrDefault();
            nv.MaPhongBan = currentPB.MaPhongBan;

            var currentCV = db.ChucVu.Where(n => n.TenChucVu == updatenv.ChucVu).FirstOrDefault();
            nv.MaChucVu = currentCV.MaChucVu;
            var salary = db.Luong.Where(n => n.MaNhanVien == nv.MaNhanVien).FirstOrDefault();
            if (nv.MaChucVu == "001")
                salary.HeSoLuong = 4.2;
            else if (nv.MaChucVu == "002")
                salary.HeSoLuong = 3.3;
            else if (nv.MaChucVu == "003")
                salary.HeSoLuong = 2.9;
            else if (nv.MaChucVu == "004")
                salary.HeSoLuong = 2.5;
            else
                salary.HeSoLuong = 2.1;
            var tl = (salary.LuongCoBan * salary.HeSoLuong) + salary.TienThuong - salary.TienPhat + salary.PhuCap;
            if (tl <= 5000000 && tl > 0)
                salary.Thue = 5;
            else if (tl <= 10000000)
                salary.Thue = 10;
            else if (tl <= 18000000)
                salary.Thue = 15;
            else if (tl <= 32000000)
                salary.Thue = 20;
            else if (tl <= 52000000)
                salary.Thue = 25;
            else if (tl <= 80000000)
                salary.Thue = 30;
            else
                salary.Thue = 35;
            db.SaveChanges();
            return Redirect("/Home/QLNhanVien");
        }
        public ActionResult SaveNhanVien(InformationNhanVien nv)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            if (nv.HoTen == "nv")
            {
                ViewBag.messeage = "Họ tên không được để trống!";
                return Redirect("/NhanVien/AddNhanVien");
            }
            else
            {
                NhanVien newnv = new NhanVien();
                var nvdb = db.NhanVien.ToList();
                ViewBag.currentmanv = nv.MaNhanVien;
                ViewBag.messeage = "";
                foreach (var x in nvdb)
                {
                    if (x.MaNhanVien == nv.MaNhanVien)
                    {
                        ViewBag.messeage = "Mã nhân viên đã tồn tại! ";
                        //return View(nvdb);
                    }
                }
                newnv.MaNhanVien = "";
                ViewBag.MaNhanVien = "";
                newnv.MaNhanVien  = nv.MaNhanVien;
                newnv.MatKhau = nv.MatKhau;
                newnv.HoTen = nv.HoTen;
                newnv.NgaySinh = nv.NgaySinh;
                newnv.QueQuan = nv.QueQuan;
                newnv.TaiKhoan = nv.TaiKhoan;
                if (nv.GioiTinh.Equals("nam", StringComparison.CurrentCultureIgnoreCase))
                {
                    newnv.GioiTinh = 1;
                }
                else
                {
                    newnv.GioiTinh = 0;
                }
                var newCV = db.ChucVu.ToList();
                var currentCV = db.ChucVu.Where(n => n.TenChucVu == nv.ChucVu).FirstOrDefault();
                newnv.MaChucVu = currentCV.MaChucVu;

                newnv.sdt = nv.sdt;
                var currentPb = db.PhongBan.Where(n => n.TenPhongBan == nv.PhongBan).FirstOrDefault();
                newnv.MaPhongBan = currentPb.MaPhongBan;
                newnv.DateCreate = DateTime.Now;
                TongNgayNghi tnn = new TongNgayNghi();
                tnn.MaNhanVien = nv.MaNhanVien;
                tnn.HoTen = nv.HoTen;
                tnn.TongSoNgayNghi = 0;
                tnn.GhiChu = "";
                tnn.DateCreate = DateTime.Now;
                Luong salary = new Luong();
                salary.MaNhanVien = nv.MaNhanVien;
                salary.LuongCoBan = 4420000;
                if (nv.ChucVu == "Giám đốc")
                    salary.HeSoLuong = 4.2;
                else if (nv.ChucVu == "Phó Giám Đốc")
                    salary.HeSoLuong = 3.3;
                else if (nv.ChucVu == "Trưởng Phòng")
                    salary.HeSoLuong = 2.9;
                else if (nv.ChucVu == "Phó Trưởng Phòng")
                    salary.HeSoLuong = 2.5;
                else
                    salary.HeSoLuong = 2.1;
                salary.TienPhat = 0;
                salary.TienThuong = 0;
                salary.PhuCap = 0;
                salary.DateCreate = DateTime.Now;
                var tl = (salary.LuongCoBan * salary.HeSoLuong) + salary.TienThuong - salary.TienPhat + salary.PhuCap;
                if (tl <= 5000000 && tl > 0)
                    salary.Thue = 5;
                else if (tl <= 10000000)
                    salary.Thue = 10;
                else if (tl <= 18000000)
                    salary.Thue = 15;
                else if (tl <= 32000000)
                    salary.Thue = 20;
                else if (tl <= 52000000)
                    salary.Thue = 25;
                else if (tl <= 80000000)
                    salary.Thue = 30;
                else
                    salary.Thue = 35;
                db.Luong.Add(salary);
                db.TongNgayNghi.Add(tnn);
                db.NhanVien.Add(newnv);
                db.SaveChanges();
                return Redirect("/Home/QLNhanVien");
               
            }
            
        }

        public ActionResult SearchNhanVien(string name)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            name = name.ToLower();
            var listnv = db.NhanVien.Where(n => n.HoTen.Contains(name)).ToList();
            TempData["SearchResults"] = listnv;
            return RedirectToAction("QLNhanVien", "Home");
        }

        public ActionResult SearchNhanVienByChucVu(string nvByCV)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var macv = db.ChucVu.Where(n => n.TenChucVu.Contains(nvByCV)).FirstOrDefault().MaChucVu;
            var listcv = db.ChucVu.ToList();
            var listnv = db.NhanVien.ToList();
            var listnvbycv = db.NhanVien.Where(n => n.MaChucVu.Equals(macv)).ToList();
            TempData["SearchResulstByChucVu"] = listnvbycv;
            return RedirectToAction("QLNhanVien","Home");
        }

        public ActionResult SearchNhanVienByPhongBan(string nvByPB)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var mapb = db.PhongBan.Where(n => n.TenPhongBan.Contains(nvByPB)).FirstOrDefault();
            var listnvbypb = new List<NhanVien>();
            if (mapb != null)
            {
                listnvbypb = db.NhanVien.Where(n => n.MaPhongBan.Equals(mapb.MaPhongBan)).ToList();
            }
            TempData["SearchResulstByPhongBan"] = listnvbypb;
            return RedirectToAction("QLNhanVien", "Home");
        }

    }
}