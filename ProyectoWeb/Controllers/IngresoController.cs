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
    public class IngresoController : Controller
    {
        private NOMINABDEntities db = new NOMINABDEntities();

        // GET: Ingreso
        public ActionResult Index(string Criterio = null)
        {
            return View(db.Ingreso.Where(p=> Criterio == null ||
            p.NombreIngreso.StartsWith(Criterio)));
        }

        [Authorize(Roles = "Administrador, Consulta")]
        // GET: Ingreso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingreso ingreso = db.Ingreso.Find(id);
            if (ingreso == null)
            {
                return HttpNotFound();
            }
            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Ingreso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingreso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIngreso,NombreIngreso,DependeSalario,Estado")] Ingreso ingreso)
        {
            if (ModelState.IsValid)
            {
                db.Ingreso.Add(ingreso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Ingreso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingreso ingreso = db.Ingreso.Find(id);
            if (ingreso == null)
            {
                return HttpNotFound();
            }
            return View(ingreso);
        }

        // POST: Ingreso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIngreso,NombreIngreso,DependeSalario,Estado")] Ingreso ingreso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingreso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Ingreso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingreso ingreso = db.Ingreso.Find(id);
            if (ingreso == null)
            {
                return HttpNotFound();
            }
            return View(ingreso);
        }

        // POST: Ingreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingreso ingreso = db.Ingreso.Find(id);
            db.Ingreso.Remove(ingreso);
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
