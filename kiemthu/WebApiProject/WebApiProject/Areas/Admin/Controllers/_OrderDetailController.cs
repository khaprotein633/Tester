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
    public class _OrderDetailController : Controller
    {
        // GET: Admin/_OrderDetail
        public async Task<ActionResult> Index(int? Page)
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
            int pageNumber = (Page ?? 1);

            List<ChiTietDH> list = (List<ChiTietDH>)Session["ListOrderdetail"];
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> GetDetailByID_Order(string id_order)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Detail/GetdetailByIDOrder?id_order=" + id_order);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<ChiTietDH> list = new List<ChiTietDH>();
                    list = res.Content.ReadAsAsync<List<ChiTietDH>>().Result;
                    Session["ListOrderdetail"] = list;
                    return RedirectToAction("Index", "_OrderDetail");
                }
                return null;
            }

        }
    }
}