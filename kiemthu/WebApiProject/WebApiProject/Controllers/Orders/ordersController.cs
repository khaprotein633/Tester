using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers.Orders
{
    public class ordersController : Controller
    {
        
        Store_apiEntities db = new Store_apiEntities();

        // GET: orders
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
            List<DonHang> list = (List<DonHang>)Session["ListOrder"];

            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public async Task<ActionResult> Getorder(string user_id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Orders/Get_orders_by_ID_user?id_user=" + user_id);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<DonHang> list = new List<DonHang>();
                    list = res.Content.ReadAsAsync<List<DonHang>>().Result;
                    Session["ListOrder"] = list;
                    return RedirectToAction("Index", "orders");
                }
                return null;
            }
        }

        public string GenerateOrderId()
        {
            var maxOrderId = db.Orders.Max(p => p.orders_id);
            if (maxOrderId == null)
            {
                return "O0001";
            }
            int maxOderIdNumber = int.Parse(maxOrderId.Substring(1));
            string newOId = "O" + (maxOderIdNumber + 1).ToString().PadLeft(4, '0');
            return newOId;
        }
        
        [HttpPost]
        public async Task<ActionResult> Createorder(string shippingAddress, decimal totalAmount)
        {
            if (totalAmount == 40)
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn trống!";
                return RedirectToAction("Index", "Cart");
            }
            var user  = (NguoiDung)Session["user"]; ;
            DonHang neworder = new DonHang
            {
                orders_id = GenerateOrderId(),
                user_id = user.user_id,
                total_amount = totalAmount,
                orders_date = DateTime.Now,
                shipping_address = shippingAddress,
                user_phone = user.phonenumber,
                oderstatus_id = 1

            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Orders/PostOrder", neworder);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Createorderdetail", new { id_order = neworder.orders_id, id_user = user.user_id });
                    //TempData["SuccessMessage"] = "Đơn hàng đã được đặt!";
                    //return RedirectToAction("Index", "Cart");
                }
                TempData["ErrorMessage"] = "Đơn hàng không đặt được!";
                return RedirectToAction("Index", "Cart");

            }

        }

        public async Task<ActionResult> detail(string id_order)
        {
           
            var list = await GetDetail(id_order);

            return View(list);
        }
        public async Task<List<ChiTietDH>> GetDetail(string id_order)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Detail/GetdetailByIDOrder?id_order=" +  id_order);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<ChiTietDH> list = new List<ChiTietDH>();
                    list = res.Content.ReadAsAsync<List<ChiTietDH>>().Result;
                    return list;
                }
                return null;
            }
        }

        public string GenerateOrderDetailId()
        {
            var maxOrderDetailId = db.Order_Details.Max(p => p.order_details_id);
            if (maxOrderDetailId == null)
            {
                return "OD00001";
            }

            if (maxOrderDetailId.Length < 3)
            {
                return "OD" + (int.Parse(maxOrderDetailId) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                int maxODid = int.Parse(maxOrderDetailId.Substring(2));
                string newODId = "OD" + (maxODid + 1).ToString().PadLeft(5, '0');
                return newODId;
            }
        }

        public async Task<Boolean> UpdateWarehouseByID(int id_warehouse, int quantity)
        {
            Soluongton slt = new Soluongton
            {
                product_size_quantity_id = id_warehouse,
                quantity = quantity
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";   // Lấy base uri của website
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Warehouse/Put_size_quantity", slt);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<ActionResult> Createorderdetail(string id_user, string id_order)
        {
            List<Shopping_Cart> cartItems = db.Shopping_Cart.Where(c => c.user_id == id_user).ToList();
           
                foreach (var item in cartItems)
                {
                    ChiTietDH newOD = new ChiTietDH
                    {
                        order_details_id = GenerateOrderDetailId(),
                        order_id = id_order,
                        product_id = item.product_id,
                        size= item.size,
                        quantity = item.quantity,
                        price_oder = item.Product.price
                    };
                var up = db.Product_Size_Quantity.FirstOrDefault(c => c.product_id == newOD.product_id && c.size == newOD.size);
                    var CheckUpdateWarehouse = await UpdateWarehouseByID((int)up.product_size_quantity_id, (int)newOD.quantity);
                    if(CheckUpdateWarehouse != true)
                    {
                        TempData["ErrorMessage"] = "Sản phẩm " + item.product_id+"size "+item.size+" hiện hết hàng hoặc không đủ số lượng để cung cấp.\n";
                        return RedirectToAction("Index", "Cart");

                    }

                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    using (var httpClient = new HttpClient())
                    {
                        HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Detail/PostDetail", newOD);
                        if (res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            continue;
                        }
                        else
                        {
                        TempData["ErrorMessage"] = "Loi tao chi  tiet don hang!";
                            return RedirectToAction("Index", "Cart");
                        }
                    }
                }
                TempData["SuccessMessage"] = "Đơn hàng đã được đặt!";
                return RedirectToAction("Index", "Cart");
            
            
        }

        [HttpPost]
        public async Task<ActionResult> CancelOrder(string id_order, string id_user)
        {
            DonHang order = new DonHang
            {
                orders_id = id_order,
                oderstatus_id = 5
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Orders/PutOrder", order);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    TempData["SuccessMessage"] = "Huỷ đơn hàng thành công";
                    return RedirectToAction("Getorder", "orders", new { user_id = id_user });
                }
                else
                {

                    TempData["ErrorMessage"] = "Lỗi khi huỷ đơn hàng!";
                    return RedirectToAction("Getorder", "orders", new { user_id = id_user });
                }
            }

        }

    }
}
    