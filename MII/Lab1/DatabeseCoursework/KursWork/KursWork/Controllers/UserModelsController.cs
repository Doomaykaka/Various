using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KursWork.Models;

namespace KursWork.Controllers
{
    public class UserModelsController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();


        // GET: UserModels/Create
        public ActionResult LogIn()
        {
            Session["Type"] = "0";
            return View();
        }

        // POST: UserModels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult LogIn(string LogIn, string PassW)
        {
            string pass = "";
            string log = "";
            try
            {
                pass = db.UserModels.Where(u => u.Login == LogIn).Where(u => u.Password == PassW).Select(u => u.Password).Single();
                log = db.UserModels.Where(u => u.Login == LogIn).Where(u => u.Password == PassW).Select(u => u.Login).Single();
            }
            catch (InvalidOperationException)//если указанного логина не существует
            {

                ViewBag.Error = "Указанного логина не существует!";
                
            }


            if (pass == PassW && LogIn == "Admin")
            {
                Session["Type"] = "1"; //Admin
                return RedirectToAction("Index", "Menu");
            }
            else {
                if (pass == PassW && log == LogIn)
                {
                    Session["Type"] = "2"; //Worker
                    return RedirectToAction("IndexW", "Menu");
                }
            }
            Session["Type"] = "0"; //Guest
            ViewBag.Error = "Пароль неправильный!";


            return View();
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
