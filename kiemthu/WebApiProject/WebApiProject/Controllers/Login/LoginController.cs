using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiProject.Controllers.Login
{
    public class LoginController : Controller
    {
        // Database1Entities1 db = new Database1Entities1();
        Store_apiEntities db = new Store_apiEntities();
        // GET: Login
        public ActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> login(string email, string pass)
        {
            NguoiDung login = new NguoiDung
            {
                email = email,
                password = pass
            };
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Users/CheckLogin", login);

                if(res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    NguoiDung u = new NguoiDung();
                    u = res.Content.ReadAsAsync<NguoiDung>().Result;
                    if (u.role_id == "R2")
                    {
                        Session["user"] = u;
                        TempData["SuccessMessage"] = "Chào Mừng " + u.name;
                        return RedirectToAction("Index", "_HomeAdmin", new { area = "Admin" }); 
                    }

                    Session["user"] = u;
                    TempData["SuccessMessage"] = "Chào Mừng "+u.name;
                    return RedirectToAction("GetProduct", "Home");
                }
               
            }
            TempData["ErrorMessage"] = "Email hoặc mật khẩu không đúng!";
            return RedirectToAction("Index", "Login");
        }

        public ActionResult signout()
        {
            // Xóa Session user
            Session["user"] = null;

            // Chuyển hướng đến trang chính sau khi đăng xuất
            return RedirectToAction("GetProduct", "Home");
        }

        public ActionResult ForgotPassword()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            return View();
        }
        public ActionResult ResetPassword(string email)
        {
            var user = db.Users.FirstOrDefault(u => u.email == email);
            if (user != null)
            {
                 ViewBag.Email = email;
                return View("ForgotPassword");
            }
            else
            {
                 TempData["ErrorMessage"] = "Email ko tồn tại!";
                return RedirectToAction("ForgotPassword", "Login");
            }
        }
    }
}