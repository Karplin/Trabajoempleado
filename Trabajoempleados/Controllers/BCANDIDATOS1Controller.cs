using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trabajoempleados.Models;

namespace Trabajoempleados.Controllers
{
    public class BCANDIDATOS1Controller : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: BCANDIDATOS1
        public ActionResult Index()
        {
            return View(db.CANDIDATOS.ToList());
        }

        // GET: BCANDIDATOS1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANDIDATOS cANDIDATOS = db.CANDIDATOS.Find(id);
            if (cANDIDATOS == null)
            {
                return HttpNotFound();
            }
            return View(cANDIDATOS);
        }

        // GET: BCANDIDATOS1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BCANDIDATOS1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCandidato,Cedula,Nombre,Apellido,Correo,Telefono,Contrasena,fechanacimiento")] CANDIDATOS cANDIDATOS)
        {
            if (ModelState.IsValid)
            {
                db.CANDIDATOS.Add(cANDIDATOS);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }

            return View(cANDIDATOS);
        }

        // GET: BCANDIDATOS1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANDIDATOS cANDIDATOS = db.CANDIDATOS.Find(id);
            if (cANDIDATOS == null)
            {
                return HttpNotFound();
            }
            return View(cANDIDATOS);
        }

        // POST: BCANDIDATOS1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCandidato,Cedula,Nombre,Apellido,Correo,Telefono,Contrasena,fechanacimiento")] CANDIDATOS cANDIDATOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cANDIDATOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ver", "BEMPLEOS1");
            }
            return View(cANDIDATOS);
        }

        // GET: BCANDIDATOS1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANDIDATOS cANDIDATOS = db.CANDIDATOS.Find(id);
            if (cANDIDATOS == null)
            {
                return HttpNotFound();
            }
            return View(cANDIDATOS);
        }

        // POST: BCANDIDATOS1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CANDIDATOS cANDIDATOS = db.CANDIDATOS.Find(id);
            db.CANDIDATOS.Remove(cANDIDATOS);
            db.SaveChanges();
            return RedirectToAction("Ver", "BEMPLEOS1");
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
