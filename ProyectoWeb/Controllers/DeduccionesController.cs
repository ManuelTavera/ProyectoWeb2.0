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
    public class DeduccionesController : Controller
    {
        private NOMINABDEntities db = new NOMINABDEntities();

        // GET: Deducciones
        public ActionResult Index()
        {
            return View(db.Deducciones.ToList());
        }

        [Authorize(Roles = "Administrador, Consulta")]
        // GET: Deducciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducciones deducciones = db.Deducciones.Find(id);
            if (deducciones == null)
            {
                return HttpNotFound();
            }
            return View(deducciones);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Deducciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deducciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDeduccion,NombreTipoDeducciones,DependeSalario,Estado")] Deducciones deducciones)
        {
            if (ModelState.IsValid)
            {
                db.Deducciones.Add(deducciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deducciones);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Deducciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducciones deducciones = db.Deducciones.Find(id);
            if (deducciones == null)
            {
                return HttpNotFound();
            }
            return View(deducciones);
        }

        // POST: Deducciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDeduccion,NombreTipoDeducciones,DependeSalario,Estado")] Deducciones deducciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deducciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deducciones);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Deducciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deducciones deducciones = db.Deducciones.Find(id);
            if (deducciones == null)
            {
                return HttpNotFound();
            }
            return View(deducciones);
        }

        // POST: Deducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deducciones deducciones = db.Deducciones.Find(id);
            db.Deducciones.Remove(deducciones);
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
