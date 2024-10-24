using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers.Login
{
    public class accountController : Controller
    {
        // GET: account
        Store_apiEntities db = new Store_apiEntities();
       
        public ActionResult Info()
        {
             NguoiDung Khachhang = (NguoiDung)Session["user"];
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            if (Khachhang == null)
            {   
                return RedirectToAction("Index", "Login"); 
            }

            return View(Khachhang);
        }

        [HttpPost]
        public async Task<ActionResult> Info(NguoiDung upuser)
        {
           
            var olduser = (NguoiDung)Session["user"];
            if (string.IsNullOrEmpty(upuser.name) ||
                string.IsNullOrEmpty(upuser.phonenumber) ||
                string.IsNullOrEmpty(upuser.address) ||
                string.IsNullOrEmpty(upuser.email) ||
                string.IsNullOrEmpty(upuser.password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin."); ;
                return View(upuser);
            }
            if (upuser.email != olduser.email)
            {
                if (db.Users.Any(u => u.email == upuser.email))
                {

                    ModelState.AddModelError("email", "Email đã được sử dụng.");
                    return View(upuser);
                }
            }
            if (upuser.phonenumber != olduser.phonenumber)
            {
                if (db.Users.Any(u => u.phonenumber == upuser.phonenumber))
                {
                    ModelState.AddModelError("phonenumber", "Số điện thoại đã được sử dụng.");
                    return View(upuser);
                }
            }
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                           Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Users/Putuser", upuser);

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        NguoiDung u = new NguoiDung();
                        u= res.Content.ReadAsAsync<NguoiDung>().Result;
                        Session["user"] = u;
                        TempData["SuccessMessage"] = "Lưu thành công";
                        return RedirectToAction("GetProduct", "Home");
                    }
                        ModelState.AddModelError("", "Lỗi không lưu được");
                        return View(upuser);      
            }
        }

             [HttpPost]
            public async Task<ActionResult> ChangePassword(string email,string password) {

                    NguoiDung upPass = new NguoiDung { 
                        email= email,
                        password = password
                    };
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Users/Putpassword", upPass);

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                    return RedirectToAction("Index","Login");
                }
                TempData["ErrorMessage"] = "Không đổi mật khẩu được!";
                return RedirectToAction("ForgotPassword", "Login");
            }
        }
    }
}