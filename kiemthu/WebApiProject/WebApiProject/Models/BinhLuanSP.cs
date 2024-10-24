using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class BinhLuanSP
    {
        public string product_review_id { get; set; }
        public string product_id { get; set; }
        public string user_id { get; set; }
        public Nullable<int> rating { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> review_date { get; set; }
        public string nameuser { get; set; }
 

    }
}