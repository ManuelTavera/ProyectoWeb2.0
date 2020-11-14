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
    public class EmpleadosController : Controller
    {
        private NOMINABDEntities db = new NOMINABDEntities();

        // GET: Empleados
        public ActionResult Index(string Criterio = null)
        {
            var empleados = db.Empleados.Include(e => e.Departamentos).Include(e => e.Puestos);
            var resultados = empleados.Where(p => Criterio == null ||
            p.Nombre.StartsWith(Criterio) ||
            p.SalarioMensual.ToString().StartsWith(Criterio) ||
            p.Cedula.StartsWith(Criterio) || 
            p.Departamentos.Nombre.StartsWith(Criterio) ||
            p.Puestos.NombrePuesto.StartsWith(Criterio));
  
            return View(resultados);
        }

        [Authorize(Roles = "Administrador, Consulta")]
        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "IdDepartamento", "Nombre");
            ViewBag.IdPuesto = new SelectList(db.Puestos, "IdPuesto", "NombrePuesto");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmpleado,Cedula,Nombre,IdDepartamento,IdPuesto,SalarioMensual,IdNomina,Estado")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "IdDepartamento", "Nombre", empleados.IdDepartamento);
            ViewBag.IdPuesto = new SelectList(db.Puestos, "IdPuesto", "NombrePuesto", empleados.IdPuesto);
            return View(empleados);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "IdDepartamento", "Nombre", empleados.IdDepartamento);
            ViewBag.IdPuesto = new SelectList(db.Puestos, "IdPuesto", "NombrePuesto", empleados.IdPuesto);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpleado,Cedula,Nombre,IdDepartamento,IdPuesto,SalarioMensual,IdNomina,Estado")] Empleados empleados)
        {
            if (!validaCedula(empleados.Cedula))
            {
                ModelState.AddModelError("Cedula", "Esta cedula no es valida");
            }

            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDepartamento = new SelectList(db.Departamentos, "IdDepartamento", "Nombre", empleados.IdDepartamento);
            ViewBag.IdPuesto = new SelectList(db.Puestos, "IdPuesto", "NombrePuesto", empleados.IdPuesto);
            return View(empleados);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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

        public static bool validaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }

    }
}
