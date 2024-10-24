using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class BrandsController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();
        [HttpGet]
        [Route("api/Brands/Get_all_brands")]
        public IHttpActionResult Get_all_brands()
        {
            var list = db.Brands.Select(b => new ThuongHieu { 
                brand_id = b.brand_id,
                brand_name = b.brand_name
            }).ToList();

            return Ok(list);
        }
        
        [HttpGet]
        [Route("api/Brands/Get_brand_by_id")]
        public IHttpActionResult Get_brand_by_id(int id_brand)
        {
            var brand = db.Brands.FirstOrDefault(c => c.brand_id == id_brand);
            ThuongHieu th = new ThuongHieu
            {
                brand_id = brand.brand_id,
                brand_name = brand.brand_name
            };
            return Ok(th);
        }
      
        [HttpPost]
        [Route("api/Brands/Post_brand")]
        public IHttpActionResult Post_brand(ThuongHieu newth)
        {
            Brand newb = new Brand {  
                brand_name = newth.brand_name
            };
            db.Brands.Add(newb);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Brands/Put_brand")]
        public IHttpActionResult Put_brand(ThuongHieu old_brand)
        {
            Brand b = db.Brands.FirstOrDefault(c => c.brand_id == old_brand.brand_id);
            if (b != null)
            {
                b.brand_name = old_brand.brand_name;
                db.SaveChanges();
                return Ok();
            }
               
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/Brands/Delete_brand")]
        public IHttpActionResult Delete_brand(int id_brand)
        {
            Brand brand = db.Brands.FirstOrDefault(c => c.brand_id == id_brand);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return Ok();
        }


    }
}
