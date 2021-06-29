using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trabajoempleados.Models;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Trabajoempleados.Controllers
{
    public class BADMINISTRADORES1Controller : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: BADMINISTRADORES1
        public ActionResult Index()
        {
            return View(db.ADMINISTRADORES.ToList());
        }

        // GET: BADMINISTRADORES1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMINISTRADORES aDMINISTRADORES = db.ADMINISTRADORES.Find(id);
            if (aDMINISTRADORES == null)
            {
                return HttpNotFound();
            }
            return View(aDMINISTRADORES);
        }

        // GET: BADMINISTRADORES1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BADMINISTRADORES1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAdmin,Cedula,Nombre,Apellido,Correo,Contrasena,fechanacimiento")] ADMINISTRADORES aDMINISTRADORES)
        {

            
            if (ModelState.IsValid)
            {
                db.ADMINISTRADORES.Add(aDMINISTRADORES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aDMINISTRADORES);
        }

        // GET: BADMINISTRADORES1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMINISTRADORES aDMINISTRADORES = db.ADMINISTRADORES.Find(id);
            if (aDMINISTRADORES == null)
            {
                return HttpNotFound();
            }
            return View(aDMINISTRADORES);
        }

        // POST: BADMINISTRADORES1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAdmin,Cedula,Nombre,Apellido,Correo,Contrasena,fechanacimiento")] ADMINISTRADORES aDMINISTRADORES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDMINISTRADORES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDMINISTRADORES);
        }

        // GET: BADMINISTRADORES1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMINISTRADORES aDMINISTRADORES = db.ADMINISTRADORES.Find(id);
            if (aDMINISTRADORES == null)
            {
                return HttpNotFound();
            }
            return View(aDMINISTRADORES);
        }

        // POST: BADMINISTRADORES1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ADMINISTRADORES aDMINISTRADORES = db.ADMINISTRADORES.Find(id);
            db.ADMINISTRADORES.Remove(aDMINISTRADORES);
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
