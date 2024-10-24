using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class NguoiDung
    {
        public string user_id { get; set; }
        public string role_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", 
            ErrorMessage = "Email không hợp lệ.")]
        public string email { get; set; }
        public string address { get; set; }
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string phonenumber { get; set; }
        public DateTime? accounddate { get; set; }
        
    }
}