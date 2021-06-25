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
    public class EMPLEOSController : ApiController
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: api/EMPLEOS
        public IQueryable<EMPLEOS> GetEMPLEOS()
        {
            return db.EMPLEOS;
        }

        // GET: api/EMPLEOS/5
        [ResponseType(typeof(EMPLEOS))]
        public IHttpActionResult GetEMPLEOS(int id)
        {
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            if (eMPLEOS == null)
            {
                return NotFound();
            }

            return Ok(eMPLEOS);
        }

        // PUT: api/EMPLEOS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMPLEOS(int id, EMPLEOS eMPLEOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMPLEOS.Id)
            {
                return BadRequest();
            }

            db.Entry(eMPLEOS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMPLEOSExists(id))
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

        // POST: api/EMPLEOS
        [ResponseType(typeof(EMPLEOS))]
        public IHttpActionResult PostEMPLEOS(EMPLEOS eMPLEOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMPLEOS.Add(eMPLEOS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eMPLEOS.Id }, eMPLEOS);
        }

        // DELETE: api/EMPLEOS/5
        [ResponseType(typeof(EMPLEOS))]
        public IHttpActionResult DeleteEMPLEOS(int id)
        {
            EMPLEOS eMPLEOS = db.EMPLEOS.Find(id);
            if (eMPLEOS == null)
            {
                return NotFound();
            }

            db.EMPLEOS.Remove(eMPLEOS);
            db.SaveChanges();

            return Ok(eMPLEOS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMPLEOSExists(int id)
        {
            return db.EMPLEOS.Count(e => e.Id == id) > 0;
        }
    }
}