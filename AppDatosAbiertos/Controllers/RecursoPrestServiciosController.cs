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
    public class RecursoPrestServiciosController : Controller
    {
        private DatosAbiertosEntities db = new DatosAbiertosEntities();

        // GET: RecursoPrestServicios
        public ActionResult Index()
        {
            var recursoPrestServicios = db.RecursoPrestServicios.Include(r => r.ConjuntodeDato).Include(r => r.Mes);
            return View(recursoPrestServicios.ToList());
        }

        public void ExportarCSV()
        {
            StringWriter strw = new StringWriter();

            strw.WriteLine("\"AÑO\";\"MES\";\"ACUERDOS DE PAGO\";\"NUEVOS USUARIOS\";\"RECONEXIONES\"");

            Response.ClearContent();


            Response.AddHeader("content-disposition",
                string.Format("attachment;filename=Prestacion_del_Servicio.csv"));


            var prestServicio = db.RecursoPrestServicios.OrderBy(x => x.IdRecursoPServicios).ToList();
            foreach (var servicio in prestServicio)

            {
                strw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\"",
                    servicio.Anio, servicio.Mes.NombreMes, servicio.AcuerdoPago, servicio.NuevosUsuarios, servicio.Reconexiones));
            }

            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            Response.Write(strw.ToString());
            Response.End();
        }



        public ActionResult ExportarAxlsx()
        {


            DataTable dt = new DataTable("RecursoPrestServicios");
            dt.Columns.AddRange(new DataColumn[5]   {  new DataColumn("AÑO", typeof(Int32)),
                new DataColumn("MES", typeof(string)),
                new DataColumn("ACUERDOS DE PAGO", typeof(int)),
                new DataColumn("NUEVOS USUARIOS", typeof(int)),
                new DataColumn("RECONEXIONES", typeof(int))});


            var prestServicio = db.RecursoPrestServicios.OrderBy(x => x.IdRecursoPServicios).ToList();

            foreach (var Lista in prestServicio)
            {

                dt.Rows.Add(Lista.Anio, Lista.Mes.NombreMes, Lista.AcuerdoPago, Lista.NuevosUsuarios, Lista.Reconexiones);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add("Prestacion_del_servicio");
                ws.FirstCell().InsertTable(dt).Theme = XLTableTheme.None;

                ws.Tables.FirstOrDefault().ShowAutoFilter = false;


                //wb.Worksheets.Add(dt);


                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Prestaciondelservicio.xlsx");
                }
            }


        }



        // GET: RecursoPrestServicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoPrestServicios recursoPrestServicios = db.RecursoPrestServicios.Find(id);
            if (recursoPrestServicios == null)
            {
                return HttpNotFound();
            }
            return View(recursoPrestServicios);
        }

        // GET: RecursoPrestServicios/Create
        public ActionResult Create()
        {
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto");
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes");
            return View();
        }

        // POST: RecursoPrestServicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRecursoPServicios,IdConjunto,Anio,IdMes,AcuerdoPago,NuevosUsuarios,Reconexiones")] RecursoPrestServicios recursoPrestServicios)
        {
            if (ModelState.IsValid)
            {
                db.RecursoPrestServicios.Add(recursoPrestServicios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoPrestServicios.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoPrestServicios.IdMes);
            return View(recursoPrestServicios);
        }

        // GET: RecursoPrestServicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoPrestServicios recursoPrestServicios = db.RecursoPrestServicios.Find(id);
            if (recursoPrestServicios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoPrestServicios.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoPrestServicios.IdMes);
            return View(recursoPrestServicios);
        }

        // POST: RecursoPrestServicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecursoPServicios,IdConjunto,Anio,IdMes,AcuerdoPago,NuevosUsuarios,Reconexiones")] RecursoPrestServicios recursoPrestServicios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursoPrestServicios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoPrestServicios.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoPrestServicios.IdMes);
            return View(recursoPrestServicios);
        }

        // GET: RecursoPrestServicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoPrestServicios recursoPrestServicios = db.RecursoPrestServicios.Find(id);
            if (recursoPrestServicios == null)
            {
                return HttpNotFound();
            }
            return View(recursoPrestServicios);
        }

        // POST: RecursoPrestServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoPrestServicios recursoPrestServicios = db.RecursoPrestServicios.Find(id);
            db.RecursoPrestServicios.Remove(recursoPrestServicios);
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
