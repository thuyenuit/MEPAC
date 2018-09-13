using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEPAC.Web.Controllers
{
    public class AdminController : Controller
    {
        public static string UserID = "";
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}