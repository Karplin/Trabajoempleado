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
    public class BCONTRATISTASController : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: BCONTRATISTAS
        public ActionResult Index()
        {
            return View(db.CONTRATISTAS.ToList());
        }

        // GET: BCONTRATISTAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            if (cONTRATISTAS == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATISTAS);
        }

        // GET: BCONTRATISTAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BCONTRATISTAS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdContratista,Rnc,NombreEmpresa,Representante,Correo,Telefono,Contrasena,Direccion,Urlc,Descripcion")] CONTRATISTAS cONTRATISTAS)
        {
            if (ModelState.IsValid)
            {
                db.CONTRATISTAS.Add(cONTRATISTAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cONTRATISTAS);
        }

        // GET: BCONTRATISTAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            if (cONTRATISTAS == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATISTAS);
        }

        // POST: BCONTRATISTAS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdContratista,Rnc,NombreEmpresa,Representante,Correo,Telefono,Contrasena,Direccion,Urlc,Descripcion")] CONTRATISTAS cONTRATISTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRATISTAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONTRATISTAS);
        }

        // GET: BCONTRATISTAS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            if (cONTRATISTAS == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATISTAS);
        }

        // POST: BCONTRATISTAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            db.CONTRATISTAS.Remove(cONTRATISTAS);
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
