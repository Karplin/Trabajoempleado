using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabajoempleados.Models;

namespace Trabajoempleados.Controllers
{
    public class ADMINISTRADORESController : ApiController
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: api/ADMINISTRADORES
        public IQueryable<ADMINISTRADORES> GetADMINISTRADORES()
        {
            return db.ADMINISTRADORES;
        }

        // GET: api/ADMINISTRADORES/5
        [ResponseType(typeof(ADMINISTRADORES))]
        public IHttpActionResult GetADMINISTRADORES(int id)
        {
            ADMINISTRADORES aDMINISTRADORES = db.ADMINISTRADORES.Find(id);
            if (aDMINISTRADORES == null)
            {
                return NotFound();
            }

            return Ok(aDMINISTRADORES);
        }

        // PUT: api/ADMINISTRADORES/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutADMINISTRADORES(int id, ADMINISTRADORES aDMINISTRADORES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aDMINISTRADORES.IdAdmin)
            {
                return BadRequest();
            }

            db.Entry(aDMINISTRADORES).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ADMINISTRADORESExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ADMINISTRADORES
        [ResponseType(typeof(ADMINISTRADORES))]
        public IHttpActionResult PostADMINISTRADORES(ADMINISTRADORES aDMINISTRADORES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ADMINISTRADORES.Add(aDMINISTRADORES);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = aDMINISTRADORES.IdAdmin }, aDMINISTRADORES);
        }

        // DELETE: api/ADMINISTRADORES/5
        [ResponseType(typeof(ADMINISTRADORES))]
        public IHttpActionResult DeleteADMINISTRADORES(int id)
        {
            ADMINISTRADORES aDMINISTRADORES = db.ADMINISTRADORES.Find(id);
            if (aDMINISTRADORES == null)
            {
                return NotFound();
            }

            db.ADMINISTRADORES.Remove(aDMINISTRADORES);
            db.SaveChanges();

            return Ok(aDMINISTRADORES);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ADMINISTRADORESExists(int id)
        {
            return db.ADMINISTRADORES.Count(e => e.IdAdmin == id) > 0;
        }
    }
}