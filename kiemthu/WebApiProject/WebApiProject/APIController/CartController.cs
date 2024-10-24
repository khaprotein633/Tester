using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;


namespace WebApiProject.APIController
{
    public class CartController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();
        [HttpGet]
        [Route("api/Cart/Getcartbyiduser")]
        public IHttpActionResult Getcartbyiduser(string id_user)
        {
            if (string.IsNullOrEmpty(id_user))
            {
                return BadRequest("Invalid user ID");
            }  
            var list = db.Shopping_Cart.Where(c => c.user_id == id_user).Select(c=>new GioHang {
                cart_id = c.cart_id,
                product_id = c.product_id,
                size = c.size,
                quantity = c.quantity,
                user_id = c.user_id,
                imageurl = c.Product.productimage_url,
                productname = c.Product.product_name,
                price = c.Product.price    
            }).ToList();

            return Ok(list);
        }
        [HttpPost]
        [Route("api/Cart/PostCart")]
        public HttpResponseMessage PostCart(GioHang cart)
        {
            Shopping_Cart oldcart = db.Shopping_Cart.FirstOrDefault(c => c.user_id == cart.user_id && c.size == cart.size && c.product_id==cart.product_id);
          
            if (oldcart == null)
            {
                Shopping_Cart newcart = new Shopping_Cart
                {
                    cart_id = cart.cart_id,
                    product_id = cart.product_id,
                    size = cart.size,
                    price = cart.price,
                    quantity = cart.quantity,
                    user_id = cart.user_id,
                   
                };
                db.Shopping_Cart.Add(newcart);
            }
            else
            {
                oldcart.quantity+= cart.quantity;
            } 
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPut]
        [Route("api/Cart/PutCart")]
        public IHttpActionResult PutCart(GioHang cart)
        {
            Shopping_Cart oldcart = db.Shopping_Cart.FirstOrDefault(c => c.cart_id == cart.cart_id);
            if (oldcart == null)
            {
                return NotFound();
            }
            if (cart.quantity <1)
            {
                return BadRequest();
            }
            oldcart.quantity = cart.quantity;  
            db.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("api/Cart/Deletecart")]
        public IHttpActionResult Deletecart(string id_cart)
        {
            Shopping_Cart cart = db.Shopping_Cart.Where(c => c.cart_id == id_cart).FirstOrDefault();
            if (cart == null)
            {
                return NotFound();
            }
            db.Shopping_Cart.Remove(cart);
            db.SaveChanges();
            return Ok();
        }
        
    }
}
