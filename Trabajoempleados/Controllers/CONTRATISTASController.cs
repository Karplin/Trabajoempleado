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
    public class CONTRATISTASController : ApiController
    {
        private bolsaempleosEntities db = new bolsaempleosEntities();

        // GET: api/CONTRATISTAS
        public IQueryable<CONTRATISTAS> GetCONTRATISTAS()
        {
            return db.CONTRATISTAS;
        }

        // GET: api/CONTRATISTAS/5
        [ResponseType(typeof(CONTRATISTAS))]
        public IHttpActionResult GetCONTRATISTAS(int id)
        {
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            if (cONTRATISTAS == null)
            {
                return NotFound();
            }

            return Ok(cONTRATISTAS);
        }

        // PUT: api/CONTRATISTAS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCONTRATISTAS(int id, CONTRATISTAS cONTRATISTAS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cONTRATISTAS.Id)
            {
                return BadRequest();
            }

            db.Entry(cONTRATISTAS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CONTRATISTASExists(id))
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

        // POST: api/CONTRATISTAS
        [ResponseType(typeof(CONTRATISTAS))]
        public IHttpActionResult PostCONTRATISTAS(CONTRATISTAS cONTRATISTAS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CONTRATISTAS.Add(cONTRATISTAS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cONTRATISTAS.Id }, cONTRATISTAS);
        }

        // DELETE: api/CONTRATISTAS/5
        [ResponseType(typeof(CONTRATISTAS))]
        public IHttpActionResult DeleteCONTRATISTAS(int id)
        {
            CONTRATISTAS cONTRATISTAS = db.CONTRATISTAS.Find(id);
            if (cONTRATISTAS == null)
            {
                return NotFound();
            }

            db.CONTRATISTAS.Remove(cONTRATISTAS);
            db.SaveChanges();

            return Ok(cONTRATISTAS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CONTRATISTASExists(int id)
        {
            return db.CONTRATISTAS.Count(e => e.Id == id) > 0;
        }
    }
}