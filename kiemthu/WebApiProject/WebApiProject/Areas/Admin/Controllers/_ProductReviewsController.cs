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
    public class _ProductReviewsController : Controller
    {
        // GET: Admin/ProductReviews
        public async Task <ActionResult> Index(int? page)
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

            
                List<BinhLuanSP> list = (List<BinhLuanSP>)Session["ListReview"]; 
                return View(list.ToPagedList(pageNumber, pageSize));
           

        }

        public async Task<ActionResult> GetAllReview()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/ProductReviews/GetAllReview");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<BinhLuanSP> list = new List<BinhLuanSP>();
                    list = res.Content.ReadAsAsync<List<BinhLuanSP>>().Result;
                    Session["ListReview"] = list;
                    return RedirectToAction("Index", "_ProductReviews");
                }
                return null;
            }
 
        }

        public async Task<ActionResult> GetReviewByIDProduct(string id_product)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/ProductReviews/GetReviewByID?id_product="+id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<BinhLuanSP> list = new List<BinhLuanSP>();
                    list = res.Content.ReadAsAsync<List<BinhLuanSP>>().Result;
                    Session["ListReview"] = list;
                    return RedirectToAction("Index", "_ProductReviews");
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteReview(string id_review)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
           Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/ProductReviews/DeleteReview?id_review=" + id_review);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Xoá bình luận thành công!";
                    return RedirectToAction("GetAllReview", "_ProductReviews");
                }
                TempData["ErrorMessage"] = "Lỗi không xoá được!";
                return RedirectToAction("GetAllReview", "_ProductReviews");
            }
        }
    }
}