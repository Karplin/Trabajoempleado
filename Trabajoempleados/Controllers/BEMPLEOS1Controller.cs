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
using System.Text.RegularExpressions;

namespace Trabajoempleados.Controllers
{
    public class BEMPLEOS1Controller : Controller
    {

        filtros ola = new filtros();

        

        private bolsaempleosEntities db = new bolsaempleosEntities();

        #region  Ver e Indexe
        // GET: BEMPLEOS1
        public ActionResult Ver(int idcontracheck = 1, int pagina = 1)
        {
            ViewData["items"] = ListarCategoria();

            combinados model = new combinados
            {
                Empleos = db.EMPLEOS.ToList(),
                Categoria = new CATEGORIA()
            };

            if (idcontracheck == 1)
            {
                model.idcontracheck = model.idcontracheck;
            }
            else
            {
                model.idcontracheck = idcontracheck;
            }

            var cantidadRegistrosPorPagina = 9; // parámetro
            using (var db = new bolsaempleosEntities())
            {

                var item = db.EMPLEOS.OrderByDescending(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.EMPLEOS.Count();

    
                model.Empleos = item;
                model.PaginaActual = pagina;
                model.TotalDeRegistros = totalDeRegistros;
                model.RegistrosPorPagina = cantidadRegistrosPorPagina;             
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Indexe(string nombre)
        {

            return View(Buscarcategoria(nombre));
        }
        #endregion


        public List<busca_categoria_Result> Buscarcategoria(string nombre)
        {

            return db.busca_categoria(nombre).ToList();
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

        public ActionResult Detailscontra(int? id)
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

        public ActionResult Detailsadmin(int? id)
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
        public List<SelectListItem> ListarTipo()
        {

            //--------------------------------------------------------------------------
            List<SelectListItem> itemz = new List<SelectListItem>();
            {
                SelectListItem item1 = new SelectListItem() { Text = "Tiempo Completo", Value = "Tiempo Completo", Selected = true };
                SelectListItem item2 = new SelectListItem() { Text = "Medio Tiempo", Value = "Medio Tiempo", Selected = false };
                SelectListItem item3 = new SelectListItem() { Text = "Freelance", Value = "Freelance", Selected = false };

                itemz.Add(item1);
                itemz.Add(item2);
                itemz.Add(item3);
            };

            return itemz;

        }


        public List<SelectListItem> ListarCategoria()
        {
            List<CATEGORIA> lst = null;

            using (bolsaempleosEntities dbx = new bolsaempleosEntities())
            {
                lst = (from d in dbx.CATEGORIA.AsEnumerable()
                       select new CATEGORIA
                       {
                           Id = d.Id,
                           Nombre = d.Nombre
                       }).ToList();
            }

            //--------------------------------------------------------------------------
            List<SelectListItem> itemz = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Nombre.ToString(),
                    Selected = false

                };

            });

            return itemz;

        }



        // GET: BEMPLEOS1/Create
        public ActionResult Create()
        {
            ViewData["items"] = ListarCategoria();
            ViewData["itemsx"] = ListarTipo();
            return View();
        }

        // POST: BEMPLEOS1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EMPLEOS emple, [Bind(Include = "Id,IdEmpleo,IdEmpresa,Empresa,Tipo,Logo,Posicion,Ubicacion,Categoria,Descripcion,caplicar,Email,Fechapubli,Urlx")] EMPLEOS eMPLEOS)
        {
            ViewData["items"] = ListarCategoria();
            ViewData["itemsx"] = ListarTipo();
            // Fecha
            var anio = DateTime.Now;
            var fecha = anio.ToShortDateString();

            eMPLEOS.Fechapubli = Convert.ToString(fecha);
            //-------------------------------------------------

            
            HttpPostedFileBase FileBase = Request.Files[0];
            try
            {

        
            WebImage image = new WebImage(FileBase.InputStream);

            eMPLEOS.Logo = image.GetBytes();

            }
            catch { }






            var code = string.Empty;
            var code2 = string.Empty;

            // ORIGEN DE

            code = Regex.Replace(emple.Posicion, @"[\p{P}\p{S}\p{C}\p{N}]+", "");
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
                no = db.EMPLEOS
                .OrderByDescending(x => x.Id)
                .First().Id;


                string codigo;
                string c1 = "00", c2 = "0";
                int secuencia = 1 + no;
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
                
                eMPLEOS.IdEmpleo = codigo;

            }
            catch
            {
                string c1 = "00";
                no = 0;

                int secuencia = 1 + no;

                string codigo = code + "-" + c1 + secuencia;
                eMPLEOS.IdEmpleo = codigo;
            }

            if (ModelState.IsValid)
            {
                db.EMPLEOS.Add(eMPLEOS);
                db.SaveChanges();
                return RedirectToAction("Vercontratista");
            }

            return View(eMPLEOS);
        }

