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
    public class PuestosController : Controller
    {
        private NOMINABDEntities db = new NOMINABDEntities();

        // GET: Puestos
        public ActionResult Index(string Criterio = null)
        {
            return View(db.Puestos.Where(p => Criterio == null ||
            p.NombrePuesto.StartsWith(Criterio) ||
            p.NivelMinimoSalario.ToString().StartsWith(Criterio) ||
            p.NivelMáximoSalario.ToString().StartsWith(Criterio) ||
            p.NivelRiesgo.ToString().StartsWith(Criterio)));
        }

        [Authorize(Roles = "Administrador, Consulta")]
        // GET: Puestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Puestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Puestos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPuesto,NombrePuesto,NivelRiesgo,NivelMinimoSalario,NivelMáximoSalario,Estado")] Puestos puestos)
        {
            if (!validarMontos(puestos.NivelMáximoSalario, puestos.NivelMinimoSalario))
            {
                ModelState.AddModelError("NivelMáximoSalario", "El salario maximo no puede ser menor al minimo");
            }

            if (ModelState.IsValid)
            {
                db.Puestos.Add(puestos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(puestos);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Puestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        // POST: Puestos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPuesto,NombrePuesto,NivelRiesgo,NivelMinimoSalario,NivelMáximoSalario,Estado")] Puestos puestos)
        {
            if (!validarMontos(puestos.NivelMáximoSalario, puestos.NivelMinimoSalario))
            {
                ModelState.AddModelError("NivelMáximoSalario", "El salario maximo no puede ser menor al minimo");
            }
            if (ModelState.IsValid)
            {
                db.Entry(puestos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(puestos);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Puestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        // POST: Puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Puestos puestos = db.Puestos.Find(id);
            db.Puestos.Remove(puestos);
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

        public static bool validarMontos(int SalarioMaximo, int SalarioMinimo){
            if (SalarioMaximo < SalarioMinimo)
            {
                return false;
            }
            else { return true; };
        }          
    }
}
