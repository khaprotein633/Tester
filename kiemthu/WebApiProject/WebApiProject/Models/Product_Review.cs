//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Review
    {
        public string product_review_id { get; set; }
        public string product_id { get; set; }
        public string user_id { get; set; }
        public Nullable<int> rating { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> review_date { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
