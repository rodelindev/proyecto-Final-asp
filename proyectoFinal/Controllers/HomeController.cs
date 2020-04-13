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
    public class HomeController : Controller
    {
        private proyectoFinalEntities db = new proyectoFinalEntities();

        // GET: Home
        public ActionResult Index()
        {
            var detalle_factura = db.detalle_factura.Include(d => d.factura).Include(d => d.producto);
            return View(detalle_factura.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_factura detalle_factura = db.detalle_factura.Find(id);
            if (detalle_factura == null)
            {
                return HttpNotFound();
            }
            return View(detalle_factura);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.IdFactura = new SelectList(db.factura, "Id", "Usuario");
            ViewBag.Idproducto = new SelectList(db.producto, "Id", "Nombre");
            return View();
        }

        // POST: Home/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdFactura,Idproducto,Cantidad,Precio,Fecha,Itbis")] detalle_factura detalle_factura)
        {
            if (ModelState.IsValid)
            {
                db.detalle_factura.Add(detalle_factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFactura = new SelectList(db.factura, "Id", "Usuario", detalle_factura.IdFactura);
            ViewBag.Idproducto = new SelectList(db.producto, "Id", "Nombre", detalle_factura.Idproducto);
            return View(detalle_factura);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_factura detalle_factura = db.detalle_factura.Find(id);
            if (detalle_factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFactura = new SelectList(db.factura, "Id", "Usuario", detalle_factura.IdFactura);
            ViewBag.Idproducto = new SelectList(db.producto, "Id", "Nombre", detalle_factura.Idproducto);
            return View(detalle_factura);
        }

        // POST: Home/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdFactura,Idproducto,Cantidad,Precio,Fecha,Itbis")] detalle_factura detalle_factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFactura = new SelectList(db.factura, "Id", "Usuario", detalle_factura.IdFactura);
            ViewBag.Idproducto = new SelectList(db.producto, "Id", "Nombre", detalle_factura.Idproducto);
            return View(detalle_factura);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_factura detalle_factura = db.detalle_factura.Find(id);
            if (detalle_factura == null)
            {
                return HttpNotFound();
            }
            return View(detalle_factura);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalle_factura detalle_factura = db.detalle_factura.Find(id);
            db.detalle_factura.Remove(detalle_factura);
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
