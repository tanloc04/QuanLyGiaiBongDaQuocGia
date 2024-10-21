using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doan.Models
{
    public class TaiKhoan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Role {  get; set; }
    }
}