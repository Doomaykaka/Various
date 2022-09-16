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

    public class ИнвентарьController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: ИнвентарьU
        public ActionResult IndexW()
        {
            var инвентарь = db.Инвентарь.Include(и => и.Инвентарь2);
            return View(инвентарь.ToList());
        }

        // GET: ИнвентарьU
        public ActionResult IndexU()
        {
            var инвентарь = db.Инвентарь.Include(и => и.Инвентарь2);
            return View(инвентарь.ToList());
        }

        // GET: Инвентарь
        public ActionResult Index()
        {
            var инвентарь = db.Инвентарь.Include(и => и.Инвентарь2);
            return View(инвентарь.ToList());
        }

        // GET: Инвентарь/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Инвентарь инвентарь = db.Инвентарь.Find(id);
            if (инвентарь == null)
            {
                return HttpNotFound();
            }
            return View(инвентарь);
        }

        // GET: Инвентарь/CreateW
        public ActionResult CreateW()
        {
            ViewBag.Номер_комплектного_инвентаря = new SelectList(db.Инвентарь, "Номер_инвентаря", "Наименование");
            Инвентарь def = new Инвентарь { Наименование = "Мотыга", Состояние = "Рабочее", Назначение = "Полоть грядки", Число_владельцев = 1, Стоимость = 2000, Принцип_работы = "Рубит сорняки", Номер_комплектного_инвентаря = 2 };
            return View(def);
        }

        // GET: Инвентарь/Create
        public ActionResult Create()
        {
            ViewBag.Номер_комплектного_инвентаря = new SelectList(db.Инвентарь, "Номер_инвентаря", "Наименование");
            Инвентарь def = new Инвентарь {Наименование="Мотыга",Состояние="Рабочее",Назначение="Полоть грядки",Число_владельцев=1,Стоимость=2000,Принцип_работы="Рубит сорняки" , Номер_комплектного_инвентаря=2 };
            return View(def);
        }

        // POST: Инвентарь/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Наименование,Номер_инвентаря,Состояние,Назначение,Число_владельцев,Стоимость,Принцип_работы,Номер_комплектного_инвентаря")] Инвентарь инвентарь)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Инвентарь.Add(инвентарь);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }

                ViewBag.Номер_комплектного_инвентаря = new SelectList(db.Инвентарь, "Номер_инвентаря", "Наименование", инвентарь.Номер_комплектного_инвентаря);
            }
            catch {
                return RedirectToAction("IndexW");
            }
            return View(инвентарь);
        }

        // POST: Инвентарь/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Наименование,Номер_инвентаря,Состояние,Назначение,Число_владельцев,Стоимость,Принцип_работы,Номер_комплектного_инвентаря")] Инвентарь инвентарь)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Инвентарь.Add(инвентарь);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Номер_комплектного_инвентаря = new SelectList(db.Инвентарь, "Номер_инвентаря", "Наименование", инвентарь.Номер_комплектного_инвентаря);
            }
            catch {
                return RedirectToAction("Index");
            }
            return View(инвентарь);
        }

        // GET: Инвентарь/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Инвентарь инвентарь = db.Инвентарь.Find(id);
                if (инвентарь == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер_комплектного_инвентаря = new SelectList(db.Инвентарь, "Номер_инвентаря", "Наименование", инвентарь.Номер_комплектного_инвентаря);
                return View(инвентарь);
        }

        // POST: Инвентарь/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Наименование,Номер_инвентаря,Состояние,Назначение,Число_владельцев,Стоимость,Принцип_работы,Номер_комплектного_инвентаря")] Инвентарь инвентарь)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(инвентарь).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер_комплектного_инвентаря = new SelectList(db.Инвентарь, "Номер_инвентаря", "Наименование", инвентарь.Номер_комплектного_инвентаря);
            }
            catch {
                return RedirectToAction("Index");
            }
            return View(инвентарь);
        }

        // GET: Инвентарь/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Инвентарь инвентарь = db.Инвентарь.Find(id);
                if (инвентарь == null)
                {
                    return HttpNotFound();
                }
                return View(инвентарь);
        }

        // POST: Инвентарь/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Инвентарь инвентарь = db.Инвентарь.Find(id);
                db.Инвентарь.Remove(инвентарь);
                db.SaveChanges();
            }
            catch {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
