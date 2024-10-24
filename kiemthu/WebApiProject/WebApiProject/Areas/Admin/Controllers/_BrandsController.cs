using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApiProject.Models;

namespace WebApiProject.Areas.Admin.Controllers
{
    public class _BrandsController : Controller
    {
        // GET: Admin/_Brands
        public async Task<ActionResult> Index(int? Page)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
               
            }
            if(TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            int pageSize = 10;
            int pageNumber = (Page ?? 1);

            List<ThuongHieu> list = await GetBrands();
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public async Task<List<ThuongHieu>> GetBrands()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Brands/Get_all_brands");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<ThuongHieu> list = new List<ThuongHieu>();
                    list = res.Content.ReadAsAsync<List<ThuongHieu>>().Result;
                    return list;
                }
                return null;
            }
        }

        public ActionResult CreateBrand()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostBrand(ThuongHieu newTH)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Brands/Post_brand", newTH);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã thêm thương hiệu!";
                    return RedirectToAction("Index", "_Brands");
                }
                TempData["ErrorMessage"] = "Không thêm được!";
                return RedirectToAction("CreateBrand", "_Brands");
            }

        }


        public async Task<ThuongHieu> GetBrandByID(int id_brand)
        {
            ThuongHieu brand = new ThuongHieu();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Brands/Get_brand_by_id?id_brand="+id_brand);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    brand = res.Content.ReadAsAsync<ThuongHieu>().Result;
                    return brand;
                }
                TempData["ErrorMessage"] = "Không tìm được!";
            }
            return brand;
        }

        public async Task<ActionResult> EditBrand(int id_brand)
        {
            ThuongHieu brand = await GetBrandByID(id_brand);
            
            return View(brand);
        }
        [HttpPost]
        public async Task<ActionResult> EditBrand(ThuongHieu brand)
        {
            if (string.IsNullOrEmpty(brand.brand_name))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return View(brand);
            }
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
               Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Brands/Put_brand",brand);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Cập nhật thương hiệu thành công!";
                    return RedirectToAction("Index", "_Brands");
                }
                TempData["ErrorMessage"] = "Không Cập nhật được!"; 
            }

            return View(brand);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteBrand(int brand_id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Brands/Delete_brand?id_brand=" + brand_id);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã xoá thêm thương hiệu!";
                    return RedirectToAction("Index", "_Brands");
                }
                TempData["ErrorMessage"] = "Không Xoá được!";
                return RedirectToAction("Index", "_Brands");
            }

        }



    }
}