using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoWeb;

namespace ProyectoWeb.Controllers
{
    [Authorize]
    public class TransaccionesController : Controller
    {
        private NOMINABDEntities db = new NOMINABDEntities();

        // GET: Transacciones
        public ActionResult Index(string Criterio = null)
        {
            var transacciones = db.Transacciones.Include(t => t.Deducciones).Include(t => t.Empleados).Include(t => t.Ingreso);
            return View(transacciones.Where(p => Criterio == null ||
            p.TipoTransaccion.StartsWith(Criterio) ||
            p.Fecha.ToString().StartsWith(Criterio) ||
            p.Monto.ToString().StartsWith(Criterio) ||
            p.Deducciones.NombreTipoDeducciones.StartsWith(Criterio) ||
            p.Empleados.Cedula.StartsWith(Criterio) ||
            p.Ingreso.NombreIngreso.StartsWith(Criterio)));
        }

        [Authorize(Roles = "Administrador, Consulta")]
        // GET: Transacciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.Find(id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Transacciones/Create
        public ActionResult Create()
        {
            ViewBag.IdDeduccion = new SelectList(db.Deducciones, "IdDeduccion", "NombreTipoDeducciones");
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Cedula");
            ViewBag.IdIngreso = new SelectList(db.Ingreso, "IdIngreso", "NombreIngreso");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTransaccion,IdEmpleado,IdIngreso,IdDeduccion,TipoTransaccion,Fecha,Monto,Estado")] Transacciones transacciones)
        {
            if (ModelState.IsValid)
            {
                db.Transacciones.Add(transacciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDeduccion = new SelectList(db.Deducciones, "IdDeduccion", "NombreTipoDeducciones", transacciones.IdDeduccion);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Cedula", transacciones.IdEmpleado);
            ViewBag.IdIngreso = new SelectList(db.Ingreso, "IdIngreso", "NombreIngreso", transacciones.IdIngreso);
            return View(transacciones);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Transacciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.Find(id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDeduccion = new SelectList(db.Deducciones, "IdDeduccion", "NombreTipoDeducciones", transacciones.IdDeduccion);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Cedula", transacciones.IdEmpleado);
            ViewBag.IdIngreso = new SelectList(db.Ingreso, "IdIngreso", "NombreIngreso", transacciones.IdIngreso);
            return View(transacciones);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTransaccion,IdEmpleado,IdIngreso,IdDeduccion,TipoTransaccion,Fecha,Monto,Estado")] Transacciones transacciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transacciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDeduccion = new SelectList(db.Deducciones, "IdDeduccion", "NombreTipoDeducciones", transacciones.IdDeduccion);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Cedula", transacciones.IdEmpleado);
            ViewBag.IdIngreso = new SelectList(db.Ingreso, "IdIngreso", "NombreIngreso", transacciones.IdIngreso);
            return View(transacciones);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Transacciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.Find(id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transacciones transacciones = db.Transacciones.Find(id);
            db.Transacciones.Remove(transacciones);
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
