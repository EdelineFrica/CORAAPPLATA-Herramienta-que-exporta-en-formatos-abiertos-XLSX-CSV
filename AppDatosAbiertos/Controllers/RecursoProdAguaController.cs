using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AppDatosAbiertos;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;


namespace AppDatosAbiertos.Controllers
{
    public class RecursoProdAguaController : Controller
    {
        private DatosAbiertosEntities db = new DatosAbiertosEntities();

        // GET: RecursoProdAgua
        public ActionResult Index()
        {
            var recursoProdAgua = db.RecursoProdAgua.Include(r => r.ConjuntodeDato).Include(r => r.Mes);
            return View(recursoProdAgua.ToList());
        }

        //metodo para exportar en formato csv
        public void ExportarCSV()
        {
            StringWriter strw = new StringWriter();

            strw.WriteLine("\"AÑO\";\"MES\";\"GALONES POR MES\";\"METRO CUBICO POR MES\"");
         
            Response.ClearContent();

            
            Response.AddHeader("content-disposition",
                string.Format("attachment;filename=Produccion_de_agua.csv"));

            var listaProdAgua = db.RecursoProdAgua.OrderBy(x => x.IdRecursoPAgua).ToList();
            foreach (var recursoProdAgua in listaProdAgua)

            {
                strw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                    recursoProdAgua.Anio, recursoProdAgua.Mes.NombreMes, recursoProdAgua.GalonesxMes, recursoProdAgua.Mts3Mes));
            }

            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            Response.Write(strw.ToString());
            Response.End();
        }

       





       //metodo para exportar a formato xlsx 
        public ActionResult ExportarAxlsx()
        {

       
            DataTable dt = new DataTable("RecursoProdAgua");
            dt.Columns.AddRange(new DataColumn[4]   {  new DataColumn("AÑO", typeof(Int32)),
                new DataColumn("MES", typeof(string)),
                new DataColumn("GALONES POR MES", typeof(decimal)),
                new DataColumn("METRO CUBICO POR MES", typeof(decimal))});


            var listaProdAgua = db.RecursoProdAgua.OrderBy(x => x.IdRecursoPAgua).ToList();

            foreach (var Lista in listaProdAgua)
            {
                
                dt.Rows.Add(Lista.Anio, Lista.Mes.NombreMes, Lista.GalonesxMes, Lista.Mts3Mes);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add("Produccion_de_agua");
                ws.FirstCell().InsertTable(dt).Theme = XLTableTheme.None;

                ws.Tables.FirstOrDefault().ShowAutoFilter = false;


                //wb.Worksheets.Add(dt);


                using (MemoryStream stream = new MemoryStream())
                {
                   
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Producciondegua.xlsx");
                }
            }

           
        }

       

       

        // GET: RecursoProdAgua/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoProdAgua recursoProdAgua = db.RecursoProdAgua.Find(id);
            if (recursoProdAgua == null)
            {
                return HttpNotFound();
            }
            return View(recursoProdAgua);
        }

        // GET: RecursoProdAgua/Create
        public ActionResult Create()
        {
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto");
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes");
            return View();
        }

        // POST: RecursoProdAgua/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRecursoPAgua,IdConjunto,Anio,IdMes,GalonesxMes,Mts3Mes")] RecursoProdAgua recursoProdAgua)
        {
            if (ModelState.IsValid)
            {
                db.RecursoProdAgua.Add(recursoProdAgua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoProdAgua.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoProdAgua.IdMes);
            return View(recursoProdAgua);
        }

        // GET: RecursoProdAgua/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoProdAgua recursoProdAgua = db.RecursoProdAgua.Find(id);
            if (recursoProdAgua == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoProdAgua.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoProdAgua.IdMes);
            return View(recursoProdAgua);
        }

        // POST: RecursoProdAgua/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecursoPAgua,IdConjunto,Anio,IdMes,GalonesxMes,Mts3Mes")] RecursoProdAgua recursoProdAgua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursoProdAgua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConjunto = new SelectList(db.ConjuntodeDato, "IdConjunto", "NombreConjunto", recursoProdAgua.IdConjunto);
            ViewBag.IdMes = new SelectList(db.Mes, "IdMes", "NombreMes", recursoProdAgua.IdMes);
            return View(recursoProdAgua);
        }

        // GET: RecursoProdAgua/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoProdAgua recursoProdAgua = db.RecursoProdAgua.Find(id);
            if (recursoProdAgua == null)
            {
                return HttpNotFound();
            }
            return View(recursoProdAgua);
        }

        // POST: RecursoProdAgua/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoProdAgua recursoProdAgua = db.RecursoProdAgua.Find(id);
            db.RecursoProdAgua.Remove(recursoProdAgua);
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
