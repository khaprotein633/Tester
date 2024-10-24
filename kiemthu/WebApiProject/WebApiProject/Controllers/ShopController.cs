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
    public class ShopController : Controller
    {
        // GET: product
       
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
            if (TempData["SearchString"] != null)
            {
                ViewBag.SearchString = TempData["SearchString"].ToString();
            }

            int pageSize = 12;
            int pageNumber = (page ?? 1);

            List<Shoes> list = (List<Shoes>)Session["ListProduct"];
            return View(list.ToPagedList(pageNumber, pageSize));
        }
           
        public async Task<ActionResult> GetProduct()    
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproduct");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                     List < Shoes > list = new  List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    Session["ListProduct"] = list;
                    return RedirectToAction("Index","Shop");

                }
                return null;
            }
        }
        public async Task<ActionResult> GetProductbyidcategory(int id_category)   
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproductbyid_category?id_category=" + id_category);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    Session["ListProduct"] = list;
                    return RedirectToAction("Index", "Shop");
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
                    TempData["SearchString"] = search;
                    return RedirectToAction("Index", "Shop");
                }
                TempData["ErrorMessage"] = "Không tìm được!";
                return RedirectToAction("Index", "Shop");
            }
        }

        public async Task<ActionResult> ProductDetail(string id_product)
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

            Shoes sp = await GetProductbyID(id_product);

            List<BinhLuanSP> list = await GetComments(id_product);
            sp.Comments = list;
            return View(sp);
        }
        public async Task<Shoes> GetProductbyID(string id_product)    
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproductbyid?id_product=" + id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Shoes sp = new Shoes();
                    sp = res.Content.ReadAsAsync<Shoes>().Result;
                    return sp;
                }
                return null;
            }
        }
        public async Task<List< BinhLuanSP>> GetComments(string id_product)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/ProductReviews/GetReviewByID?id_product=" + id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<BinhLuanSP> list = new List<BinhLuanSP>();
                    list = res.Content.ReadAsAsync<List<BinhLuanSP>>().Result;
                    return list;
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostComment(string binhluan, string id_product, int rating,DateTime reviewdate)
        {
            var user = (NguoiDung)Session["user"];
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Tạo mã đánh giá sản phẩm độc nhất
            string uniqueReviewId = $"PR{user.user_id}_{id_product}_{DateTime.Now:yyyyMMddHHmmss}";

            BinhLuanSP newreview = new BinhLuanSP
            {
                product_review_id = uniqueReviewId,
                user_id = user.user_id,
                comment = binhluan,
                review_date = DateTime.Now,
                product_id = id_product,
                rating = rating
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                             Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/ProductReviews/PostReview", newreview);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("ProductDetail", "Shop", new { id_product = id_product });
                }
                return RedirectToAction("Index", "Login");
            }
        }

    }
}