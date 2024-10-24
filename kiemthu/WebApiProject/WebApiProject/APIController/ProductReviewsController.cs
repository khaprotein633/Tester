using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.APIController
{
    public class ProductReviewsController : ApiController
    {
        Store_apiEntities db = new Store_apiEntities();


        [HttpGet]
        [Route("api/ProductReviews/GetAllReview")]
        public IHttpActionResult GetAllReview()
        {
            var list = db.Product_Review
                        .OrderByDescending(r => r.review_date)
                        .Select(r => new BinhLuanSP
                        {
                            product_review_id = r.product_review_id,
                            product_id = r.product_id,
                            user_id = r.user_id,
                            comment = r.comment,
                            rating = r.rating,
                            nameuser = r.User.name,
                            review_date = r.review_date
                        })
                         .ToList();

            return Ok(list);
        }

        [HttpGet]
        [Route("api/ProductReviews/GetReviewByID")]
        public IHttpActionResult GetReviewByID(string id_product)
        {
            var list = db.Product_Review
                        .Where(c => c.product_id == id_product)
                        .OrderByDescending(r => r.review_date)
                        .Select(r => new BinhLuanSP
                        {
                            product_review_id = r.product_review_id,
                            product_id = r.product_id,
                            user_id = r.user_id,
                            comment = r.comment,
                            rating = r.rating,
                            nameuser = r.User.name,
                            review_date = r.review_date
                        })
                         .ToList();

            return Ok(list);
        }

        [HttpPost]
        [Route("api/ProductReviews/PostReview")]
        public IHttpActionResult PostReview(BinhLuanSP bl)
        {
            Product_Review newreview = new Product_Review
            {
                product_review_id = bl.product_review_id,
                product_id = bl.product_id,
                user_id = bl.user_id,
                comment = bl.comment,
                rating = bl.rating,
                review_date = bl.review_date

            };
            db.Product_Review.Add(newreview);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/ProductReviews/DeleteReview")]
        public IHttpActionResult DeleteReview(string id_review)
        {
            Product_Review review = db.Product_Review.FirstOrDefault(c => c.product_review_id == id_review);
            db.Product_Review.Remove(review);
            db.SaveChanges();

            return Ok();
        }
    }
}
