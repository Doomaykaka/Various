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

    public class ЗагоныController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: ЗагоныW
        public ActionResult IndexW()
        {
            var загоны = db.Загоны.Include(з => з.Участок);
            return View(загоны.ToList());
        }

        // GET: ЗагоныU
        public ActionResult IndexU()
        {
            var загоны = db.Загоны.Include(з => з.Участок);
            return View(загоны.ToList());
        }

        // GET: Загоны
        public ActionResult Index()
        {
            var загоны = db.Загоны.Include(з => з.Участок);
            return View(загоны.ToList());
        }

        // GET: Загоны/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Загоны загоны = db.Загоны.Find(id);
            if (загоны == null)
            {
                return HttpNotFound();
            }
            return View(загоны);
        }

        // GET: Загоны/Create
        public ActionResult Create()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Загоны def = new Загоны { Колличество_стойл = 1, Тип_животных = "Парнокопытные", Тип_ограждения = "Закрытое" };
            return View(def);
        }

        // GET: Загоны/Create
        public ActionResult CreateW()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Загоны def = new Загоны { Колличество_стойл = 1, Тип_животных = "Парнокопытные", Тип_ограждения = "Закрытое" };
            return View(def);
        }

        // POST: Загоны/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_участка,Колличество_стойл,Тип_животных,Тип_ограждения")] Загоны загоны)
        {
           try
            {
                if (ModelState.IsValid)
            {
                db.Загоны.Add(загоны);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch
            {
                ViewBag.ZagError = "Ошибка , данный номер участка уже используется";
            }

            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", загоны.Номер_участка);
            return View(загоны);
        }

        // POST: Загоны/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Номер_участка,Колличество_стойл,Тип_животных,Тип_ограждения")] Загоны загоны)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Загоны.Add(загоны);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }
            }
            catch
            {
                ViewBag.ZagError = "Ошибка , данный номер участка уже используется";
            }

            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", загоны.Номер_участка);
            return View(загоны);
        }

        // GET: Загоны/Edit/5
        public ActionResult Edit(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Загоны загоны = db.Загоны.Find(id);
                if (загоны == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", загоны.Номер_участка);
                return View(загоны);
        }

        // POST: Загоны/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_участка,Колличество_стойл,Тип_животных,Тип_ограждения")] Загоны загоны)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(загоны).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", загоны.Номер_участка);
            }
            catch {
                ViewBag.ZagError = "Ошибка";
            }
            return View(загоны);
        }

        // GET: Загоны/Delete/5
        public ActionResult Delete(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Загоны загоны = db.Загоны.Find(id);
                if (загоны == null)
                {
                    return HttpNotFound();
                }
                return View(загоны);
            
        }

        // POST: Загоны/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Загоны загоны = db.Загоны.Find(id);
                db.Загоны.Remove(загоны);
                db.SaveChanges();
            }
            catch {
                ViewBag.ZagError = "Ошибка";
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