        // GET: BEMPLEOS1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["items"] = ListarCategoria();
            ViewData["itemsx"] = ListarTipo();
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
        public ActionResult Edit([Bind(Include = "Id,IdEmpleo,IdEmpresa,Empresa,Tipo,Logo,Posicion,Ubicacion,Categoria,Descripcion,caplicar,Email,Fechapubli,Urlx")] EMPLEOS eMPLEOS)
        {
            ViewData["items"] = ListarCategoria();
            ViewData["itemsx"] = ListarTipo();
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
                return RedirectToAction("Veradmin");
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
            return RedirectToAction("Veradmin");
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
            try
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
            catch
            {
                string ruta = "~/img/prueba.jpg";
                return File(ruta, "image/jpg");

            }
        }

        public ActionResult getImage2(int id)
        {
            try {
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            byte[] byteImage = eMPLEOS.Logo;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
            }
            catch {
                string ruta = "~/img/prueba.jpg";
                return File(ruta, "image/jpg");

            }

            
        }




        // ADMINISTRADOR ----------------------------------
        public ActionResult Veradmin(int idcontracheck = 1, int pagina = 1)
        {
            ViewData["items"] = ListarCategoria();

            combinados model = new combinados
            {
                Empleos = db.EMPLEOS.ToList(),
                Categoria = new CATEGORIA()
            };
            if (idcontracheck == 1)
            {
                model.idcontracheck = model.idcontracheck;
            }
            else
            {
                model.idcontracheck = idcontracheck;
            }
            var cantidadRegistrosPorPagina = 9; // parámetro
            using (var db = new bolsaempleosEntities())
            {

                var item = db.EMPLEOS.OrderByDescending(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.EMPLEOS.Count();


                model.Empleos = item;
                model.PaginaActual = pagina;
                model.TotalDeRegistros = totalDeRegistros;
                model.RegistrosPorPagina = cantidadRegistrosPorPagina;
            }

            return View(model);
        }

       

        [HttpPost]
        public ActionResult Indexeadmin(string nombre)
        {
            return View(Buscarcategoria(nombre));
        }

        public ActionResult Verconfiltro()
        {
            return View(db.EMPLEOS.ToList());
        }

        public ActionResult Verconfiltroadmin()
        {
            return View(db.EMPLEOS.ToList());
        }
        

        // CONTRATISTA ----------------------------------
        public ActionResult Vercontratista(int idcontracheck = 1, int pagina = 1)
        {
            
            combinados model = new combinados
            {
                Empleos = db.EMPLEOS.ToList(),
                Categoria = new CATEGORIA()
            };

            if (idcontracheck == 1)
            {
                model.idcontracheck = model.idcontracheck;
            }
            else
            {
                model.idcontracheck = idcontracheck;
            }
            ViewData["items"] = ListarCategoria();

            var cantidadRegistrosPorPagina = 9; // parámetro
            using (var db = new bolsaempleosEntities())
            {

                var item = db.EMPLEOS.OrderByDescending(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.EMPLEOS.Count();


                model.Empleos = item;
                model.PaginaActual = pagina;
                model.TotalDeRegistros = totalDeRegistros;
                model.RegistrosPorPagina = cantidadRegistrosPorPagina;
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Indexecontratista(string nombre)
        {
            return View(Buscarcategoria(nombre));
        }

        public ActionResult verconfiltrocontra()
        {
            return View(db.EMPLEOS.ToList());
        }

        public ActionResult EditContra(int? id)
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
        public ActionResult EditContra([Bind(Include = "Id,IdContratista,Rnc,NombreEmpresa,Representante,Correo,Telefono,Contrasena,Direccion,Descripcion")] CONTRATISTAS cONTRATISTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRATISTAS).State = EntityState.Modified;
                db.SaveChanges();
                return View(cONTRATISTAS);
            }
            return View(cONTRATISTAS);
        }

        public ActionResult EditAdmin(int? id)
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
        public ActionResult EditAdmin([Bind(Include = "IdAdmin,Cedula,Nombre,Apellido,Correo,Contrasena,fechanacimiento")] ADMINISTRADORES aDMINISTRADORES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDMINISTRADORES).State = EntityState.Modified;
                db.SaveChanges();
                return View(aDMINISTRADORES);
            }
            return View(aDMINISTRADORES);
        }

