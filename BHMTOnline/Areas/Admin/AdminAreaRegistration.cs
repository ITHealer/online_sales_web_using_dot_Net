using System.Web.Mvc;

namespace BHMTOnline.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin"; //đăng ký file này với tên phân vùng là gì?
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        { //Xem như là một route giống vs route ở ngoài
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "BHMTOnline.Areas.Admin.Controllers" }
            );
        }
    }
}