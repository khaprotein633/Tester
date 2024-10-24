using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;
using PagedList;


namespace WebApiProject.Controllers
{
    public class HomeController : Controller

    {
        //Database1Entities1 db = new Database1Entities1();    
        Store_apiEntities db = new Store_apiEntities();
        public async Task<ActionResult> Index(int? page)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            if (Session["user"] != null)
            {
                var user = (NguoiDung)Session["user"];
                var cartItems = db.Shopping_Cart.Where(c => c.user_id == user.user_id);
                int totalQuantity = cartItems.Any() ? (int)cartItems.Sum(c => c.quantity) : 0;
                Session["TotalQuantity"] = totalQuantity;      
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            
            List<Shoes> list = (List<Shoes>) Session["ListProduct"];
            return View(list.ToPagedList(pageNumber,pageSize));
        }

        public async Task<ActionResult> GetProduct()
        {
            string baseurl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpclient = new HttpClient())
            {
                HttpResponseMessage res = await httpclient.GetAsync(baseurl + "api/Products/Getproduct");
                if(res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    Session["ListProduct"] = list;
                    return RedirectToAction("Index", "Home");
                }
            }

            return null;
        }


       

        public async Task<ActionResult> GetProductbyidcategory(int id_category)   
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproductbyid_category?id_category=" + id_category);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    Session["ListProduct"] = list;
                    return RedirectToAction("Index", "Home");
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Get_product_by_name(string search)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/SearchProductByName?productName=" + search);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    TempData["SuccessMessage"] = "Tìm thành công!";
                    Session["ListProduct"] = list;
                    return RedirectToAction("Index", "Shop");
                }
                TempData["ErrorMessage"] = "Không tìm được!";
                return RedirectToAction("Index", "Shop");
            }
        }



    }
}
