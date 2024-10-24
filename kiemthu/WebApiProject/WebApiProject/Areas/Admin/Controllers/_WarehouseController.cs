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
    public class _WarehouseController : Controller
    {
        // GET: Admin/_Warehouse
        public ActionResult Index(int? page)
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
            
            
                List<Soluongton> list = (List<Soluongton>)Session["WarehouseList"] ; // Lấy danh sách sản phẩm từ TempData
                return View(list.ToPagedList(pageNumber, pageSize));
            
            
        }

        public async Task<ActionResult> GetallWarehouse()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Warehouse/Get_list_products_in_warehouse");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Soluongton> list = new List<Soluongton>();
                    list = res.Content.ReadAsAsync<List<Soluongton>>().Result;
                    Session["WarehouseList"] = list;
                    return RedirectToAction("Index", "_Warehouse");
                }
                return null;
            }
        }

        public async Task<ActionResult> GetWarehouseByProductID(string id_product)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Warehouse/Get_list_products_in_warehouse_by_id?id_product="+id_product);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Soluongton> list = new List<Soluongton>();
                    list = res.Content.ReadAsAsync<List<Soluongton>>().Result;
                    Session["WarehouseList"] = list;
                    return RedirectToAction("Index", "_Warehouse");
                }
                return null;
            }
        }


        



        public ActionResult CreateWarehouse()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostWarehouse(Soluongton newSLT)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Warehouse/Post_products_size_quantity", newSLT);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã thêm kích thước mới!";
                    return RedirectToAction("GetallWarehouse", "_Warehouse");
                }
                TempData["ErrorMessage"] = "Không thêm được!";
                return RedirectToAction("CreateBrand", "_Warehouse");
            }

        }

       
        public async Task<ActionResult> EditWarehouseByID(int id_warehouse,string id_product, string size, int quantity)
        {
            if (quantity < 0 || quantity == null)
            {
                TempData["ErrorMessage"] = "Lỗi cập nhật!";
                return RedirectToAction("GetallWarehouse", "_Warehouse");
            }

            Soluongton slt = new Soluongton {
                product_size_quantity_id = id_warehouse,
                product_id = id_product,
                size = size,
                quantity = quantity
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Warehouse/Put_products_size_quantity", slt);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                    return RedirectToAction("GetallWarehouse", "_Warehouse");
                }
                TempData["ErrorMessage"] = "Không cập nhật được!";
                return RedirectToAction("GetallWarehouse", "_Warehouse");
            }
        }




        [HttpPost]
        public async Task<ActionResult> DeleteWarehouseByID(int id_warehouse)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Warehouse/Delete_products_size_quantity?id_product_size=" + id_warehouse);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    TempData["SuccessMessage"] = "Đã xoá kho hàng!";
                   
                    return RedirectToAction("GetallWarehouse", "_Warehouse");
                }
                TempData["ErrorMessage"] = "Không Xoá được!";
                return RedirectToAction("GetallWarehouse", "_Warehouse");
            }
        }

    }
}