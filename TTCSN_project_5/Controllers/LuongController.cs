using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCSN_project_5.Models;

namespace TTCSN_project_5.Controllers
{
    public class LuongController : Controller
    {
        // GET: Luong
        Model1 db = new Model1();

        // GET: Luong
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UpdateLuong(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var currentlg = db.Luong.Where(n => n.MaNhanVien == id).FirstOrDefault();
            ViewBag.hoten = db.NhanVien.Where(n => n.MaNhanVien == currentlg.MaNhanVien).FirstOrDefault().HoTen;
            return View(currentlg);
        }

        public ActionResult SaveUpdateLuong(InformationLuong lg)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var luongcuNV = db.Luong.Where(n => n.MaNhanVien == lg.MaNhanVien).FirstOrDefault();
            var luongcu = (luongcuNV.LuongCoBan * luongcuNV.HeSoLuong) * (1 - luongcuNV.Thue / 100) + luongcuNV.TienThuong - luongcuNV.TienPhat + luongcuNV.PhuCap;
            var sal = db.Luong.Where(n => n.MaNhanVien == lg.MaNhanVien).FirstOrDefault();

            sal.LuongCoBan = lg.LuongCoBan;
            int luongInt = (int)(lg.HeSoLuong * 1000);
            double formathsl =(double) luongInt/ 1000;
            sal.HeSoLuong = Math.Round((float)lg.HeSoLuong, 3);
            sal.TienThuong = lg.TienThuong;
            sal.TienPhat = lg.TienPhat;
            sal.PhuCap = lg.PhuCap;
            var tl = (sal.LuongCoBan * sal.HeSoLuong) + sal.TienThuong - sal.TienPhat + sal.PhuCap;
            if (tl <= 5000000 && tl > 0)
                sal.Thue = 5;
            else if (tl <= 10000000)
                sal.Thue = 10;
            else if (tl <= 18000000)
                sal.Thue = 15;
            else if (tl <= 32000000)
                sal.Thue = 20;
            else if (tl <= 52000000)
                sal.Thue = 25;
            else if (tl <= 80000000)
                sal.Thue = 30;
            else
                sal.Thue = 35;
            CapNhatLuong updatel = new CapNhatLuong();
            updatel.MaCapNhatLuong = "";
            string kyTu = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                updatel.MaCapNhatLuong += kyTu[random.Next(kyTu.Length)];
            }
            updatel.MaNhanVien = lg.MaNhanVien;
            updatel.NgayCapNhat = DateTime.Now;

            updatel.LuongTruoc = Convert.ToInt32(luongcu);

            var tmp = (sal.LuongCoBan * sal.HeSoLuong) * (1 - sal.Thue / 100) + sal.TienThuong - sal.TienPhat + sal.PhuCap;
            updatel.LuongSau = Convert.ToInt32(tmp);
            updatel.GhiChu = lg.GhiChu;

            db.CapNhatLuong.Add(updatel);
            db.SaveChanges();
            return Redirect("/Home/QLLuong");
        }

        public ActionResult ChiTietCapNhat(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var cnl = db.CapNhatLuong.Where(n => n.MaNhanVien == id).OrderBy(n => n.NgayCapNhat).ToList();
            ViewBag.lg = cnl.Count();
            return View(cnl);
        }
        public ActionResult SearchNhanVien(string name)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            name = name.ToLower();
            var listluong = db.Luong.ToList();
            var listnv = db.NhanVien.ToList();
            var listnvbyName = db.NhanVien.Where(n => n.HoTen.Contains(name)).ToList();
            var listmanv = new List<string>();
            foreach (var item in listnvbyName)
            {
                listmanv.Add(item.MaNhanVien);
            }
            List<Luong> listluongsearch = new List<Luong>();
            foreach(var item in listmanv)
            {
                var currentLuong = db.Luong.Where(n => n.MaNhanVien.Equals(item)).FirstOrDefault();
                listluongsearch.Add(currentLuong);
            }
            TempData["SearchResultLuongByNhanVien"] = listluongsearch;
            return Redirect("/Home/QLLuong");
        }

    }
}