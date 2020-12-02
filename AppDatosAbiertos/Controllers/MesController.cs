using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppDatosAbiertos;

namespace AppDatosAbiertos.Controllers
{
    public class MesController : Controller
    {
        private DatosAbiertosEntities db = new DatosAbiertosEntities();

        // GET: Mes
        public ActionResult Index()
        {
            return View(db.Mes.ToList());
        }

        // GET: Mes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mes mes = db.Mes.Find(id);
            if (mes == null)
            {
                return HttpNotFound();
            }
            return View(mes);
        }

        // GET: Mes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMes,NombreMes")] Mes mes)
        {
            if (ModelState.IsValid)
            {
                db.Mes.Add(mes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mes);
        }

        // GET: Mes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mes mes = db.Mes.Find(id);
            if (mes == null)
            {
                return HttpNotFound();
            }
            return View(mes);
        }

        // POST: Mes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMes,NombreMes")] Mes mes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mes);
        }

        // GET: Mes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mes mes = db.Mes.Find(id);
            if (mes == null)
            {
                return HttpNotFound();
            }
            return View(mes);
        }

        // POST: Mes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mes mes = db.Mes.Find(id);
            db.Mes.Remove(mes);
            db.SaveChanges();
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
