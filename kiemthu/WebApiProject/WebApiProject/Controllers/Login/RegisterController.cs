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
    public class RegisterController : Controller
    {
        // Database1Entities1 db = new Database1Entities1();
        Store_apiEntities db = new Store_apiEntities();
        // GET: Register
        public ActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            return View();
        }
        

        public string GenerateUserId()
            {
            var maxUserID = db.Users.Max(c => c.user_id);
            if (maxUserID == null)
            {
                return "U001";
            }
            int maxidNumber = int.Parse(maxUserID.Substring(1));
            string newIdUser = "U" + (maxidNumber + 1).ToString().PadLeft(3, '0');
            return newIdUser;
        }
      
        [HttpPost]
        public async Task<ActionResult> Dangky(DangKy accountInfo)   
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.email == accountInfo.email))
                {
                    ModelState.AddModelError("email", "Email đã được sử dụng.");
                    return View("Index", accountInfo);
                }
                if (db.Users.Any(u => u.phonenumber == accountInfo.soDienThoai))
                {
                    ModelState.AddModelError("email", "Số điện thoại đã được sử dụng.");
                    return View("Index", accountInfo);
                }
                  
               NguoiDung newUser = new NguoiDung
                {
                    user_id = GenerateUserId(),
                    role_id = "R1",
                    name = accountInfo.hoten,
                    email = accountInfo.email,
                    phonenumber = accountInfo.soDienThoai,
                    password = accountInfo.password,
                    address = accountInfo.diachi  
                };

                using (var httpClient = new HttpClient())
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                     Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
                    var res = await httpClient.PostAsJsonAsync(baseUrl + "api/Users/Postuser", newUser);
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Đăng ký thành công hãy tiếp tục thực hiện đăng nhập";
                        return RedirectToAction("Index", "Register");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi khi tạo tài khoản");
                    }
                }
            }
            return View("Index", accountInfo);
        }


    }
}