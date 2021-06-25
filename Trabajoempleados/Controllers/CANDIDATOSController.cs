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
    public class CANDIDATOSController : ApiController
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: api/CANDIDATOS
        public IQueryable<CANDIDATOS> GetCANDIDATOS()
        {
            return db.CANDIDATOS;
        }

        // GET: api/CANDIDATOS/5
        [ResponseType(typeof(CANDIDATOS))]
        public IHttpActionResult GetCANDIDATOS(int id)
        {
            CANDIDATOS cANDIDATOS = db.CANDIDATOS.Find(id);
            if (cANDIDATOS == null)
            {
                return NotFound();
            }

            return Ok(cANDIDATOS);
        }

        // PUT: api/CANDIDATOS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCANDIDATOS(int id, CANDIDATOS cANDIDATOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cANDIDATOS.IdCandidato)
            {
                return BadRequest();
            }

            db.Entry(cANDIDATOS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CANDIDATOSExists(id))
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

        // POST: api/CANDIDATOS
        [ResponseType(typeof(CANDIDATOS))]
        public IHttpActionResult PostCANDIDATOS(CANDIDATOS cANDIDATOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CANDIDATOS.Add(cANDIDATOS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cANDIDATOS.IdCandidato }, cANDIDATOS);
        }

        // DELETE: api/CANDIDATOS/5
        [ResponseType(typeof(CANDIDATOS))]
        public IHttpActionResult DeleteCANDIDATOS(int id)
        {
            CANDIDATOS cANDIDATOS = db.CANDIDATOS.Find(id);
            if (cANDIDATOS == null)
            {
                return NotFound();
            }

            db.CANDIDATOS.Remove(cANDIDATOS);
            db.SaveChanges();

            return Ok(cANDIDATOS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CANDIDATOSExists(int id)
        {
            return db.CANDIDATOS.Count(e => e.IdCandidato == id) > 0;
        }
    }
}