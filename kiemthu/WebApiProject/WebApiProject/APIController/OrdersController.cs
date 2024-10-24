using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;


namespace WebApiProject.APIController
{

    public class OrdersController : ApiController
    {
        //Database1Entities1 db = new Database1Entities1();
        Store_apiEntities db = new Store_apiEntities();


        [HttpGet]
        [Route("api/Orders/Get_all_orders")]
        public IHttpActionResult Getallorders()
        {
            var list = db.Orders.OrderByDescending(r => r.orders_date).
            Select(o => new DonHang
            {
                orders_id = o.orders_id,
                user_id = o.user_id,
                total_amount = o.total_amount,
                orders_date = o.orders_date,
                delivery_date = o.delivery_date,
                shipping_address = o.shipping_address,
                user_phone = o.user_phone,
                oderstatus_id = o.oderstatus_id,
                status = o.oderStatusCheck.status
            }).ToList();

            return Ok(list);
        }


        [HttpGet]
        [Route("api/Orders/Get_orders_by_order_date")]
        public IHttpActionResult Getorderbydate(DateTime date)
        {
            var list = db.Orders.Where(c=>c.orders_date==date).OrderByDescending(r => r.orders_date).
            Select(o => new DonHang
            {
                orders_id = o.orders_id,
                user_id = o.user_id,
                total_amount = o.total_amount,
                orders_date = o.orders_date,
                delivery_date = o.delivery_date,
                shipping_address = o.shipping_address,
                user_phone = o.user_phone,
                oderstatus_id = o.oderstatus_id,
                status = o.oderStatusCheck.status
            }).ToList();

            return Ok(list);
        }


        [HttpGet]
        [Route("api/Orders/Get_orders_by_ID_user")]
        public IHttpActionResult Getordersbyuser(string id_user)
        {
            var list = db.Orders.Where(c => c.user_id == id_user).OrderByDescending(r => r.orders_date).
            Select(o=>new DonHang {
                orders_id = o.orders_id,
                user_id = o.user_id,
                total_amount = o.total_amount,
                orders_date = o.orders_date,
                delivery_date = o.delivery_date,
                shipping_address = o.shipping_address,
                user_phone = o.user_phone,
                oderstatus_id = o.oderstatus_id,
                status = o.oderStatusCheck.status
            }).ToList();

            return Ok(list);
        }


        [HttpGet]
        [Route("api/Orders/Get_orders_by_Status")]
        public IHttpActionResult GetOrdersByStatus(int id_status)
        {
            
                var orders = db.Orders.Where(o => o.oderstatus_id == id_status)
                                      .OrderByDescending(o => o.orders_date)
                                      .Select(o => new DonHang
                                      {
                                          orders_id = o.orders_id,
                                          user_id = o.user_id,
                                          total_amount = o.total_amount,
                                          orders_date = o.orders_date,
                                          delivery_date = o.delivery_date,
                                          shipping_address = o.shipping_address,
                                          user_phone = o.user_phone,
                                          oderstatus_id = o.oderstatus_id,
                                          status = o.oderStatusCheck.status
                                      })
                                      .ToList();

               
                return Ok(orders);
        }

        [HttpGet]
        [Route("api/Orders/GetOrderById")]
        public IHttpActionResult GetOrderById(string id_order)
        {
                // Tìm đơn hàng dựa trên orderId
                var order = db.Orders.FirstOrDefault(o => o.orders_id == id_order);
                if (order != null)
                {
                    // Chuyển đổi đơn hàng thành DTO (Data Transfer Object) nếu cần
                    DonHang orderDto = new DonHang
                    {
                        orders_id = order.orders_id,
                        user_id = order.user_id,
                        total_amount = order.total_amount,
                        orders_date = order.orders_date,
                        delivery_date = order.delivery_date,
                        shipping_address = order.shipping_address,
                        user_phone = order.user_phone,
                        oderstatus_id = order.oderstatus_id,
                        status = order.oderStatusCheck.status
                    };
                    return Ok(orderDto);
                }
                else
                {
                    return NotFound();
                }
           
        }


        [HttpPost]
        [Route("api/Orders/PostOrder")]
        public HttpResponseMessage PostOrder(DonHang o)
        { 
                    // Tạo một đối tượng Order mới từ đối tượng DonHang được gửi lên
                    Order newOrder = new Order
                    {
                        orders_id = o.orders_id,
                        user_id = o.user_id,
                        total_amount = o.total_amount,
                        orders_date = DateTime.Now, // Sử dụng thời gian hiện tại cho ngày đặt hàng
                        shipping_address = o.shipping_address,
                        user_phone = o.user_phone,
                        oderstatus_id = o.oderstatus_id,
                    };

                    // Thêm đối tượng mới vào DbSet của Orders và lưu thay đổi vào cơ sở dữ liệu
                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    // Trả về một phản hồi HTTP 201 Created nếu mọi thứ thành công
                    return Request.CreateResponse(HttpStatusCode.OK);
             
            
        }

        [HttpPut]
        [Route("api/Orders/PutOrder")]
        public HttpResponseMessage PutOrder(DonHang o)
        {
            Order oldOrder = db.Orders.FirstOrDefault(c => c.orders_id == o.orders_id);
            oldOrder.oderstatus_id = o.oderstatus_id;
            if (o.delivery_date != null)
            {
                oldOrder.delivery_date = o.delivery_date;
            } 
             
            db.SaveChanges();
          
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [HttpDelete]
        [Route("api/Orders/DeleteOrder")]
        public IHttpActionResult DeleteOrder(string id_order)
        {
            var order = db.Orders.FirstOrDefault(c => c.orders_id == id_order);
            var details = db.Order_Details.Where(c => c.order_id == order.orders_id).ToList();
            foreach(var item in details)
            {
                db.Order_Details.Remove(item);
            }
            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok();
        }

    }
}
