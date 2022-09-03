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

    public class ЗаказыController : Controller
    {
        private kursachV2Entities db = new kursachV2Entities();

        // GET: ЗаказыW
        public ActionResult IndexW()
        {
            return View(db.Заказы.ToList());
        }

        // GET: ЗаказыU
        public ActionResult IndexU()
        {
            return View(db.Заказы.ToList());
        }

        // GET: Заказы
        public ActionResult Index()
        {
            return View(db.Заказы.ToList());
        }

        // POST: Замеры
        [HttpPost]
        public ActionResult Index(string Usl, string Soob, string Pok, string Procen)
        {
            var заказы = db.Заказы.Include(з => з.Продукция);
            try
            {
                if ((Usl != "") && (Soob != "") && (Pok != "") && (Procen != "") )
                {
                    using (kursachV2Entities db = new kursachV2Entities())
                    {
                        System.Data.SqlClient.SqlParameter[] parameters = new System.Data.SqlClient.SqlParameter[]
                        {
                        new System.Data.SqlClient.SqlParameter("@usl", Usl),
                        new System.Data.SqlClient.SqlParameter("@soob", Soob),
                        new System.Data.SqlClient.SqlParameter("@pok", Pok),
                        new System.Data.SqlClient.SqlParameter("@procen", Procen)
                        };
                        var orders = db.Database.SqlQuery<Заказы>("SALE @usl, @soob, @pok, @procen", parameters[0], parameters[1], parameters[2], parameters[3]);
                        foreach (var a in orders)
                            Trace.WriteLine($"{a.Номер} - {a.Отзыв} - {a.Суммарная_стоимость}");
                    }
                }
            }
            catch
            {
                ViewBag.activityCount = "Поля заполнены неверно";
            }

            return View(заказы.ToList());
        }

        // GET: Заказы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = db.Заказы.Find(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
            return View(заказы);
        }

        // GET: Заказы/Create
        public ActionResult Create()
        {
            Заказы def = new Заказы { Статус_оплаты=1, Отзыв="Хорошо" , Тип_заказа=1 , Паспорт_заказчика="1111111111" , Дата_заказа=DateTime.Parse("2020/06/12") , Суммарная_стоимость=400 , Колличество=1 , ФИО_заказчика="Иванов Иван Иванович"};
            return View(def);
        }

        // GET: Заказы/CreateW
        public ActionResult CreateW()
        {
            Заказы def = new Заказы { Статус_оплаты = 1, Отзыв = "Хорошо", Тип_заказа = 1, Паспорт_заказчика = "1111111111", Дата_заказа = DateTime.Parse("2020/06/12"), Суммарная_стоимость = 400, Колличество = 1, ФИО_заказчика = "Иванов Иван Иванович" };
            return View(def);
        }

        // POST: Заказы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Статус_оплаты,Отзыв,Тип_заказа,Номер,Паспорт_заказчика,Дата_заказа,Суммарная_стоимость,Колличество,ФИО_заказчика")] Заказы заказы)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Заказы.Add(заказы);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch {
                    return View("Error");
                }
                
            }

            return View(заказы);
        }
        // POST: Заказы/CreateW
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateW([Bind(Include = "Статус_оплаты,Отзыв,Тип_заказа,Номер,Паспорт_заказчика,Дата_заказа,Суммарная_стоимость,Колличество,ФИО_заказчика")] Заказы заказы)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Заказы.Add(заказы);
                    db.SaveChanges();
                    return RedirectToAction("IndexW");
                }
                catch
                {
                    return View("Error");
                }

            }

            return View(заказы);
        }
        // GET: Заказы/Edit/5
        public ActionResult Edit(int? id)
        {
            Заказы заказы = db.Заказы.Find(id);
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (заказы == null)
                {
                    return HttpNotFound();
                }
            }
            catch {
                ViewBag.activityCount = "Ошибка";
            }
                return View(заказы);
        }

        // POST: Заказы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Статус_оплаты,Отзыв,Тип_заказа,Номер,Паспорт_заказчика,Дата_заказа,Суммарная_стоимость,Колличество,ФИО_заказчика")] Заказы заказы)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(заказы).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch {
                    return View("Error");
                }
                
            }
            return View(заказы);
        }

        // GET: Заказы/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Заказы заказы = db.Заказы.Find(id);
                if (заказы == null)
                {
                    return HttpNotFound();
                }
                return View(заказы);
        }

        // POST: Заказы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Заказы заказы = db.Заказы.Find(id);
            try
            {
                db.Заказы.Remove(заказы);
            db.SaveChanges();
            }
            catch
            {
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