        public ActionResult EditCand(int? id)
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
        public ActionResult EditCand([Bind(Include = "IdCandidato,Cedula,Nombre,Apellido,Correo,Telefono,Contrasena,fechanacimiento")] CANDIDATOS cANDIDATOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cANDIDATOS).State = EntityState.Modified;
                db.SaveChanges();
                return View(cANDIDATOS);
            }
            return View(cANDIDATOS);
        }






        public ActionResult Veradminx(int idcontracheck = 1, int pagina = 1)
        {
            ViewData["items"] = ListarCategoria();

            combinados model = new combinados
            {
                Empleos = db.EMPLEOS.ToList(),
                Categoria = new CATEGORIA()
            };
            if (idcontracheck == 1)
            {
                model.idcontracheck = model.idcontracheck;
            }
            else
            {
                model.idcontracheck = idcontracheck;
            }
            var cantidadRegistrosPorPagina = 9; // parámetro
            using (var db = new bolsaempleosEntities())
            {

                var item = db.EMPLEOS.OrderByDescending(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.EMPLEOS.Count();


                model.Empleos = item;
                model.PaginaActual = pagina;
                model.TotalDeRegistros = totalDeRegistros;
                model.RegistrosPorPagina = cantidadRegistrosPorPagina;
            }

            return View(model);
        }



        public ActionResult Vercontratistax(int idcontracheck = 1, int pagina = 1)
        {

            combinados model = new combinados
            {
                Empleos = db.EMPLEOS.ToList(),
                Categoria = new CATEGORIA()
            };

            if (idcontracheck == 1)
            {
                model.idcontracheck = model.idcontracheck;
            }
            else
            {
                model.idcontracheck = idcontracheck;
            }
            ViewData["items"] = ListarCategoria();

            var cantidadRegistrosPorPagina = 9; // parámetro
            using (var db = new bolsaempleosEntities())
            {

                var item = db.EMPLEOS.OrderByDescending(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.EMPLEOS.Count();


                model.Empleos = item;
                model.PaginaActual = pagina;
                model.TotalDeRegistros = totalDeRegistros;
                model.RegistrosPorPagina = cantidadRegistrosPorPagina;
            }

            return View(model);
        }

        public ActionResult Verx(int idcontracheck = 1, int pagina = 1)
        {
            ViewData["items"] = ListarCategoria();

            combinados model = new combinados
            {
                Empleos = db.EMPLEOS.ToList(),
                Categoria = new CATEGORIA()
            };

            if (idcontracheck == 1)
            {
                model.idcontracheck = model.idcontracheck;
            }
            else
            {
                model.idcontracheck = idcontracheck;
            }

            var cantidadRegistrosPorPagina = 9; // parámetro
            using (var db = new bolsaempleosEntities())
            {

                var item = db.EMPLEOS.OrderByDescending(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.EMPLEOS.Count();


                model.Empleos = item;
                model.PaginaActual = pagina;
                model.TotalDeRegistros = totalDeRegistros;
                model.RegistrosPorPagina = cantidadRegistrosPorPagina;
            }

            return View(model);
        }

   




    }
}
