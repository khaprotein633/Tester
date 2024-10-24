using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class Shoes
    {
            public string product_id { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn phân loại !")]
        public Nullable<int> category_id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm !")]
        public string product_name { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn thương hiệu !")]

        public Nullable<int> brand_id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập giá !")]
        public Nullable<decimal> price { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Url hình ảnh !")]
        public string productimage_url { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mô tả !")]
        public string description { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập chi tiết sản phẩm!")]
        public string detail { get; set; }
        public string brandname { get; set; }
        public Nullable<int> hide { get; set; }

        public List<BinhLuanSP> Comments { get; set; }
    }
}