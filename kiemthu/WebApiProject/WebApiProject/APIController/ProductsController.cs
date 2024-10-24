using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;
using System.Data.Entity;


namespace WebApiProject.APIController
{
    public class ProductsController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();
        [HttpGet]
        [Route("api/Products/Getproduct")]
        public IHttpActionResult Getproduct()
        {
            var list = db.Products.Select(p=>new Shoes{
                    product_id=p.product_id,
                    category_id = p.category_id,
                    product_name = p.product_name,
                    brand_id = p.brand_id,
                    price = p.price,
                    productimage_url =p.productimage_url,
                    description = p.description,
                    brandname = p.Brand.brand_name,
                    detail = p.detail ,
                    hide = p.hide
                }).ToList();
           
               return Ok(list);
           
        }   

        [HttpGet]
        [Route("api/Products/Getproductbyid")]
        public IHttpActionResult Getproductbyid(string id_product)
        {
       
            var p = db.Products.FirstOrDefault(c => c.product_id == id_product);
            Shoes sp = new Shoes {
            product_id = p.product_id,
            category_id = p.category_id,
            product_name = p.product_name,
            brand_id = p.brand_id,
            price = p.price,
            productimage_url = p.productimage_url,
            description = p.description,
                brandname = p.Brand.brand_name,
                detail = p.detail,
                hide = p.hide
            };
            if (sp == null)
            {
                return NotFound(); 
            }
            return Ok(sp); 
        }

        [HttpGet]
        [Route("api/Products/Getproductbyid_category")]
        public IHttpActionResult Getproducttype(int id_category)
        {
           
            var list = db.Products.Where(c=>c.category_id==id_category).
               Select(p => new Shoes
               {
                   product_id = p.product_id,
                   category_id = p.category_id,
                   product_name = p.product_name,
                   brand_id = p.brand_id,
                   price = p.price,
                   productimage_url = p.productimage_url,
                   description = p.description,
                   brandname = p.Brand.brand_name,
                   detail = p.detail,
                   hide = p.hide
               }).ToList();
            if (list == null)
            {
                return NotFound(); 
            }
            return Ok(list); 
        }

        [HttpGet]
        [Route("api/Products/SearchProductByName")]
        public IHttpActionResult GetProductByName(string productName)
        {
            var products = db.Products
                            .Where(p => p.product_name.Contains(productName))
                            .Select(p => new Shoes
                            {
                                product_id = p.product_id,
                                category_id = p.category_id,
                                product_name = p.product_name,
                                brand_id = p.brand_id,
                                price = p.price,
                                productimage_url = p.productimage_url,
                                description = p.description,
                                brandname = p.Brand.brand_name,
                                detail = p.detail,
                                hide = p.hide
                            })
                            .ToList();

            
            if (products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }


        [HttpPost]
        [Route("api/Products/PostProduct")]
        public IHttpActionResult PostProduct(Shoes P)
        {
            Product newP = new Product { 
                product_id = P.product_id,
                category_id = P.category_id,
                brand_id = P.brand_id,
                product_name = P.product_name,
                price = P.price,
                productimage_url = P.productimage_url,
                description = P.description,
                detail = P.detail ,
                hide = 1
            };

            db.Products.Add(newP);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Products/PutProduct")]
        public IHttpActionResult PutProduct(Shoes P)
        {
            Product OldP = db.Products.FirstOrDefault(c => c.product_id == P.product_id);

            OldP.category_id = P.category_id;
            OldP.brand_id = P.brand_id;
            OldP.product_name = P.product_name;
            OldP.price = P.price;
            OldP.productimage_url = P.productimage_url;
            OldP.description = P.description;
            OldP.detail = P.detail;
            OldP.hide = P.hide;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("api/Products/DeleteProduct")]
        public IHttpActionResult DeleteProduct(string id_product)
        {
            var sp = db.Products.FirstOrDefault(c => c.product_id == id_product);
            if (sp == null)
            {
                return NotFound();
            }

            var ctdhs = db.Order_Details.Where(o => o.product_id == id_product).ToList();
            if (ctdhs != null)
            {
                foreach (var item in ctdhs)
                {
                    db.Order_Details.Remove(item);
                }
            }
           
            var inventorys = db.Product_Size_Quantity.Where(o => o.product_id == id_product).ToList();
            if (inventorys != null) {
                foreach (var item in inventorys)
                {
                    db.Product_Size_Quantity.Remove(item);
                }
            }
            
            var carts = db.Shopping_Cart.Where(o => o.product_id == id_product).ToList();
            if (carts != null)
            {
                foreach (var item in carts)
                {
                    db.Shopping_Cart.Remove(item);
                }
            }

            db.Products.Remove(sp);
            db.SaveChanges();
            return Ok();
        }

    }
}
