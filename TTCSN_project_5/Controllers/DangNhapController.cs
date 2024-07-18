using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTCSN_project_5.Models;

namespace TTCSN_project_5.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        Model1 db = new Model1();
        public static string TenDangNhap;
        [HttpGet]
        public ActionResult HomeDangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HomeDangNhap(FormCollection collection)
        {
            if (Session["HoTen"] != null)
                return RedirectToActionPermanent("Index", "Home");
            var tendn = collection["username"];
            var matkhau = collection["password"];
            bool ck = true;
            if(tendn == "")
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống!";
                ViewData["LoiTong"] = "";
                ck = false;
            }
            if(matkhau == "")
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống!";
                ViewData["LoiTong"] = "";
                ck = false;
            }
            QuanTri qt = db.QuanTri.SingleOrDefault(n => n.TenDangNhap == tendn && n.MatKhau == matkhau);
            if (qt != null)
            {
                if (Session["HoTen"] == null)
                    Session["HoTen"] = qt.HoTen;
                TenDangNhap = qt.TenDangNhap;
                return RedirectToActionPermanent("Index", "Home");
            }
            else if(ck)
            {
                ViewData["LoiTong"] = "Tài khoản hoặc mật khẩu không chính xác!";
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Abandon();
            return Redirect("HomeDangNhap");
        }

        public ActionResult ThongTinAdmin()
        {
            if (Session["HoTen"] == null)
                return RedirectToActionPermanent("HomeDangNhap", "DangNhap");
            var ad = db.QuanTri.Where(x => x.TenDangNhap == TenDangNhap).FirstOrDefault();
            ViewBag.gt = "Nam";
            if (ad.GioiTinh == 0)
                ViewBag.gt = "Nữ";
            return View(ad);
        }
    }
}