using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;


namespace WebApiProject.APIController
{
    public class DetailController : ApiController
    {
        //Database1Entities1 db = new Database1Entities1();
        Store_apiEntities db = new Store_apiEntities();
        [HttpGet]   
        [Route("api/Detail/GetdetailByIDOrder")]
        public IHttpActionResult Getdetail(string id_order)
        {
            var list = db.Order_Details.Where(c => c.order_id == id_order).Select(od => new ChiTietDH
            {
                order_details_id = od.order_details_id,
                product_id = od.product_id,
                order_id = od.order_id,
                price_oder = od.price_oder,
                quantity = od.quantity,
                productimage_url = od.Product.productimage_url,
                size = od.size,
                product_name = od.Product.product_name
            }).ToList();

            return Ok(list);
        }

        [HttpPost]  
        [Route("api/Detail/PostDetail")]
        public IHttpActionResult PostDetail(ChiTietDH od)
        {
            Order_Details newod = new Order_Details
            {
                order_details_id = od.order_details_id,
                order_id = od.order_id,
                product_id = od.product_id,
                quantity = od.quantity,
                size = od.size,
                price_oder = od.price_oder
            };
            db.Order_Details.Add(newod);
            db.SaveChanges();
            return Ok();
        }
    }
}
