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
    public class _OrdersController : Controller
    {
        // GET: Admin/_Orders
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
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<DonHang> list = (List<DonHang>)Session["ListOrder"];
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // các action lấy danh sách theo yêu cầu
        public async Task<ActionResult> GetAllOrders()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Orders/Get_all_orders");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DonHang> list = new List<DonHang>();
                    list = res.Content.ReadAsAsync<List<DonHang>>().Result;
                    Session["ListOrder"] = list;
                    return RedirectToAction("Index", "_Orders");
                }
                return null;
            }
        }
        public async Task<ActionResult> GetAllOrdersByIdUser(string id_user)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Orders/Get_orders_by_ID_user?id_user="+id_user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DonHang> list = new List<DonHang>();
                    list = res.Content.ReadAsAsync<List<DonHang>>().Result;
                    Session["ListOrder"] = list;
                    return RedirectToAction("Index", "_Orders");
                }
                return null;
            }
        }
        public async Task<ActionResult> GetAllOrdersByIdStatus(int id_status)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Orders/Get_orders_by_Status?id_status=" + id_status);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DonHang> list = new List<DonHang>();
                    list = res.Content.ReadAsAsync<List<DonHang>>().Result;
                    Session["ListOrder"] = list;
                    return RedirectToAction("Index", "_Orders");
                }
                return null;
            }
        }

        // cập nhật trạng thái đơn hàng
        public async Task<ActionResult> Edit_Order(string id_order)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            DonHang dh = await GetOrdersById(id_order);

            return View(dh);
        }
        public async Task<DonHang> GetOrdersById(string id_order)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Orders/GetOrderById?id_order=" + id_order);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DonHang dh = new DonHang();
                    dh = res.Content.ReadAsAsync<DonHang>().Result;
                    return dh;
                }
                return null;
            }
        }
        public async Task<ActionResult> PutOrder(DonHang order)
        {
            if(order.oderstatus_id == 4)
            {
                order.delivery_date = DateTime.Now;
            }
           
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Orders/PutOrder",order);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                    return RedirectToAction("GetAllOrders", "_Orders");
                }
                TempData["ErrorMessage"] = "Cập nhật lỗi !";
                return RedirectToAction("Edit_Order", "_Orders", new { id_order = order.orders_id });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOrder(string id_order)
        {
            try
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Orders/DeleteOrder?id_order=" + id_order);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMessage"] = "Xóa đơn hàng thành công!";
                        return RedirectToAction("GetAllOrders", "_Orders");
                    }
                    TempData["ErrorMessage"] = "Xóa bị lỗi !";
                    return RedirectToAction("GetAllOrders", "_Orders");
                }

            }catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetAllOrders", "_Orders");
            }
           
        }

    }
}