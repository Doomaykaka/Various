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

    public class УчастокController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: УчастокW
        public ActionResult IndexW(string SquareS, string SquareF, string SquareNum)
        {
            var участок = db.Участок.Include(у => у.Загоны).Include(у => у.Поля_и_теплицы);

            if (!String.IsNullOrEmpty(SquareNum))
            {
                int sn = Int32.Parse(SquareNum);
                участок = участок.Where(x => x.Номер_участка == sn);
            }

            if (!String.IsNullOrEmpty(SquareS) && !String.IsNullOrEmpty(SquareF))
            {
                int sq1 = Int32.Parse(SquareS);
                int sq2 = Int32.Parse(SquareF);
                участок = участок.Where(x => x.Площадь > sq1 && x.Площадь < sq2);
            }

            return View(участок.ToList());
        }

        // GET: УчастокU
        public ActionResult IndexU() {
            var участок = db.Участок.Include(у => у.Загоны).Include(у => у.Поля_и_теплицы);
            return View(участок.ToList());
        }

        // GET: Участок
        public ActionResult Index(string SquareS, string SquareF, string SquareNum)
        {
            var участок = db.Участок.Include(у => у.Загоны).Include(у => у.Поля_и_теплицы);

            if (!String.IsNullOrEmpty(SquareNum))
            {
                int sn = Int32.Parse(SquareNum);
                участок = участок.Where(x => x.Номер_участка == sn);
            }

            if (!String.IsNullOrEmpty(SquareS) && !String.IsNullOrEmpty(SquareF))
            {
                int sq1 = Int32.Parse(SquareS);
                int sq2 = Int32.Parse(SquareF);
                участок = участок.Where(x => x.Площадь > sq1 && x.Площадь < sq2);
            }

            return View(участок.ToList());
        }

        // GET: Участок/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Участок участок = db.Участок.Find(id);
            if (участок == null)
            {
                return HttpNotFound();
            }
            return View(участок);
        }

        // GET: Участок/CreateW
        public ActionResult CreateW()
        {
            ViewBag.Номер_участка = new SelectList(db.Загоны, "Номер_участка", "Тип_животных");
            ViewBag.Номер_участка = new SelectList(db.Поля_и_теплицы, "Номер_участка", "Тип_почвы");
            Участок def = new Участок { Площадь = 400, Эффективность = 10, Тип_участка = "Крытый" };
            return View(def);
        }

        // GET: Участок/Create
        public ActionResult Create()
        {
            ViewBag.Номер_участка = new SelectList(db.Загоны, "Номер_участка", "Тип_животных");
            ViewBag.Номер_участка = new SelectList(db.Поля_и_теплицы, "Номер_участка", "Тип_почвы");
            Участок def = new Участок { Площадь=400,Эффективность=10 , Тип_участка="Крытый"};
            return View(def);
        }

        // POST: Участок/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Номер_участка,Площадь,Эффективность,Тип_участка")] Участок участок)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Участок.Add(участок);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }

                ViewBag.Номер_участка = new SelectList(db.Загоны, "Номер_участка", "Тип_животных", участок.Номер_участка);
                ViewBag.Номер_участка = new SelectList(db.Поля_и_теплицы, "Номер_участка", "Тип_почвы", участок.Номер_участка);
            }
            catch {
                return RedirectToAction("IndexW");
            }
            return View(участок);
        }

        // POST: Участок/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_участка,Площадь,Эффективность,Тип_участка")] Участок участок)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Участок.Add(участок);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Номер_участка = new SelectList(db.Загоны, "Номер_участка", "Тип_животных", участок.Номер_участка);
                ViewBag.Номер_участка = new SelectList(db.Поля_и_теплицы, "Номер_участка", "Тип_почвы", участок.Номер_участка);
            }catch{
                return RedirectToAction("Index");
            }
            return View(участок);
        }

        // GET: Участок/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Участок участок = db.Участок.Find(id);
                if (участок == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер_участка = new SelectList(db.Загоны, "Номер_участка", "Тип_животных", участок.Номер_участка);
                ViewBag.Номер_участка = new SelectList(db.Поля_и_теплицы, "Номер_участка", "Тип_почвы", участок.Номер_участка);
                return View(участок);
        }

        // POST: Участок/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_участка,Площадь,Эффективность,Тип_участка")] Участок участок)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(участок).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер_участка = new SelectList(db.Загоны, "Номер_участка", "Тип_животных", участок.Номер_участка);
                ViewBag.Номер_участка = new SelectList(db.Поля_и_теплицы, "Номер_участка", "Тип_почвы", участок.Номер_участка);
            }
            catch {
                return RedirectToAction("Index");
            }
            return View(участок);
        }

        // GET: Участок/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Участок участок = db.Участок.Find(id);
                if (участок == null)
                {
                    return HttpNotFound();
                }
                return View(участок);
        }

        // POST: Участок/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Участок участок = db.Участок.Find(id);
                db.Участок.Remove(участок);
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
