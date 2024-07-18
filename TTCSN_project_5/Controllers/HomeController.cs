using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTCSN_project_5.Models;

namespace TTCSN_project_5.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            ViewBag.soLuongNhanVien = db.NhanVien.Count();
                ViewBag.soLuongPhongBan = db.PhongBan.Count();
                //chart 1
                List<string> Label = new List<string>();
                for (int i = 0; i < ViewBag.soLuongPhongBan; i++)
                {
                    Label.Add(db.PhongBan.ToList()[i].TenPhongBan.ToString());
                }
                List<int> dt = new List<int>();
                for (int i = 0; i < ViewBag.soLuongPhongBan; i++)
                {
                    string mpb = db.PhongBan.ToList()[i].MaPhongBan.ToString();
                    dt.Add(db.NhanVien.Where(x => x.MaPhongBan == mpb).Count());
                }
                ViewBag.Label = Label;
                ViewBag.Data = dt;
                //chart 2
                List<string> LabelThang = new List<string>();
                for (int i = 1; i <= DateTime.Now.Month; i++)
                {
                    LabelThang.Add($"Tháng {i}");
                }
                ViewBag.LabelThang = LabelThang;
                List<int> nghiValue = new List<int>();
                for (int i = 0; i < DateTime.Now.Month; i++)
                {
                    int sln = db.NgayNghi.Where(x => x.NgayNghiDate.Value.Month == (i + 1)).Count();
                    nghiValue.Add(sln);
                }
                ViewBag.nghiValue = nghiValue;

                List<int> nvThemTrongThang = new List<int>();
                for (int i = 0; i < DateTime.Now.Month; i++)
                {
                    int sln = db.NhanVien.Where(x => x.DateCreate.Value.Month == (i + 1)).Count();
                    nvThemTrongThang.Add(sln);
                }
                ViewBag.nvThemTrongThang = nvThemTrongThang;

                List<int> nvmPhong = new List<int>();
                List<string> lbmPhong = new List<string>();
                for (int i = 0; i < ViewBag.soLuongPhongBan; i++)
                {
                    lbmPhong.Add(db.PhongBan.ToList()[i].TenPhongBan);
                    var p = db.PhongBan.ToList()[i].MaPhongBan;
                    nvmPhong.Add(db.NhanVien.Where(x => x.MaPhongBan == p && x.DateCreate.Value.Month == DateTime.Now.Month && x.DateCreate.Value.Year == DateTime.Now.Year).Count());
                }
                ViewBag.nvmPhong = nvmPhong;
                ViewBag.lbmPhong = lbmPhong;
                return View();
        }

        public ActionResult QLNhanVien()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var listnv1 = TempData["SearchResults"] as List<NhanVien>;
                var listnv2 = TempData["SearchResulstByChucVu"] as List<NhanVien>;
                var listnv3 = TempData["SearchResulstByPhongBan"] as List<NhanVien>;
                var listnv = new List<NhanVien>();
                listnv = (listnv1 != null) ? (listnv1) : ((listnv2 != null) ? (listnv2) : listnv3);
                List<NhanVien> nv = new List<NhanVien>();
                if (listnv == null)
                {
                    nv = db.NhanVien.OrderByDescending(x => x.DateCreate).ToList();
                }
                else nv = listnv;
                ViewBag.lg = nv.Count();
                Session["TongSo"] = nv.Count();
                ViewBag.listPB = db.PhongBan.ToList();
                ViewBag.listCV = db.ChucVu.ToList();
                return View(nv);
        }
        public ActionResult QLLuong()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            List<Luong> luong = new List<Luong>();
                var listluong1 = TempData["SearchResultLuongByNhanVien"] as List<Luong>;
                luong = (listluong1 != null) ? listluong1 : (db.Luong.OrderByDescending(x => x.DateCreate).ToList());
                ViewBag.lg = luong.Count();
                ViewBag.listNV = db.NhanVien.ToList();
                return View(luong);
        }

        public ActionResult QLPhongBan()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            ViewBag.listpb = db.PhongBan.ToList();
            List<PhongBan> pb = new List<PhongBan>();
            var listpb = TempData["SearchResultsPhongBan"] as List<PhongBan>;
            if (listpb != null)
            {
                pb = listpb;
            }
            else { pb = db.PhongBan.OrderByDescending(x => x.DateCreate).ToList(); }
            ViewBag.lg = pb.Count();
            return View(pb);
        }

        public ActionResult QLNgayNghi()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            List<TongNgayNghi> nghi = new List<TongNgayNghi>();
            var listnn1= TempData["SearchResultsNhaVienByTNN"] as List<TongNgayNghi>;
            var listnn2 = TempData["SortResultsByTSNN"] as List<TongNgayNghi>;
            nghi = (listnn1 != null) ? listnn1 : listnn2;
            if(nghi == null)
            {
                nghi =  db.TongNgayNghi.OrderByDescending(x => x.DateCreate).ToList();
            }
            ViewBag.lg = nghi.Count();
            for (int i = 0; i < ViewBag.lg; i++)
            {
                string tmp = nghi[i].MaNhanVien;
                var queryNV = db.NgayNghi.Where(n => n.MaNhanVien.Equals(tmp)).ToList();
                nghi[i].TongSoNgayNghi = queryNV.Count();
                nghi[i].HoTen = db.NhanVien.Where(n => n.MaNhanVien == tmp).FirstOrDefault().HoTen;
            }
            db.SaveChanges();
            return View(nghi);
        }
        public ActionResult ExportToExcel(string code)
        {
            var query = Enumerable.Empty<object>();
            if (code == "NhanVien")
                query = db.NhanVien.ToList();
            else if (code == "PhongBan")
                query = db.PhongBan.ToList();
            else if (code == "Luong")
                query = db.Luong.ToList();
            else if (code == "NgayNghi")
                query = db.NgayNghi.ToList();

            var grid = new GridView();
            grid.DataSource = query;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment; filename=DanhSach{code}.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }
    }
}