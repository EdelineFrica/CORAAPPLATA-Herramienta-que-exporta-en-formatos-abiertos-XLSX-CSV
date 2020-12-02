using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppDatosAbiertos;
using ClosedXML.Excel;

namespace AppDatosAbiertos.Controllers
{
    public class RecursoClorogasSulfatoController : Controller
    {
        private DatosAbiertosEntities db = new DatosAbiertosEntities();

        // GET: RecursoClorogasSulfato
        public ActionResult Index()
        {
            var recursoClorogasSulfato = db.RecursoClorogasSulfato.Include(r => r.ConjuntodeDato).Include(r => r.Mes);
            return View(recursoClorogasSulfato.ToList());
        }

        public void ExportarCSV()
        {
            StringWriter strw = new StringWriter();

            strw.WriteLine("\"AÑO\";\"MES\";\"CONSUMO CLORO GAS\";\"CONSUMO SULFATO\"");

            Response.ClearContent();


            Response.AddHeader("content-disposition",
                string.Format("attachment;filename=Consumo_cloro_gas_y_sulfato.csv"));

            var listaClorogasySulfato = db.RecursoClorogasSulfato.OrderBy(x => x.IdRecursoCGasSulfato).ToList();
            foreach (var ListaCGasySultato in listaClorogasySulfato)

            {
                strw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                    ListaCGasySultato.Anio, ListaCGasySultato.Mes.NombreMes, ListaCGasySultato.CCloroGas, ListaCGasySultato.CSulfato));
            }

            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            Response.Write(strw.ToString());
            Response.End();
        }

        public ActionResult ExportarAxlsx()
        {


            DataTable dt = new DataTable("RecursoClorogasSulfato");
            dt.Columns.AddRange(new DataColumn[4]   {  new DataColumn("AÑO", typeof(Int32)),
                new DataColumn("MES", typeof(string)),
                new DataColumn("CONSUMO CLORO GAS", typeof(decimal)),
                new DataColumn("CONSUMO SULFATO", typeof(decimal))});


            var listaClorogasySulfato = db.RecursoClorogasSulfato.OrderBy(x => x.IdRecursoCGasSulfato).ToList();

            foreach (var Lista in listaClorogasySulfato)
            {

                dt.Rows.Add(Lista.Anio, Lista.Mes.NombreMes, Lista.CCloroGas, Lista.CSulfato);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add("Consumo_cloro_gas_y_sulfato");
                ws.FirstCell().InsertTable(dt).Theme = XLTableTheme.None;

                ws.Tables.FirstOrDefault().ShowAutoFilter = false;


                //wb.Worksheets.Add(dt);


                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Consumoclorogasysulfato.xlsx");
                }
            }


        }


        // GET: RecursoClorogasSulfato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoClorogasSulfato recursoClorogasSulfato = db.RecursoClorogasSulfato.Find(id);
            if (recursoClorogasSulfato == null)
            {
                return HttpNotFound();
            }
            return View(recursoClorogasSulfato);
        }

        // GET: RecursoClorogasSulfato/Create
        public ActionResult Create()
        {
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto");
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes");
            return View();
        }

        // POST: RecursoClorogasSulfato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRecursoCGasSulfato,IdConjunto,Anio,IdMes,CCloroGas,CSulfato")] RecursoClorogasSulfato recursoClorogasSulfato)
        {
            if (ModelState.IsValid)
            {
                db.RecursoClorogasSulfato.Add(recursoClorogasSulfato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoClorogasSulfato.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoClorogasSulfato.IdMes);
            return View(recursoClorogasSulfato);
        }

        // GET: RecursoClorogasSulfato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoClorogasSulfato recursoClorogasSulfato = db.RecursoClorogasSulfato.Find(id);
            if (recursoClorogasSulfato == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoClorogasSulfato.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoClorogasSulfato.IdMes);
            return View(recursoClorogasSulfato);
        }

        // POST: RecursoClorogasSulfato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecursoCGasSulfato,IdConjunto,Anio,IdMes,CCloroGas,CSulfato")] RecursoClorogasSulfato recursoClorogasSulfato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursoClorogasSulfato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoClorogasSulfato.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoClorogasSulfato.IdMes);
            return View(recursoClorogasSulfato);
        }

        // GET: RecursoClorogasSulfato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoClorogasSulfato recursoClorogasSulfato = db.RecursoClorogasSulfato.Find(id);
            if (recursoClorogasSulfato == null)
            {
                return HttpNotFound();
            }
            return View(recursoClorogasSulfato);
        }

        // POST: RecursoClorogasSulfato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoClorogasSulfato recursoClorogasSulfato = db.RecursoClorogasSulfato.Find(id);
            db.RecursoClorogasSulfato.Remove(recursoClorogasSulfato);
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
