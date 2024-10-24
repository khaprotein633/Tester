using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Areas.Admin.Controllers
{
    public class _UsersController : Controller
    {
        // GET: Admin/_Users
        Store_apiEntities db = new Store_apiEntities();
        public async Task<ActionResult> Index(int? page)
        {
            if (TempData["SuccessMessage"] != null  )
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                
            }
            if(TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<NguoiDung> list = (List<NguoiDung>)Session["ListKH"];
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> GetAllUser()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Users/Get_list_user");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<NguoiDung> list = new List<NguoiDung>();
                    list = res.Content.ReadAsAsync<List<NguoiDung>>().Result;
                    Session["ListKH"] = list;
                    return RedirectToAction("Index", "_Users");
                }
                return null;
            }
        }

        public async Task<ActionResult> GetUserByName(string name)
        {

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Users/GetALLByName?name=" + name);

                if(res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<NguoiDung> list = new List<NguoiDung>();
                    list = res.Content.ReadAsAsync<List<NguoiDung>>().Result;
                    Session["ListKH"] = list;
                    return RedirectToAction("Index","_Users");
                }
                TempData["ErrorMessage"] = "Không tìm được!";
                return RedirectToAction("GetAllUser", "_Users");
            }
        }

        public async Task<ActionResult> Get_user_by_id(string id_user)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Users/Get_user_by_id?id_user=" + id_user);
                if(res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<NguoiDung> list = new List<NguoiDung>();
                    list = res.Content.ReadAsAsync<List<NguoiDung>>().Result;
                    Session["ListKH"] = list;
                    return RedirectToAction("Index","_Users");
                }
                TempData["ErrorMessage"] = "Không tìm được!";
                return RedirectToAction("GetAllUser", "_Users");
            }
        }

        public async Task<NguoiDung> GetUserByID(string id_user)
        {
            NguoiDung user = new NguoiDung();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Users/GetUserById?id_user=" + id_user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    user = res.Content.ReadAsAsync<NguoiDung>().Result;
                    return user;
                }
                TempData["ErrorMessage"] = "Không tìm được!";
                return user;
            }
        }

        public ActionResult CreateUser()
        {
           
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
        public async Task<ActionResult> CreateUser(NguoiDung user)
        {
            if(string.IsNullOrEmpty(user.name)||
                string.IsNullOrEmpty(user.phonenumber)|| 
                string.IsNullOrEmpty(user.address)|| 
                string.IsNullOrEmpty(user.email)||
                string.IsNullOrEmpty(user.password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin."); ;
                return View(user);
            }

            if (db.Users.Any(u => u.email == user.email))
            {
                ModelState.AddModelError("email", "Email đã được sử dụng.");
                return View( user);
            }
            if (db.Users.Any(u => u.phonenumber == user.phonenumber))
            {
                ModelState.AddModelError("phonenumber", "Số điện thoại đã được sử dụng.");
                return View (user);
            }
            user.user_id = GenerateUserId();

            using (var httpClient = new HttpClient())
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                 Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
                var res = await httpClient.PostAsJsonAsync(baseUrl + "api/Users/Postuser", user);
                if (res.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Tạo User thành công";
                    return RedirectToAction("GetAllUser", "_Users");
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tạo được!";
                }
            }
            return View(user);
        }


        public async Task<ActionResult> EditUser(string id_user)
        {
            NguoiDung user = await GetUserByID(id_user);
            Session["EditUser"] = user;

            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> EditUser(NguoiDung user)
        {
            var olduser = (NguoiDung)Session["EditUser"];
            if (string.IsNullOrEmpty(user.name) ||
                string.IsNullOrEmpty(user.phonenumber) ||
                string.IsNullOrEmpty(user.address) ||
                string.IsNullOrEmpty(user.email) ||
                string.IsNullOrEmpty(user.password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin."); ;
                return View(user);
            }
            if(user.email != olduser.email) {
                if (db.Users.Any(u => u.email == user.email))
                {

                    ModelState.AddModelError("email", "Email đã được sử dụng.");
                    return View(user);
                }
            }
            if (user.phonenumber != olduser.phonenumber)
            {
                if (db.Users.Any(u => u.phonenumber == user.phonenumber))
                {
                    ModelState.AddModelError("phonenumber", "Số điện thoại đã được sử dụng.");
                    return View(user);
                }
            }
            
                
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Users/Putuser", user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Cập nhật user thành công!";
                    return RedirectToAction("GetAllUser", "_Users");
                }
                ModelState.AddModelError("", "Không cập nhật được."); 
            }
            return View(user);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id_user)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Users/DeleteUser?id_user=" + id_user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Xóa người dùng thành công!";
                    return RedirectToAction("GetAllUser", "_Users");
                }
                TempData["ErrorMessage"] = "Không xóa được!";
                return RedirectToAction("GetAllUser", "_Users");
            }

        }


    }
}