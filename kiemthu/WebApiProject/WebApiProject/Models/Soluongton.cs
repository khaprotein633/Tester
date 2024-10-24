using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class Soluongton
    {
        public int product_size_quantity_id { get; set; }
        public string product_id { get; set; }
        public string size { get; set; }
        public Nullable<int> quantity { get; set; }
    }
}