using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class UsersController : ApiController
    {
        //Database1Entities1 db = new Database1Entities1();
        Store_apiEntities db = new Store_apiEntities();

        [HttpPost]
        [Route("api/Users/CheckLogin")]
        public IHttpActionResult CheckLogin(NguoiDung login)
        {
            User user = db.Users.FirstOrDefault(c => c.email == login.email && c.password == login.password);
            if (user == null)
            {
                return NotFound();
            }
            NguoiDung KH = new NguoiDung { 
                user_id = user.user_id,
                role_id = user.role_id,
                email = user.email,
                password = user.password,
                phonenumber = user.phonenumber,
                address = user.address,
                name = user.name
            };
            return Ok(KH);

        }

        [HttpGet]
        [Route("api/Users/Get_list_user")]
        public IHttpActionResult Getalluser()
        {
            var list = db.Users.Select(u=>new NguoiDung{
                user_id = u.user_id,
                role_id = u.role_id,
                name = u.name,
                email = u.email,
                phonenumber = u.phonenumber,
                password = u.password,
                address = u.address,
                accounddate = u.account_date
            }).ToList();

            return Ok(list);
        }


        [HttpGet]
        [Route("api/Users/GetALLByName")]
        public IHttpActionResult GetALLByName(string name)
        {
            var list = db.Users.Where(c=>c.name.Contains(name)).Select(u => new NguoiDung
            {
                user_id = u.user_id,
                role_id = u.role_id,
                name = u.name,
                email = u.email,
                phonenumber = u.phonenumber,
                password = u.password,
                address = u.address,
                accounddate = u.account_date
                
            }).ToList();
            if (list.Count == 0)
            {
                return NotFound();
            }

            return Ok(list);
        }


        [HttpGet]
        [Route("api/Users/Get_user_by_id")]
        public IHttpActionResult Getuserbyid(string id_user)
        {
            var list = db.Users.Where(c => c.user_id == id_user).Select(u=>new NguoiDung {
                user_id = u.user_id,
                role_id = u.role_id,
                name = u.name,
                email = u.email,
                phonenumber = u.phonenumber,
                password = u.password,
                address = u.address,
                accounddate = u.account_date
            }).ToList();
             
            return Ok(list);
        }

        [HttpGet]
        [Route("api/Users/GetUserById")]
        public IHttpActionResult GetUserById(string id_user)
        {
            var u = db.Users.FirstOrDefault(c => c.user_id == id_user);
            if (u == null)
            {
                return NotFound();
            }
            NguoiDung user = new NguoiDung
            {
                user_id = u.user_id,
                role_id = u.role_id,
                name = u.name,
                email = u.email,
                phonenumber = u.phonenumber,
                password = u.password,
                address = u.address,
                accounddate = u.account_date
            };
            return Ok(user);
        }


        [HttpPost]
        [Route("api/Users/Postuser")]
        public IHttpActionResult Postuser(NguoiDung user)
        {
            User newUser = new User {
                user_id = user.user_id,
                role_id = user.role_id,
                name = user.name,
                email = user.email,
                password = user.password,
                phonenumber = user.phonenumber,
                address = user.address,
                account_date = DateTime.Now
            };
            db.Users.Add(newUser);
            db.SaveChanges();
            return Ok(); 
        }

        [HttpPut]
        [Route("api/Users/Putpassword")]
        public IHttpActionResult PutPassword(NguoiDung u)
        {
            var user = db.Users.SingleOrDefault(c => c.email == u.email);
            user.password = u.password;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Users/Putuser")]
        public IHttpActionResult Putuser(NguoiDung user)
        {
            User olduser = db.Users.FirstOrDefault(c => c.user_id == user.user_id);
            olduser.role_id = user.role_id;
            olduser.name = user.name;
            olduser.email = user.email;
            olduser.password = user.password;      
            olduser.phonenumber = user.phonenumber;
            olduser.address = user.address;

            db.SaveChanges();
            return Ok(user);
        }

        [HttpDelete]
        [Route("api/Users/DeleteUser")]
        public IHttpActionResult DeleteUser(string id_user)
        {
            var user = db.Users.FirstOrDefault(u => u.user_id == id_user);
            if (user == null) {
                return NotFound();
            }
            var carts = db.Shopping_Cart.Where(c => c.user_id == id_user).ToList();
            if (carts != null)
            {
                foreach(var item in carts)
                {
                    db.Shopping_Cart.Remove(item);
                }
            }
            var reviews = db.Product_Review.Where(r => r.user_id == id_user).ToList();
            if (reviews != null)
            {
                foreach (var item in reviews)
                {
                    db.Product_Review.Remove(item);
                }
            }
            var orders = db.Orders.Where(o => o.user_id == id_user).ToList();
            foreach(var item in orders)
            {
                var detail = db.Order_Details.Where(od => od.order_id == item.orders_id).ToList();
                foreach(var od in detail)
                {
                    db.Order_Details.Remove(od);
                }
                db.Orders.Remove(item);
            }

            db.Users.Remove(user);
            db.SaveChanges();
            return Ok();
        }


    }
}
