using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BHMTOnline.Models;
using System.Web.Mvc;

namespace BHMTOnline.Controllers
{
    public class UserController : Controller
    {
        BHMTModel user = new BHMTModel();
        // GET: User
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(NguoiDung nguoiDung)
        {
            try
            {
                if(nguoiDung.Email != "Admin@gmail.com")
                {
                    nguoiDung.IDQuyen = 2;
                }
                else
                {
                    nguoiDung.IDQuyen = 1;
                } 
                    
                // Thêm người dùng  mới
                user.NguoiDungs.Add(nguoiDung);
                // Lưu lại vào cơ sở dữ liệu
                user.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    // Nếu dữ liệu đúng thì gọi đến trang đăng nhập
                    return RedirectToAction("DangNhap"); 
                }
                return View("DangKy");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection userLogin)
        {
            string userEmail = userLogin["userEmail"].ToString();
            string passWord = userLogin["passWord"].ToString();
            var isLogin = user.NguoiDungs.SingleOrDefault(x => x.Email.Equals(userEmail) && x.MatKhau.Equals(passWord));

            if (isLogin != null)
            {
                if (userEmail == "Admin@gmail.com")
                {
                    Session["use"] = isLogin;
                    return RedirectToAction("Index", "Admin/Home");
                }
                else
                {
                    Session["use"] = isLogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View("DangNhap");
            }

        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

    }
}