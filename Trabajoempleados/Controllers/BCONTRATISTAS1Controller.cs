using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Trabajoempleados.Models;

namespace Trabajoempleados.Controllers
{
    public class BCONTRATISTAS1Controller : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();


        // GET: BCONTRATISTAS1
        public ActionResult Index()
        {
            return View(db.CONTRATISTAS.ToList());
        }

        // GET: BCONTRATISTAS1/Details/5
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

        // GET: BCONTRATISTAS1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BCONTRATISTAS1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CONTRATISTAS contra, [Bind(Include = "Id,IdContratista,Rnc,NombreEmpresa,Representante,Correo,Telefono,Contrasena,Direccion,Descripcion")] CONTRATISTAS cONTRATISTAS)
        {
            var code = string.Empty;
            var code2 = string.Empty;

            // ORIGEN DE

            code = Regex.Replace(contra.NombreEmpresa, @"[\p{P}\p{S}\p{C}\p{N}]+", "");
            code = Regex.Replace(code, @"\p{Z}+", " ");
            code = Regex.Replace(code.Trim(), @"\s+(?:[JS]R|I{1,3}|I[VX]|VI{0,3})$", "", RegexOptions.IgnoreCase);
            code = Regex.Replace(code, @"^(\p{L})[^\s]*(?:\s+(?:\p{L}+\s+(?=\p{L}))?(?:(\p{L})\p{L}*)?)?$", "$1$2").Trim();


            if (code.Length > 2)
            {
                code = code.Substring(0, 2);
            }

            code = code.ToUpperInvariant();

            int no = 0;
           
            try
            {
                no = db.CONTRATISTAS
                .OrderByDescending(x => x.Id)
                .First().Id;



                int secuencia = 1 + no;

                string codigo;
                string c1 = "00", c2 = "0";
                if (secuencia < 10)
                {
                    codigo = code + "-" + c1 + secuencia;
                }
                else if (secuencia < 100)
                {
                    codigo = code + "-" + c2 + secuencia;
                }
                else
                {
                    codigo = code + "-" + secuencia;
                }
                cONTRATISTAS.IdContratista = codigo;

            }
            catch
            {
                string c1 = "00";
                no = 0;

                int secuencia = 1 + no;

                string codigo = code + "-" + secuencia;
                cONTRATISTAS.IdContratista = codigo;

            }
            if (ModelState.IsValid)
            {
                db.CONTRATISTAS.Add(cONTRATISTAS);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }

            return View(cONTRATISTAS);
        }

        // GET: BCONTRATISTAS1/Edit/5
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

        // POST: BCONTRATISTAS1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdContratista,Rnc,NombreEmpresa,Representante,Correo,Telefono,Contrasena,Direccion,Descripcion")] CONTRATISTAS cONTRATISTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRATISTAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ver", "BEMPLEOS1");
            }
            return View(cONTRATISTAS);
        }

        // GET: BCONTRATISTAS1/Delete/5
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

        // POST: BCONTRATISTAS1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            db.CONTRATISTAS.Remove(cONTRATISTAS);
            db.SaveChanges();
            return RedirectToAction("Vercontratista", "BEMPLEOS1");
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
