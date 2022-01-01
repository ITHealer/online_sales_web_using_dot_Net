using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHMTOnline.Areas.Admin.Controllers
{
    public class SignOutController : Controller
    {
        // GET: Admin/SignOut
        public ActionResult Index()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}