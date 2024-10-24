using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class WarehouseController : ApiController
    {

        Store_apiEntities db = new Store_apiEntities();

        [HttpGet]
        [Route("api/Warehouse/Get_list_products_in_warehouse")]
        public IHttpActionResult Get_list_products_in_warehouse()
        {
            var list = db.Product_Size_Quantity.Select(b => new Soluongton
            {
                product_size_quantity_id = b.product_size_quantity_id,
                product_id = b.product_id,
                size = b.size,
                quantity = b.quantity
            }).ToList();

            return Ok(list);
        }

        [HttpGet]
        [Route("api/Warehouse/Get_warehouse_by_id")]
        public IHttpActionResult Get_warehouse_by_id(int id_warehouse)
        {
            var b = db.Product_Size_Quantity.FirstOrDefault(c => c.product_size_quantity_id == id_warehouse);  
                
                Soluongton wh = new Soluongton
                {
                product_size_quantity_id = b.product_size_quantity_id,
                product_id = b.product_id,
                size = b.size,
                quantity = b.quantity
                };

            return Ok(b);
        }

        [HttpGet]
        [Route("api/Warehouse/Get_list_products_in_warehouse_by_id")]
        public IHttpActionResult Get_list_products_in_warehouse_by_id(string id_product)
        {
            var list = db.Product_Size_Quantity.Where(c=>c.product_id==id_product).Select(b => new Soluongton
            {
                product_size_quantity_id = b.product_size_quantity_id,
                product_id = b.product_id,
                size = b.size,
                quantity = b.quantity
            }).ToList();

            return Ok(list);
        }

        [HttpPost]
        [Route("api/Warehouse/Post_products_size_quantity")]
        public IHttpActionResult Post_products_size_quantity(Soluongton newslt)
        {
            Product_Size_Quantity newpsq = new Product_Size_Quantity { 
                product_id = newslt.product_id,
                size = newslt.size,
                quantity = newslt.quantity
            };
            db.Product_Size_Quantity.Add(newpsq);
            db.SaveChanges();

            return Ok();
        }


        [HttpPut]
        [Route("api/Warehouse/Put_size_quantity")]
        public IHttpActionResult Put_size_quantity(Soluongton newSLT)
        {
            Product_Size_Quantity oleSLT = db.Product_Size_Quantity.FirstOrDefault(c => c.product_size_quantity_id == newSLT.product_size_quantity_id);

            if (oleSLT != null)
            {
                if (newSLT.quantity >= 0 && newSLT.quantity <= oleSLT.quantity)
                {
                    oleSLT.quantity -= newSLT.quantity;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("api/Warehouse/Put_products_size_quantity")]
        public IHttpActionResult Put_products_size_quantity(Soluongton newSLT)
        {
            Product_Size_Quantity oleSLT = db.Product_Size_Quantity.FirstOrDefault(c => c.product_size_quantity_id == newSLT.product_size_quantity_id);
            
            oleSLT.quantity = newSLT.quantity;
           
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("api/Warehouse/Delete_products_size_quantity")]
        public IHttpActionResult Delete_brand(int id_product_size)
        {
            Product_Size_Quantity SLT = db.Product_Size_Quantity.FirstOrDefault(c => c.product_size_quantity_id == id_product_size);
           
            db.Product_Size_Quantity.Remove(SLT);
            db.SaveChanges();
            return Ok();
        }

    }
}
