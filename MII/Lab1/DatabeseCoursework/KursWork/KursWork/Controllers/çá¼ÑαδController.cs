using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KursWork.Models;

namespace KursWork.Controllers
{

    public class ЗамерыController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();


        // GET: ЗамерыU
        public ActionResult IndexU()
        {
            var замеры = db.Замеры.Include(з => з.Участок);
            return View(замеры.ToList());
        }

        // GET: ЗамерыW
        public ActionResult IndexW()
        {
            var замеры = db.Замеры.Include(з => з.Участок);
            return View(замеры.ToList());
        }

        // GET: Замеры
        public ActionResult Index()
        {
            var замеры = db.Замеры.Include(з => з.Участок);
            return View(замеры.ToList());
        }

        // POST: Замеры
        [HttpPost]
        public ActionResult Index(string Tmin,string Tmax,string Pmin ,string Pmax,string Wmin,string Wmax,string Lie)
        {
            var замеры = db.Замеры.Include(з => з.Участок);
            try
            {
                if ((Tmax != "") && (Tmin != "") && (Pmax != "") && (Pmin != "") && (Wmax != "") && (Wmin != "") && (Lie != ""))
                {
                    string sqlQuery = "SELECT [dbo].[PRIGOD] ({0},{1},{2},{3},{4},{5},{6})";
                    Object[] parameters = { Int32.Parse(Tmax), Int32.Parse(Tmin), Int32.Parse(Pmax), Int32.Parse(Pmin), Int32.Parse(Wmax), Int32.Parse(Wmin), Int32.Parse(Lie) };
                    int activityCount = db.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                    Debug.WriteLine(activityCount.ToString());
                    if (activityCount == 0)
                    {
                        ViewBag.activityCount = "Данные прошли проверку на корректность";
                    }
                    if (activityCount == 1)
                    {
                        ViewBag.activityCount = "Проверка на корректность не требуется";
                    }
                    if (activityCount == -1)
                    {
                        ViewBag.activityCount = "Данные не прошли проверку на корректность";
                    }
                }
            }
            catch {
                ViewBag.activityCount = "Поля заполнены неверно";
            }
            
            return View(замеры.ToList());
        }

        // GET: Замеры/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Замеры замеры = db.Замеры.Find(id);
            if (замеры == null)
            {
                return HttpNotFound();
            }
            return View(замеры);
        }

        // GET: Замеры/Create
        public ActionResult Create()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Замеры def = new Замеры { Номер_замера=4,Средняя_температура_на_участке=36,Давление=1,Влажность_участка=10,Освещенность_участка="Хорошая",Пригодность_параметров=1,Дата_проведения=DateTime.Parse("2020/06/01") };
            return View(def);
        }

        // GET: Замеры/CreateW
        public ActionResult CreateW()
        {
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Замеры def = new Замеры { Номер_замера = 4, Средняя_температура_на_участке = 36, Давление = 1, Влажность_участка = 10, Освещенность_участка = "Хорошая", Пригодность_параметров = 1, Дата_проведения = DateTime.Parse("2020/06/01") };
            return View(def);
        }

        // POST: Замеры/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Средняя_температура_на_участке,Давление,Влажность_участка,Освещенность_участка,Пригодность_параметров,Дата_проведения,Номер_замера,Номер_участка")] Замеры замеры)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Замеры.Add(замеры);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", замеры.Номер_участка);
            }
            catch {
                ViewBag.activityCount = "Ошибка";
            }
            return View(замеры);
            }

        // POST: Замеры/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Средняя_температура_на_участке,Давление,Влажность_участка,Освещенность_участка,Пригодность_параметров,Дата_проведения,Номер_замера,Номер_участка")] Замеры замеры)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Замеры.Add(замеры);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }

                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", замеры.Номер_участка);
            }
            catch {
                ViewBag.activityCount = "Ошибка";
            }
            return View(замеры);
        }

        // GET: Замеры/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Замеры замеры = db.Замеры.Find(id);
                if (замеры == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", замеры.Номер_участка);
                return View(замеры);
        }


        // POST: Замеры/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Средняя_температура_на_участке,Давление,Влажность_участка,Освещенность_участка,Пригодность_параметров,Дата_проведения,Номер_замера,Номер_участка")] Замеры замеры)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(замеры).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", замеры.Номер_участка);
            }
            catch {
                ViewBag.activityCount = "Ошибка";
            }
            return View(замеры);
        }

        // GET: Замеры/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Замеры замеры = db.Замеры.Find(id);
                if (замеры == null)
                {
                    return HttpNotFound();
                }
                return View(замеры);
        }

        // POST: Замеры/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Замеры замеры = db.Замеры.Find(id);
                db.Замеры.Remove(замеры);
                db.SaveChanges();
            }
            catch {
                ViewBag.activityCount = "Ошибка";
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
