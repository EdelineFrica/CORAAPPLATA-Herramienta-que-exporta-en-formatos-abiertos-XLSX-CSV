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
    public class ConjuntodeDatosController : Controller
    {
        private DatosAbiertosEntities db = new DatosAbiertosEntities();

        // GET: ConjuntodeDatos
        public ActionResult Index()
        {
            return View(db.ConjuntodeDato.ToList());
        }

        // GET: ConjuntodeDatos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConjuntodeDato conjuntodeDato = db.ConjuntodeDato.Find(id);
            if (conjuntodeDato == null)
            {
                return HttpNotFound();
            }
            return View(conjuntodeDato);
        }

        // GET: ConjuntodeDatos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConjuntodeDatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConjunto,NombreConjunto,DescripcionConjunto,Url")] ConjuntodeDato conjuntodeDato)
        {
            if (ModelState.IsValid)
            {
                db.ConjuntodeDato.Add(conjuntodeDato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conjuntodeDato);
        }

        // GET: ConjuntodeDatos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConjuntodeDato conjuntodeDato = db.ConjuntodeDato.Find(id);
            if (conjuntodeDato == null)
            {
                return HttpNotFound();
            }
            return View(conjuntodeDato);
        }

        // POST: ConjuntodeDatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdConjunto,NombreConjunto,DescripcionConjunto,Url")] ConjuntodeDato conjuntodeDato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conjuntodeDato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conjuntodeDato);
        }

        // GET: ConjuntodeDatos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConjuntodeDato conjuntodeDato = db.ConjuntodeDato.Find(id);
            if (conjuntodeDato == null)
            {
                return HttpNotFound();
            }
            return View(conjuntodeDato);
        }

        // POST: ConjuntodeDatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConjuntodeDato conjuntodeDato = db.ConjuntodeDato.Find(id);
            db.ConjuntodeDato.Remove(conjuntodeDato);
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
