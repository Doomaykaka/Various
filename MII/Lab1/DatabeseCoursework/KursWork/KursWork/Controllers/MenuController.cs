using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursWork.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            if (Session["Type"] == "0") {
                return RedirectToAction("IndexU", "Menu");
            }
            if (Session["Type"] == "2") {
                return RedirectToAction("IndexW", "Menu");
            }
            return View();
        }
        // GET: MenuU
        public ActionResult IndexU() {
            return View();
        }
        // GET: MenuW
        public ActionResult IndexW()
        {
            return View();
        }
    }
}