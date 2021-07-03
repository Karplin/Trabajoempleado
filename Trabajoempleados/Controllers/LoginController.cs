using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajoempleados.Models;

namespace Trabajoempleados.Controllers
{
    public class LoginController : Controller
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Correo, string Contrasena)
        {
            combinados obj = new combinados();
 
            var admin = db.ADMINISTRADORES.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);
            
            if (admin != null)
            {
                obj.idcontracheck = admin.IdAdmin;
                return RedirectToAction("Veradmin", "BEMPLEOS1", new { obj.idcontracheck });
            }

            else
            {
                var candidato = db.CANDIDATOS.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);

                if (candidato != null)
                {
                    obj.idcontracheck = candidato.IdCandidato;
                    return RedirectToAction("Ver", "BEMPLEOS1", new { obj.idcontracheck });
                }
                else
                {
                    var contratista = db.CONTRATISTAS.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);

                    if (contratista != null)
                    {

                        obj.idcontracheck = contratista.Id;
                        return RedirectToAction("Vercontratista", "BEMPLEOS1", new { obj.idcontracheck });
                    }
                    else
                    {
                        ViewBag.Mensaje = "ISDNFIS";
                        return View();
                    }
                }
            }

        }




    }
}