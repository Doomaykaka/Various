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

    public class ПродукцияController : Controller
    {
        // GET: ПродукцияW
        public ActionResult IndexW(string PrPriceS, string PrPriceF, string PrName)
        {
            var продукция = db.Продукция.Include(п => п.Заказы).Include(п => п.Участок);

            if (!String.IsNullOrEmpty(PrName))
            {
                продукция = продукция.Where(x => x.Название_продукта == PrName);
            }

            try
            {
                if (!String.IsNullOrEmpty(PrPriceS) && !String.IsNullOrEmpty(PrPriceF))
                {
                    decimal prc1 = decimal.Parse(PrPriceS);
                    decimal prc2 = decimal.Parse(PrPriceF);
                    продукция = продукция.Where(x => x.Стоимость > prc1 && x.Стоимость < prc2);
                }
            }
            catch
            {
                ViewBag.ProdFieldErr = "Ошибка заполнения полей";
            }



            return View(продукция.ToList());
        }

        private kursachV2Entities db = new kursachV2Entities();
        // GET: ПродукцияU
        public ActionResult IndexU() {
            var продукция = db.Продукция.Include(п => п.Заказы).Include(п => п.Участок);
            return View(продукция.ToList());
        }
        // GET: Продукция
        public ActionResult Index(string PrPriceS , string PrPriceF, string PrName , string Stat , string Good , string Bad)
        {
            var продукция = db.Продукция.Include(п => п.Заказы).Include(п => п.Участок);

            if (!String.IsNullOrEmpty(PrName))
            {
                продукция = продукция.Where(x => x.Название_продукта == PrName);
            }

            try
            {
                if (!String.IsNullOrEmpty(PrPriceS) && !String.IsNullOrEmpty(PrPriceF))
                {
                    decimal prc1 = decimal.Parse(PrPriceS);
                    decimal prc2 = decimal.Parse(PrPriceF);
                    продукция = продукция.Where(x => x.Стоимость > prc1 && x.Стоимость < prc2);
                }

                if (!String.IsNullOrEmpty(Stat) && !String.IsNullOrEmpty(Good) && !String.IsNullOrEmpty(Bad))
                {
                    using (kursachV2Entities db = new kursachV2Entities())
                    {
                        System.Data.SqlClient.SqlParameter[] parameters = new System.Data.SqlClient.SqlParameter[]
                        {
                        new System.Data.SqlClient.SqlParameter("@stat", Stat),
                        new System.Data.SqlClient.SqlParameter("@good", Good),
                        new System.Data.SqlClient.SqlParameter("@bad", Bad)
                        };
                        db.Database.ExecuteSqlCommand("PROD @stat, @good, @bad", parameters[0], parameters[1], parameters[2]);
                    }
                }
            }
            catch {
                ViewBag.ProdFieldErr = "Ошибка заполнения полей";
            }

            

            return View(продукция.ToList());
        }

        // GET: Продукция/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Продукция продукция = db.Продукция.Find(id);
            if (продукция == null)
            {
                return HttpNotFound();
            }
            return View(продукция);
        }

        // GET: Продукция/CreateW
        public ActionResult CreateW()
        {
            ViewBag.Номер = new SelectList(db.Заказы, "Номер", "Отзыв");
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Продукция def = new Продукция { Объем_продукции = 10, Дата_сбора = DateTime.Parse("2020/03/12"), Название_продукта = "Картофель", Срок_годности = DateTime.Parse("2020/06/12"), Дата_производства = DateTime.Parse("2020/03/15"), Тип_животноводческая_растительная_ = true, Сорт_продукции = 2, Стоимость = 400 };
            return View(def);
        }

        // GET: Продукция/Create
        public ActionResult Create()
        {
            ViewBag.Номер = new SelectList(db.Заказы, "Номер", "Отзыв");
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка");
            Продукция def = new Продукция { Объем_продукции=10 , Дата_сбора=DateTime.Parse("2020/03/12") , Название_продукта="Картофель" , Срок_годности= DateTime.Parse("2020/06/12"), Дата_производства= DateTime.Parse("2020/03/15"), Тип_животноводческая_растительная_=true,Сорт_продукции=2,Стоимость=400};
            return View(def);
        }

        // POST: Продукция/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Номер,Объем_продукции,Дата_сбора,Номер_участка,Название_продукта,Срок_годности,Дата_производства,Тип_животноводческая_растительная_,Сорт_продукции,Стоимость")] Продукция продукция)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Продукция.Add(продукция);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }
                catch
                {
                    return RedirectToAction("IndexW");
                }

            }

            ViewBag.Номер = new SelectList(db.Заказы, "Номер", "Отзыв", продукция.Номер);
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", продукция.Номер_участка);
            return View(продукция);
        }

        // POST: Продукция/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер,Объем_продукции,Дата_сбора,Номер_участка,Название_продукта,Срок_годности,Дата_производства,Тип_животноводческая_растительная_,Сорт_продукции,Стоимость")] Продукция продукция)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Продукция.Add(продукция);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch {
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.Номер = new SelectList(db.Заказы, "Номер", "Отзыв", продукция.Номер);
            ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", продукция.Номер_участка);
            return View(продукция);
        }

        // GET: Продукция/Edit/5
        public ActionResult Edit(string id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Продукция продукция = db.Продукция.Find(id);
                if (продукция == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Номер = new SelectList(db.Заказы, "Номер", "Отзыв", продукция.Номер);
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", продукция.Номер_участка);
                return View(продукция);
        }

        // POST: Продукция/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер,Объем_продукции,Дата_сбора,Номер_участка,Название_продукта,Срок_годности,Дата_производства,Тип_животноводческая_растительная_,Сорт_продукции,Стоимость")] Продукция продукция)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(продукция).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Номер = new SelectList(db.Заказы, "Номер", "Отзыв", продукция.Номер);
                ViewBag.Номер_участка = new SelectList(db.Участок, "Номер_участка", "Тип_участка", продукция.Номер_участка);
            }
            catch {
                ViewBag.ProdFieldErr = "Ошибка";
            }
            return View(продукция);
        }

        // GET: Продукция/Delete/5
        public ActionResult Delete(string id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Продукция продукция = db.Продукция.Find(id);
                if (продукция == null)
                {
                    return HttpNotFound();
                }
                return View(продукция);
        }

        // POST: Продукция/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Продукция продукция = db.Продукция.Find(id);
                db.Продукция.Remove(продукция);
                db.SaveChanges();
            }
            catch {
                ViewBag.ProdFieldErr = "Ошибка";
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
