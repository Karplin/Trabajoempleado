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
 
            var admin = db.ADMINISTRADORES.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);
            
            if (admin != null)
            {
                return RedirectToAction("Veradmin", "BEMPLEOS1", admin.IdAdmin);
            }

            else
            {
                var candidato = db.CANDIDATOS.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);

                if (candidato != null)
                {
                    return RedirectToAction("Ver", "BEMPLEOS1", candidato.IdCandidato);
                }
                else
                {
                    var contratista = db.CONTRATISTAS.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);

                    if (contratista != null)
                    {
                        int idcontra = contratista.Id;
                        return RedirectToAction("Vercontratista", "BEMPLEOS1", new { idcontra });
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