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

    public class Поля_и_теплицыController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: Поля_и_теплицыW
        public ActionResult IndexW()
        {
            var поля_и_теплицы = db.Поля_и_теплицы.Include(п => п.Участок);
            return View(поля_и_теплицы.ToList());
        }

        // GET: Поля_и_теплицыU
        public ActionResult IndexU()
        {
            var поля_и_теплицы = db.Поля_и_теплицы.Include(п => п.Участок);
            return View(поля_и_теплицы.ToList());
        }

        // GET: Поля_и_теплицы
        public ActionResult Index()
        {
            var поля_и_теплицы = db.Поля_и_теплицы.Include(п => п.Участок);
            return View(поля_и_теплицы.ToList());
        }

        // GET: Поля_и_теплицы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Поля_и_теплицы поля_и_теплицы = db.Поля_и_теплицы.Find(id);
            if (поля_и_теплицы == null)
            {
                return HttpNotFound();
            }
            return View(поля_и_теплицы);
        }

        // GET: Поля_и_теплицы/CreateW
        public ActionResult CreateW()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Номер_участка"/*"Тип_участка"*/);
            Поля_и_теплицы def = new Поля_и_теплицы { Тип_почвы = "Водянистая", Вид_растений = "Корнеплод", Наличие_водоканалов = true };
            return View(def);
        }

        // GET: Поля_и_теплицы/Create
        public ActionResult Create()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Номер_участка"/*"Тип_участка"*/);
            Поля_и_теплицы def = new Поля_и_теплицы { Тип_почвы="Водянистая",Вид_растений="Корнеплод",Наличие_водоканалов=true};
            return View(def);
        }

        // POST: Поля_и_теплицы/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Номер_участка,Тип_почвы,Вид_растений,Наличие_водоканалов")] Поля_и_теплицы поля_и_теплицы)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Поля_и_теплицы.Add(поля_и_теплицы);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }
            }
            catch
            {
                ViewBag.PitError = "Ошибка , данный номер участка уже используется";
            }

            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Номер_участка", поля_и_теплицы.Номер_участка);
            return View(поля_и_теплицы);
        }

        // POST: Поля_и_теплицы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_участка,Тип_почвы,Вид_растений,Наличие_водоканалов")] Поля_и_теплицы поля_и_теплицы)
        {
            try
            {
                if (ModelState.IsValid)
            {
                db.Поля_и_теплицы.Add(поля_и_теплицы);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch
            {
                ViewBag.PitError = "Ошибка , данный номер участка уже используется";
            }

            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Номер_участка", поля_и_теплицы.Номер_участка);
            return View(поля_и_теплицы);
        }

        // GET: Поля_и_теплицы/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Поля_и_теплицы поля_и_теплицы = db.Поля_и_теплицы.Find(id);
                if (поля_и_теплицы == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", поля_и_теплицы.Номер_участка);
                return View(поля_и_теплицы);
        }

        // POST: Поля_и_теплицы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_участка,Тип_почвы,Вид_растений,Наличие_водоканалов")] Поля_и_теплицы поля_и_теплицы)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(поля_и_теплицы).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", поля_и_теплицы.Номер_участка);
            }
            catch {
                ViewBag.PitError = "Ошибка";
            }
            return View(поля_и_теплицы);
        }

        // GET: Поля_и_теплицы/Delete/5
        public ActionResult Delete(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Поля_и_теплицы поля_и_теплицы = db.Поля_и_теплицы.Find(id);
                if (поля_и_теплицы == null)
                {
                    return HttpNotFound();
                }
                return View(поля_и_теплицы);
        }

        // POST: Поля_и_теплицы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Поля_и_теплицы поля_и_теплицы = db.Поля_и_теплицы.Find(id);
                db.Поля_и_теплицы.Remove(поля_и_теплицы);
                db.SaveChanges();
            }
            catch {
                ViewBag.PitError = "Ошибка";
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
