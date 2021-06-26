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
                return RedirectToAction("Create", "BCANDIDATOS1");
            }

            else
            {
                var candidato = db.CANDIDATOS.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);

                if (candidato != null)
                {
                    return RedirectToAction("Index", "BCANDIDATOS1");
                }
                else
                {
                    var contratista = db.CONTRATISTAS.FirstOrDefault(x => x.Correo == Correo && x.Contrasena == Contrasena);

                    if (contratista != null)
                    {
                        return RedirectToAction("Delete", "BCANDIDATOS1");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Datos Incorrectos, ninguna cuenta con estas credenciales.";
                        return View();
                    }
                }
            }

        }




    }
}