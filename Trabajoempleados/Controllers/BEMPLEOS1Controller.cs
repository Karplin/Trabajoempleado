using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trabajoempleados.Models;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Drawing;
using System.Drawing.Imaging;

namespace Trabajoempleados.Controllers
{
    public class BEMPLEOS1Controller : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: BEMPLEOS1
        public ActionResult Index()
        {
            return View(db.EMPLEOS.ToList());
        }

        // GET: BEMPLEOS1/Details/5
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

        // GET: BEMPLEOS1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BEMPLEOS1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEmpleo,IdEmpresa,Empresa,Tipo,Logo,Posicion,Ubicacion,Categoria,Descripcion,caplicar,Email,Fechapubli")] EMPLEOS eMPLEOS)
        {

            HttpPostedFileBase FileBase = Request.Files[0];

            WebImage image = new WebImage(FileBase.InputStream);

            eMPLEOS.Logo = image.GetBytes();

            if (ModelState.IsValid)
            {
                db.EMPLEOS.Add(eMPLEOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eMPLEOS);
        }

        // GET: BEMPLEOS1/Edit/5
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

        // POST: BEMPLEOS1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEmpleo,IdEmpresa,Empresa,Tipo,Logo,Posicion,Ubicacion,Categoria,Descripcion,caplicar,Email,Fechapubli")] EMPLEOS eMPLEOS)
        {
            byte[] imagenactual = null;

            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase == null)
            {
                imagenactual = db.EMPLEOS.SingleOrDefault(t=>t.Id == eMPLEOS.Id).Logo;
            }
            else
            {
                WebImage image = new WebImage(FileBase.InputStream);

                eMPLEOS.Logo = image.GetBytes();

            }

            if (ModelState.IsValid)
            {
                db.Entry(eMPLEOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eMPLEOS);
        }

        // GET: BEMPLEOS1/Delete/5
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

        // POST: BEMPLEOS1/Delete/5
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

      

        public ActionResult getImage(int id)
        {
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            byte[] byteImage = eMPLEOS.Logo;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
            
        }


    }
}
