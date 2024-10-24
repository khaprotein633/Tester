using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class ChiTietDH
    {
        public string order_details_id { get; set;}
        public string order_id { get; set; }
        public string product_id { get; set; }
        public Nullable<decimal> price_oder { get; set; }
        public Nullable<int> quantity { get; set; }
        public string productimage_url { get; set; }
        public string product_name { get; set; }
        public string size { get; set; }
    }
}