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
    public class cartController : Controller
    {
        // GET: cart
     
        Store_apiEntities db = new Store_apiEntities();
       

        public async Task<ActionResult> Index()
        {
            var user = (NguoiDung)Session["user"];
            if (user != null)
            {
                string id_user = user.user_id;
                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                }
                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                }
                var list = await Getcart(id_user);
                if (list != null)
                {
                    return View(list);
                }
            }
            else
            {
                ViewBag.TotalQuantity = 0;
            }
            return RedirectToAction("Index", "Login");    
        }

        public async Task<List<GioHang>> Getcart(string id_user)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Cart/Getcartbyiduser?id_user=" + id_user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<GioHang> listcart = new List<GioHang>();
                    listcart = res.Content.ReadAsAsync<List<GioHang>>().Result;
                    return listcart;
                }
                return null;
            }
        }

            public string GenerateCartId()
            {
                if (!db.Shopping_Cart.Any()) { return "C0001";}
                var maxCartId = db.Shopping_Cart.Max(p => p.cart_id);
                int maxCartIDnumber = int.Parse(maxCartId.Substring(1));
                string newCId = "C" + (maxCartIDnumber + 1).ToString().PadLeft(4, '0');
                return newCId;
            }
       

        [HttpPost]
        public async Task<ActionResult> Addtocart(string id_product, string size, int soluong, decimal price)
        { 
            var user = (NguoiDung)Session["user"];
            if (user != null)
            {
                var sotonkho = db.Product_Size_Quantity.FirstOrDefault(c => c.product_id == id_product && c.size==size);
                if(sotonkho == null)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
                    return RedirectToAction("ProductDetail", "Shop", new { id_product = id_product });
                }
                if(sotonkho.quantity < soluong)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không đủ số lượng!";
                    return RedirectToAction("ProductDetail", "Shop",new { id_product=id_product});
                }
                 GioHang cart = new GioHang
                 {
                    cart_id = GenerateCartId(),
                    product_id = id_product,
                    size = size,
                    quantity = soluong,
                    user_id = user.user_id,
                    price = price
                    
                };
                
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage res = await httpClient.PostAsJsonAsync(baseUrl + "api/Cart/PostCart", cart );
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng!";
                        return RedirectToAction("ProductDetail", "Shop",new { id_product  = id_product});
                    }
                    TempData["ErrorMessage"] = "Không thêm sản phẩm được!";
                    return RedirectToAction("ProductDetail", "Shop", new { id_product = id_product });
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCart(string cartId, int quantity)
        {
            var user = (NguoiDung)Session["user"];
            if (quantity < 1)
            {
                TempData["ErrorMessage"] = "Lỗi cập nhật số lượng!";
                return RedirectToAction("Index", "Cart");
            }            
            GioHang updateRequest = new GioHang
            {
                cart_id = cartId,
                quantity = quantity  
            };

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                             Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.PutAsJsonAsync(baseUrl + "api/Cart/PutCart", updateRequest); 
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã cập nhật giỏ hàng!";
                    return RedirectToAction("Index", "Cart");
                }
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật giỏ hàng!";
                    return RedirectToAction("Index", "Cart");   
            }
        }



        public async Task<ActionResult> DeleteCart(string cartid)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.DeleteAsync(baseUrl + "api/Cart/Deletecart?id_cart=" + cartid);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["SuccessMessage"] = "Đã bỏ sản phẩm khỏi giỏ hàng!";
                    return RedirectToAction("Index", "Cart");
                }
                
                    TempData["ErrorMessage"] = "Có lỗi xảy ra!";
               
                return RedirectToAction("Index", "Cart");
            }
        }

    }


}