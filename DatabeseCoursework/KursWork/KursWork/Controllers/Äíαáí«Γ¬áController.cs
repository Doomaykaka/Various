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

    public class ОбработкаController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: ОбработкаW
        public ActionResult IndexW()
        {
            var обработка = db.Обработка.Include(о => о.Участок);
            return View(обработка.ToList());
        }

        // GET: ОбработкаU
        public ActionResult IndexU()
        {
            var обработка = db.Обработка.Include(о => о.Участок);
            return View(обработка.ToList());
        }

        // GET: Обработка
        public ActionResult Index()
        {
            var обработка = db.Обработка.Include(о => о.Участок);
            return View(обработка.ToList());
        }

        // GET: Обработка/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Обработка обработка = db.Обработка.Find(id);
            if (обработка == null)
            {
                return HttpNotFound();
            }
            return View(обработка);
        }

        // GET: Обработка/CreateW
        public ActionResult CreateW()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Обработка def = new Обработка { Время_обработки = DateTime.Parse("2020/01/02"), Статус_выполнения = 0, Описание_процесса_обработки = "Уборка сорняков" };
            return View(def);
        }

        // GET: Обработка/Create
        public ActionResult Create()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Обработка def = new Обработка {Время_обработки=DateTime.Parse("2020/01/02") , Статус_выполнения=0 , Описание_процесса_обработки="Уборка сорняков" };
            return View(def);
        }

        // POST: Обработка/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "ID_процесса_обработки,Время_обработки,Статус_выполнения,Номер_участка,Описание_процесса_обработки")] Обработка обработка)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Обработка.Add(обработка);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }

                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", обработка.Номер_участка);
            }
            catch {
                return RedirectToAction("IndexW");
            }
            return View(обработка);
        }


        // POST: Обработка/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_процесса_обработки,Время_обработки,Статус_выполнения,Номер_участка,Описание_процесса_обработки")] Обработка обработка)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Обработка.Add(обработка);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", обработка.Номер_участка);
            }
            catch {
                return RedirectToAction("Index");
            }
            return View(обработка);
        }

        // GET: Обработка/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Обработка обработка = db.Обработка.Find(id);
                if (обработка == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", обработка.Номер_участка);
                return View(обработка);
        }

        // POST: Обработка/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_процесса_обработки,Время_обработки,Статус_выполнения,Номер_участка,Описание_процесса_обработки")] Обработка обработка)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(обработка).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", обработка.Номер_участка);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return View(обработка);
        }

        // GET: Обработка/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Обработка обработка = db.Обработка.Find(id);
                if (обработка == null)
                {
                    return HttpNotFound();
                }
                return View(обработка);
        }

        // POST: Обработка/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Обработка обработка = db.Обработка.Find(id);
                db.Обработка.Remove(обработка);
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
