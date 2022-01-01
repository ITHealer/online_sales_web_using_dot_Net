using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BHMTOnline.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Email")]
        public string userEmail { get; set; }
        [Display(Name = "Password")]
        public string passWord { get; set; }
    }
}