using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class CategorysController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();

        [HttpGet]
        [Route("api/Categorys/Get_all_Categorys")]
        public IHttpActionResult Get_all_Categorys()
        {
            var list = db.Categories.Select(b => new PhanLoai
            {
                category_id = b.category_id,
                category_name = b.category_name
            }).ToList();

            return Ok(list);
        }

        [HttpGet]
        [Route("api/Categorys/Get_Categorys_by_id")]
        public IHttpActionResult Get_Categorys_by_id(int id_category)
        {
            var category = db.Categories.FirstOrDefault(c => c.category_id == id_category);
            PhanLoai th = new PhanLoai
            {
                category_id = category.category_id,
                category_name = category.category_name
            };
            return Ok(th);
        }

        [HttpPost]
        [Route("api/Categorys/Post_Categorys")]
        public IHttpActionResult Post_Categorys(PhanLoai newPL)
        {
            Category th = new Category
            {
                category_id = newPL.category_id,
                category_name = newPL.category_name
            };

            db.Categories.Add(th);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Categorys/Put_Categorys")]
        public IHttpActionResult Put_Categorys(PhanLoai old_category)
        {
           Category pl = db.Categories.FirstOrDefault(c => c.category_id == old_category.category_id);
            if (pl != null)
            { 
                pl.category_name = old_category.category_name;
                db.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("api/Categorys/Delete_Categorys")]
        public IHttpActionResult Delete_Categorys(int id_category)
        {
            var category = db.Categories.FirstOrDefault(c => c.category_id == id_category);
            db.Categories.Remove(category);
            db.SaveChanges();
            return Ok();
        }

    }
}
