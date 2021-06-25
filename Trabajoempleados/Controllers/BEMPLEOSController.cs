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
    public class BEMPLEOSController : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: BEMPLEOS
        public ActionResult Index()
        {
            return View(db.EMPLEOS.ToList());
        }

        // GET: BEMPLEOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            if (eMPLEOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEOS);
        }

        // GET: BEMPLEOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BEMPLEOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEmpleo,IdEmpresa,Empresa,Tipo,Logo,Posicion,Ubicacion,Categoria,Descripcion,caplicar,Email,Fechapubli")] EMPLEOS eMPLEOS)
        {
            if (ModelState.IsValid)
            {
                db.EMPLEOS.Add(eMPLEOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eMPLEOS);
        }

        // GET: BEMPLEOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            if (eMPLEOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEOS);
        }

        // POST: BEMPLEOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEmpleo,IdEmpresa,Empresa,Tipo,Logo,Posicion,Ubicacion,Categoria,Descripcion,caplicar,Email,Fechapubli")] EMPLEOS eMPLEOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLEOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eMPLEOS);
        }

        // GET: BEMPLEOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            if (eMPLEOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEOS);
        }

        // POST: BEMPLEOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            db.EMPLEOS.Remove(eMPLEOS);
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
