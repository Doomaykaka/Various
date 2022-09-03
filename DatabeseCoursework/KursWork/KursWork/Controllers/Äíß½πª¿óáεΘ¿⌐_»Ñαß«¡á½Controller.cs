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

    public class Обслуживающий_персоналController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: Обслуживающий_персоналW
        public ActionResult IndexW()
        {
            var обслуживающий_персонал = db.Обслуживающий_персонал.Include(о => о.Обработка);
            return View(обслуживающий_персонал.ToList());
        }

        // GET: Обслуживающий_персоналU
        public ActionResult IndexU()
        {
            var обслуживающий_персонал = db.Обслуживающий_персонал.Include(о => о.Обработка);
            return View(обслуживающий_персонал.ToList());
        }

        // GET: Обслуживающий_персонал
        public ActionResult Index()
        {
            var обслуживающий_персонал = db.Обслуживающий_персонал.Include(о => о.Обработка);
            return View(обслуживающий_персонал.ToList());
        }

        // GET: Обслуживающий_персонал/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Обслуживающий_персонал обслуживающий_персонал = db.Обслуживающий_персонал.Find(id);
            if (обслуживающий_персонал == null)
            {
                return HttpNotFound();
            }
            return View(обслуживающий_персонал);
        }

        // GET: Обслуживающий_персонал/CreateW
        public ActionResult CreateW()
        {
            ViewBag.ID_процесса_обработки = new SelectList(db.Обработка, "ID_процесса_обработки", "Описание_процесса_обработки");
            Обслуживающий_персонал def = new Обслуживающий_персонал { ФИО = "Иванов Иван Иванович", Должность = "Уборщик", Договор = 132453, Паспорт_работника = "1111111111" };
            return View(def);
        }

        // GET: Обслуживающий_персонал/Create
        public ActionResult Create()
        {
            ViewBag.ID_процесса_обработки = new SelectList(db.Обработка, "ID_процесса_обработки", "Описание_процесса_обработки");
            Обслуживающий_персонал def = new Обслуживающий_персонал { ФИО="Иванов Иван Иванович",Должность="Уборщик",Договор=132453,Паспорт_работника="1111111111"};
            return View(def);
        }

        // POST: Обслуживающий_персонал/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "ФИО,Должность,Договор,Паспорт_работника,ID_процесса_обработки")] Обслуживающий_персонал обслуживающий_персонал)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Обслуживающий_персонал.Add(обслуживающий_персонал);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }
                catch
                {
                    return View("Error");
                }

            }

            ViewBag.ID_процесса_обработки = new SelectList(db.Обработка, "ID_процесса_обработки", "Описание_процесса_обработки", обслуживающий_персонал.ID_процесса_обработки);
            return View(обслуживающий_персонал);
        }

        // POST: Обслуживающий_персонал/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ФИО,Должность,Договор,Паспорт_работника,ID_процесса_обработки")] Обслуживающий_персонал обслуживающий_персонал)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Обслуживающий_персонал.Add(обслуживающий_персонал);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch {
                    return View("Error");
                }
                
            }

            ViewBag.ID_процесса_обработки = new SelectList(db.Обработка, "ID_процесса_обработки", "Описание_процесса_обработки", обслуживающий_персонал.ID_процесса_обработки);
            return View(обслуживающий_персонал);
        }

        // GET: Обслуживающий_персонал/Edit/5
        public ActionResult Edit(string id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Обслуживающий_персонал обслуживающий_персонал = db.Обслуживающий_персонал.Find(id);
                if (обслуживающий_персонал == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ID_процесса_обработки = new SelectList(db.Обработка, "ID_процесса_обработки", "Описание_процесса_обработки", обслуживающий_персонал.ID_процесса_обработки);
                return View(обслуживающий_персонал);
        }

        // POST: Обслуживающий_персонал/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ФИО,Должность,Договор,Паспорт_работника,ID_процесса_обработки")] Обслуживающий_персонал обслуживающий_персонал)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(обслуживающий_персонал).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch {
                    return View("Error");
                }
                
            }
            ViewBag.ID_процесса_обработки = new SelectList(db.Обработка, "ID_процесса_обработки", "Описание_процесса_обработки", обслуживающий_персонал.ID_процесса_обработки);
            return View(обслуживающий_персонал);
        }

        // GET: Обслуживающий_персонал/Delete/5
        public ActionResult Delete(string id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Обслуживающий_персонал обслуживающий_персонал = db.Обслуживающий_персонал.Find(id);
                if (обслуживающий_персонал == null)
                {
                    return HttpNotFound();
                }
                return View(обслуживающий_персонал);
        }

        // POST: Обслуживающий_персонал/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Обслуживающий_персонал обслуживающий_персонал = db.Обслуживающий_персонал.Find(id);
                db.Обслуживающий_персонал.Remove(обслуживающий_персонал);
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
