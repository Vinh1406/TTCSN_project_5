using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCSN_project_5.Models;

namespace TTCSN_project_5.Controllers
{
    public class NgayNghiController : Controller
    {
        // GET: NgayNghi
        Model1 db = new Model1();
        List<string> listNgayNghi = new List<string>() { };
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InformationNgayNghi(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var currennv = db.NgayNghi.Where(n => n.MaNhanVien == id).OrderBy(n => n.NgayNghiDate).ToList();
            ViewBag.lgth = currennv.Count();
            ViewBag.HoTen = db.NhanVien.Where(n => n.MaNhanVien == id).FirstOrDefault().HoTen;
            ViewBag.id = id;
            return View(currennv);
        }
        [HttpGet]
        public ActionResult AddNgayNghi(InformationNgayNghi nn, string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            //var listNV = db.NhanVien.ToList();
            ViewBag.id = id;
            ViewBag.listMaNgayNghi = db.NgayNghi.Select(ngayNghi => ngayNghi.MaNgayNghi);
            var temlisst = db.NgayNghi.Select(ngaynghi => ngaynghi.NgayNghiDate);
            foreach (var item in temlisst)
            {
                listNgayNghi.Add(String.Format("{0:yyyy-MM-dd}", item));
            }
            ViewBag.listNgayNghi = listNgayNghi;
            return View(nn);
        }
        [HttpGet]
        public ActionResult SaveAddNgayNghi(InformationNgayNghi nn)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            NgayNghi newNN = new NgayNghi();
            var listNN = db.NgayNghi.ToList();
            //check ngay nghi cua Dien
            
            newNN.MaNgayNghi = nn.MaNgayNghi;
            newNN.MaNhanVien = nn.MaNhanVien;
            newNN.NgayNghiDate = nn.NgayNghiDate;
            newNN.LyDo = nn.LyDo;
            newNN.GhiChu = nn.GhiChu;
            ViewBag.id = newNN.MaNhanVien;
            db.NgayNghi.Add(newNN);
            db.SaveChanges();
            return Redirect($"/NgayNghi/InformationNgayNghi/{nn.MaNhanVien}");
        }
        [HttpGet]
        public ActionResult UpdateNgayNghi(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            //this id is MaNgayNghi
            ViewBag.id = id;
            var queryNgayNghi = db.NgayNghi.Where(n => n.MaNgayNghi == id).FirstOrDefault();
            ViewBag.MaNV = queryNgayNghi.MaNhanVien;
            return View(queryNgayNghi);
        }
        public ActionResult SaveUpdateNgayNghi(InformationNgayNghi nn)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            ViewBag.id = nn.MaNhanVien;
            var tmp = nn.MaNgayNghi;
            var nnSua = db.NgayNghi.Where(n => n.MaNgayNghi == tmp).FirstOrDefault();

            nnSua.NgayNghiDate = nn.NgayNghiDate;
            nnSua.LyDo = nn.LyDo;
            nnSua.GhiChu = nn.GhiChu;
            db.SaveChanges();
            ViewBag.id = nn.MaNhanVien;
            return Redirect($"/NgayNghi/InformationNgayNghi/{nn.MaNhanVien}");
        }

        public ActionResult RemoveNgayNghi(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var nn = db.NgayNghi.Where(n => n.MaNgayNghi == id).SingleOrDefault();
            var mnv = nn.MaNhanVien;
            db.NgayNghi.Remove(nn);
            db.SaveChanges();
            return Redirect($"/NgayNghi/InformationNgayNghi/{mnv}");
        }
        public ActionResult SearchNgayNghi(string name)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            name = name.ToLower();
            List<TongNgayNghi> listtnn = new List<TongNgayNghi>();
            //var nn = db.TongNgayNghi.ToList();
            //foreach(var item in nn)
            //{
            //    string hoten = item.HoTen.ToLower();
            //    if (hoten.Contains(name))
            //    {
            //        listtnn.Add(item);
            //    }
            //}
            listtnn = db.TongNgayNghi.Where(n => n.HoTen.Contains(name)).ToList();
            TempData["SearchResultsNhaVienByTNN"] = listtnn;
            return Redirect("/Home/QLNgayNghi");
        }
        public ActionResult SortByTongSoNgayNghi()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            List<TongNgayNghi> listSort = new List<TongNgayNghi>();
            var listtnn = db.TongNgayNghi.OrderBy(n => n.TongSoNgayNghi).ToList();
            TempData["SortResultsByTSNN"] = listtnn;
            return Redirect("/Home/QLNgayNghi");
        }
    }
}