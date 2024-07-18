using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCSN_project_5.Models;

namespace TTCSN_project_5.Controllers
{
    public class PhongBanController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPhongBan()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            ViewBag.dsmpb = db.PhongBan.Select(pb => pb.MaPhongBan).ToList();
            ViewBag.dsTenPhongBan = db.PhongBan.Select(pb => pb.TenPhongBan).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult UpdatePhongBan(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var curentPhongBan = db.PhongBan.Where(n => n.MaPhongBan == id).FirstOrDefault();
            ViewBag.listpb = db.PhongBan.ToList();
            ViewBag.lg = db.PhongBan.ToList().Count;
            ViewBag.dsTenPhongBan = db.PhongBan.Select(pb => pb.TenPhongBan).ToList();
            ViewBag.curentTenPhongBan = curentPhongBan.TenPhongBan;
            return View(curentPhongBan);
        }

        public ActionResult RemovePhongBan(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var currentNvOfPb = db.NhanVien.Where(n => n.MaPhongBan == id).ToList();

            NhanVienController nvc = new NhanVienController();
            var tp2 = currentNvOfPb.Count();
            //thuc hien xoa tung nhan vien

            for (int i = 0; i < tp2; i++)
            {
                var tmp = db.NhanVien.Where(n => n.MaPhongBan == id).FirstOrDefault();
                var tnn = db.TongNgayNghi.Where(n => n.MaNhanVien == tmp.MaNhanVien).FirstOrDefault();
                var listtnn = db.NgayNghi.Where(n => n.MaNhanVien == tmp.MaNhanVien).ToList();
                var cntListTNN = listtnn.Count();
                for (int j = 0; j < cntListTNN; j++)
                {
                    var tmp2 = db.NgayNghi.Where(n => n.MaNhanVien == tmp.MaNhanVien).FirstOrDefault();
                    db.NgayNghi.Remove(tmp2);
                    db.SaveChanges();
                }
                db.TongNgayNghi.Remove(tnn);
                var listcnl = db.CapNhatLuong.Where(n => n.MaNhanVien == tmp.MaNhanVien).ToList();
                var cntListCNL = listcnl.Count();
                for (int j = 0; j < cntListCNL; j++)
                {
                    var tmp2 = db.CapNhatLuong.Where(n => n.MaNhanVien == tmp.MaNhanVien).FirstOrDefault();
                    db.CapNhatLuong.Remove(tmp2);
                    db.SaveChanges();

                }
                var sal = db.Luong.Where(n => n.MaNhanVien == tmp.MaNhanVien).FirstOrDefault();
                db.Luong.Remove(sal);
                db.NhanVien.Remove(db.NhanVien.Where(n => n.MaPhongBan == id).FirstOrDefault());
                db.SaveChanges();
            }


            var currentPb = db.PhongBan.Where(n => n.MaPhongBan == id).FirstOrDefault();
            db.PhongBan.Remove(currentPb);
            db.SaveChanges();
            return Redirect("/Home/QLPhongBan");
        }

        [HttpGet]
        public ActionResult ListNhanVienOfPhongBan(string id)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var listnvofpb = db.NhanVien.Where(n => n.MaPhongBan == id).ToList();
            ViewBag.lg = listnvofpb.Count;
            var tmp = db.PhongBan.Where(n => n.MaPhongBan == id).FirstOrDefault();
            ViewBag.tenphong = tmp.TenPhongBan;
            ViewBag.listcv = db.ChucVu.ToList();
            return View(listnvofpb);
        }
        public ActionResult SaveUpDatePhongBan(InformationPhongBan pb)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            string id = pb.MaPhongBan;
            var currentPb = db.PhongBan.Where(n => n.MaPhongBan == id).FirstOrDefault();
            currentPb.TenPhongBan = pb.TenPhongBan;
            currentPb.DiaChi = pb.DiaChi;
            currentPb.sdtLienHe = pb.sdtLienHe;
            db.SaveChanges();
            return Redirect("/Home/QLPhongBan");
        }
        public ActionResult SavePhongBan(InformationPhongBan pb)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var listpb = db.PhongBan.ToList();
            PhongBan newpb = new PhongBan();
            string idpb = pb.MaPhongBan;
            foreach (var x in listpb)
            {
                if (x.MaPhongBan == idpb)
                {
                    ViewBag.Messeage = "Phòng ban đã tồn tại!";
                    //return View(listpb);
                }
            }
            newpb.MaPhongBan = pb.MaPhongBan;
            newpb.TenPhongBan = pb.TenPhongBan;
            newpb.DiaChi = pb.DiaChi;
            newpb.sdtLienHe = pb.sdtLienHe;
            newpb.DateCreate = DateTime.Now;
            db.PhongBan.Add(newpb);
            db.SaveChanges();
            return Redirect("/Home/QLPhongBan");
        }

        public ActionResult SearchPhongBan(string searchpb)
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var listpb = db.PhongBan.ToList();
            List<PhongBan> pb = new List<PhongBan>();
            pb = db.PhongBan.Where(n => n.TenPhongBan.Contains(searchpb)).ToList();
            TempData["SearchResultsPhongBan"] = pb;
            return Redirect("/Home/QLPhongBan");
        }
    }
}