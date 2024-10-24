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
    public class _ProductsController : Controller
    {
        Store_apiEntities db = new Store_apiEntities();
        // GET: Admin/_Products
        public ActionResult Index(int? page )
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
          
            if (Session["ProductList"]!= null)
            {
                List<Shoes> list = (List<Shoes>)Session["ProductList"]; 
                return View(list.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.ErrorMessage = "Không có danh sách sản phẩm để hiển thị.";
                return View("ErrorView");
            }
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
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    Session["ProductList"] = list;
                    return RedirectToAction("Index","_Products");
                }
                return null;
            }
        }
        public async Task<ActionResult> GetProductbyidcategory(int category_id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/Getproductbyid_category?id_category=" + category_id);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    Session["ProductList"] = list;
                    return RedirectToAction("Index", "_Products");
                }
                return RedirectToAction("GetProduct", "_Products");
            }
        }


        
        public async Task<ActionResult> ProductDetail(string id_product)
        {
            Shoes sp = await GetProductbyID(id_product);

            return View(sp);
        }
      
        public async Task<Shoes> GetProductbyID(string id_product)  
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
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

       
        [HttpPost]
        public async Task<ActionResult> Get_product_by_name(string search)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Products/SearchProductByName?productName=" + search);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Shoes> list = new List<Shoes>();
                    list = res.Content.ReadAsAsync<List<Shoes>>().Result;
                    TempData["SuccessMessage"] = "Tìm thành công!";
                    Session["ProductList"] = list;
                    return RedirectToAction("Index", "_Products");
                }
                TempData["ErrorMessage"] = "Không tìm được!";
                return RedirectToAction("GetProduct", "_Products");
            }
        }

        
        public ActionResult Create_product()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product newP)
        {
            if (ModelState.IsValid) { 
                if (newP.category_id == null)
                {
                    ModelState.AddModelError("category_id", "Vui lòng chọn phân loại.");
                }
                if (newP.brand_id == null)
                {
                    ModelState.AddModelError("brand_id", "Vui lòng chọn thương hiệu.");
                    
                   
                }
                if (string.IsNullOrEmpty(newP.product_name))
                {
                    ModelState.AddModelError("product_name", "Vui lòng nhập tên sản phẩm.");
                  
                }
               
                if (newP.price == null||newP.price<1)
                {
                    ModelState.AddModelError("price", "Vui lòng nhập giá tiền và giá tiền phải lớn hơn 0");
                    
                }
                
                if (string.IsNullOrEmpty(newP.productimage_url))
                {
                    ModelState.AddModelError("productimage_url", "Vui lòng nhập Url hình ảnh sản phẩm.");
                   
                }

                if (string.IsNullOrEmpty(newP.description))
                {
                    ModelState.AddModelError("description", "Vui lòng nhập description.");
                   
                }
                if (string.IsNullOrEmpty(newP.detail))
                {
                    ModelState.AddModelError("detail", "Vui lòng nhập detail.");
                   
                }

                var same_name = db.Products.FirstOrDefault(c => c.product_name == newP.product_name);
                if(same_name != null)
                {
                    ModelState.AddModelError("product_name", "Tên sản phẩm đã tồn tại.");
                   
                }

                if (!ModelState.IsValid)
                {
                    return View("Create_product");
                }
                
                var maxProductId = db.Products.Max(p => p.product_id);
                int maxProductIdNumber = int.Parse(maxProductId.Substring(1));
                string newProductId = "S" + (maxProductIdNumber + 1).ToString().PadLeft(4, '0');
                newP.product_id = newProductId;

                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Products/PostProduct", newP);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
                        return RedirectToAction("GetProduct", "_Products");
                    }
                    TempData["ErrorMessage"] = "Không thêm được!";
                    return RedirectToAction("Create_product", "_Products");
                }
            }
            return View("Create_product");

        }


        
        public async Task<ActionResult> Edit_product(string id_product)
        {

            Shoes product = await GetProductbyID(id_product);

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> PutProduct(Product newP)
        {
            if (ModelState.IsValid)
            {
                if (newP.category_id == null)
                {
                    ModelState.AddModelError("category_id", "Vui lòng chọn phân loại.");
                }
                if (newP.brand_id == null)
                {
                    ModelState.AddModelError("brand_id", "Vui lòng chọn thương hiệu.");


                }
                if (string.IsNullOrEmpty(newP.product_name))
                {
                    ModelState.AddModelError("product_name", "Vui lòng nhập tên sản phẩm.");

                }
                if (newP.price == null || newP.price < 1)
                {
                    ModelState.AddModelError("price", "Vui lòng nhập giá tiền và giá tiền phải lớn hơn 0");

                }
                if (string.IsNullOrEmpty(newP.productimage_url))
                {
                    ModelState.AddModelError("productimage_url", "Vui lòng nhập Url hình ảnh sản phẩm.");

                }

                if (string.IsNullOrEmpty(newP.description))
                {
                    ModelState.AddModelError("description", "Vui lòng nhập description.");

                }
                if (string.IsNullOrEmpty(newP.detail))
                {
                    ModelState.AddModelError("detail", "Vui lòng nhập detail.");

                }
                if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit_product", "_Products", new { id_product = newP.product_id });

            }

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                   Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Products/PutProduct", newP);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction("GetProduct", "_Products");
                }
                TempData["ErrorMessage"] = "Cập nhật lỗi được!";
                return RedirectToAction("Edit_product", "_Products", new { id_product = newP.product_id });
            }
            }
           
            return RedirectToAction("Edit_product", "_Products", new { id_product = newP.product_id });
        }
         
        [HttpPost]
        public async Task<ActionResult> DeleteProduct(string id_product)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Products/DeleteProduct?id_product=" + id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã xoá thành công!";
                    return RedirectToAction("Index", "_Products");
                }
                TempData["ErrorMessage"] = "Không Xoá được !";
                return RedirectToAction("Index", "_Products");
            }
        }
    }
}