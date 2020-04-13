using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proyectoFinal.Models;

namespace proyectoFinal.Controllers
{
    public class existenciasController : Controller
    {
        private proyectoFinalEntities db = new proyectoFinalEntities();

        // GET: existencias
        public ActionResult Index()
        {
            var existencia = db.existencia.Include(e => e.producto).Include(e => e.proveedor);
            return View(existencia.ToList());
        }

        // GET: existencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            existencia existencia = db.existencia.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // GET: existencias/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto2 = new SelectList(db.producto, "Id", "Nombre");
            ViewBag.IdProveedor = new SelectList(db.proveedor, "Id", "Cedula");
            return View();
        }

        // POST: existencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdProveedor,IdProducto2,Documento,ctd_entrada,ctd_salida,Precio,Fecha,Existencia1,Fecha_vent")] existencia existencia)
        {
            if (ModelState.IsValid)
            {
                db.existencia.Add(existencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto2 = new SelectList(db.producto, "Id", "Nombre", existencia.IdProducto2);
            ViewBag.IdProveedor = new SelectList(db.proveedor, "Id", "Cedula", existencia.IdProveedor);
            return View(existencia);
        }

        // GET: existencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            existencia existencia = db.existencia.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto2 = new SelectList(db.producto, "Id", "Nombre", existencia.IdProducto2);
            ViewBag.IdProveedor = new SelectList(db.proveedor, "Id", "Cedula", existencia.IdProveedor);
            return View(existencia);
        }

        // POST: existencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdProveedor,IdProducto2,Documento,ctd_entrada,ctd_salida,Precio,Fecha,Existencia1,Fecha_vent")] existencia existencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(existencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto2 = new SelectList(db.producto, "Id", "Nombre", existencia.IdProducto2);
            ViewBag.IdProveedor = new SelectList(db.proveedor, "Id", "Cedula", existencia.IdProveedor);
            return View(existencia);
        }

        // GET: existencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            existencia existencia = db.existencia.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // POST: existencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            existencia existencia = db.existencia.Find(id);
            db.existencia.Remove(existencia);
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
