using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DomeSell.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Report1()
        {
            return Redirect("~/Reports/Web_PRODUC.aspx");
        }
        public ActionResult Report2()
        {
            return Redirect("~/Reports/Web_Order.aspx");
        }
        public ActionResult Report3()
        {
            return Redirect("~/Reports/Report_AddPro.aspx");
        }
        public ActionResult Report4()
        {
            return Redirect("~/Reports/Web_Product.aspx");
        }
    }
}