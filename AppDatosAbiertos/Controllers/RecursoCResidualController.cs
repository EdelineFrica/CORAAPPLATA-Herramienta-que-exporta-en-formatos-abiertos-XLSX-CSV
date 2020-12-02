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
    public class RecursoCResidualController : Controller
    {
        private DatosAbiertosEntities db = new DatosAbiertosEntities();

        // GET: RecursoCResidual
        public ActionResult Index()
        {
            var recursoCResidual = db.RecursoCResidual.Include(r => r.ConjuntodeDato).Include(r => r.Mes);
            return View(recursoCResidual.ToList());
        }


        public void ExportarCSV()
        {
            StringWriter strw = new StringWriter();

            strw.WriteLine("\"AÑO\";\"MES\";\"PUERTO PLATA\";\"SOSUA\";\"MONTELLANO\";\"LA ISABELA\";\"ALTAMIRA\";\"LUPERON\";\"LOS HIDALGOS\";\"GUANANICO\";\"IMBERT\"");


            Response.ClearContent();


            Response.AddHeader("content-disposition",
                string.Format("attachment;filename=Consumo_cloro_residual.csv"));

            var listacResidual = db.RecursoCResidual.OrderBy(x => x.IdRecursoCResidual).ToList();
            foreach (var ListaResidual in listacResidual)

            {
                strw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\"",
                    ListaResidual.Anio, ListaResidual.Mes.NombreMes, ListaResidual.AcPuertoPlata, ListaResidual.AcSosua, ListaResidual.AcMontellano, ListaResidual.AcLaIsabela, ListaResidual.AcAltamira, ListaResidual.AcLuperon, ListaResidual.AcLosHidalgos, ListaResidual.AcGuananico, ListaResidual.AcImbert));
            }

            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            Response.Write(strw.ToString());
            Response.End();



        }





        //metodo para exportar a formato xlsx 
        public ActionResult ExportarAxlsx()
        {


            DataTable dt = new DataTable("RecursoCResidual");
            dt.Columns.AddRange(new DataColumn[11]   {  new DataColumn("AÑO", typeof(Int32)),
                new DataColumn("MES", typeof(string)),
                new DataColumn("PUERTO PLATA", typeof(decimal)),
                new DataColumn("SOSUA", typeof(decimal)),
                new DataColumn("MONTELLANO", typeof(decimal)),
                new DataColumn("LA ISABELA", typeof(decimal)),
                new DataColumn("ALTAMIRA", typeof(decimal)),
                new DataColumn("LUPERON", typeof(decimal)),
                new DataColumn("LOS HIDALGOS", typeof(decimal)),
                new DataColumn("GUANANICO", typeof(decimal)),
                new DataColumn("IMBERT", typeof(decimal))});


            var listacResidual = db.RecursoCResidual.OrderBy(x => x.IdRecursoCResidual).ToList();

            foreach (var Lista in listacResidual)
            {

                dt.Rows.Add(Lista.Anio, Lista.Mes.NombreMes, Lista.AcPuertoPlata, Lista.AcSosua, Lista.AcMontellano, Lista.AcLaIsabela, Lista.AcAltamira, Lista.AcLuperon, Lista.AcLosHidalgos, Lista.AcGuananico, Lista.AcImbert);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add("Consumo_cloro_residual");
                ws.FirstCell().InsertTable(dt).Theme = XLTableTheme.None;

                ws.Tables.FirstOrDefault().ShowAutoFilter = false;


                //wb.Worksheets.Add(dt);


                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ConsumoCloroResidual.xlsx");
                }
            }


        }



        // GET: RecursoCResidual/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoCResidual recursoCResidual = db.RecursoCResidual.Find(id);
            if (recursoCResidual == null)
            {
                return HttpNotFound();
            }
            return View(recursoCResidual);
        }

        // GET: RecursoCResidual/Create
        public ActionResult Create()
        {
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto");
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes");
            return View();
        }

        // POST: RecursoCResidual/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRecursoCResidual,IdConjunto,Anio,IdMes,AcPuertoPlata,AcSosua,AcMontellano,AcLaIsabela,AcAltamira,AcLuperon,AcLosHidalgos,AcGuananico,AcImbert")] RecursoCResidual recursoCResidual)
        {
            if (ModelState.IsValid)
            {
                db.RecursoCResidual.Add(recursoCResidual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoCResidual.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoCResidual.IdMes);
            return View(recursoCResidual);
        }

        // GET: RecursoCResidual/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoCResidual recursoCResidual = db.RecursoCResidual.Find(id);
            if (recursoCResidual == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoCResidual.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoCResidual.IdMes);
            return View(recursoCResidual);
        }

        // POST: RecursoCResidual/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecursoCResidual,IdConjunto,Anio,IdMes,AcPuertoPlata,AcSosua,AcMontellano,AcLaIsabela,AcAltamira,AcLuperon,AcLosHidalgos,AcGuananico,AcImbert")] RecursoCResidual recursoCResidual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursoCResidual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoCResidual.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoCResidual.IdMes);
            return View(recursoCResidual);
        }

        // GET: RecursoCResidual/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoCResidual recursoCResidual = db.RecursoCResidual.Find(id);
            if (recursoCResidual == null)
            {
                return HttpNotFound();
            }
            return View(recursoCResidual);
        }

        // POST: RecursoCResidual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoCResidual recursoCResidual = db.RecursoCResidual.Find(id);
            db.RecursoCResidual.Remove(recursoCResidual);
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
