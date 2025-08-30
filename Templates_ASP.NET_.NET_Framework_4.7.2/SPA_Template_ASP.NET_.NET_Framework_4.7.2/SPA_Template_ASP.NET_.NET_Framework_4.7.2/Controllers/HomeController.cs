using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SPA_Template_ASP.NET_.NET_Framework_4._7._2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
