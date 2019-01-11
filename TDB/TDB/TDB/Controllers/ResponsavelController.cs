using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using TDB.DAO;
using TDB.Models;

namespace TDB.Controllers
{
    public class ResponsavelController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Responsavel
        public IQueryable<ResponsavelTDB> GetResponsavelTDB()
        {
            return db.ResponsavelTDB;
        }

        // GET: api/Responsavel/5
        [ResponseType(typeof(ResponsavelTDB))]
        public IHttpActionResult GetResponsavelTDB(long id)
        {
            ResponsavelTDB responsavelTDB = db.ResponsavelTDB.Find(id);
            if (responsavelTDB == null)
            {
                return NotFound();
            }

            return Ok(responsavelTDB);
        }

        // PUT: api/Responsavel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResponsavelTDB(long id, ResponsavelTDB responsavelTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != responsavelTDB.idResponsavel)
            {
                return BadRequest();
            }

            db.Entry(responsavelTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsavelTDBExists(id))
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

        // POST: api/Responsavel
        [ResponseType(typeof(ResponsavelTDB))]
        public IHttpActionResult PostResponsavelTDB(ResponsavelTDB responsavelTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ResponsavelTDB.Add(responsavelTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = responsavelTDB.idResponsavel }, responsavelTDB);
        }

        

        // DELETE: api/Responsavel/5
        [ResponseType(typeof(ResponsavelTDB))]
        public IHttpActionResult DeleteResponsavelTDB(long id)
        {
            ResponsavelTDB responsavelTDB = db.ResponsavelTDB.Find(id);
            if (responsavelTDB == null)
            {
                return NotFound();
            }

            db.ResponsavelTDB.Remove(responsavelTDB);
            db.SaveChanges();

            return Ok(responsavelTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponsavelTDBExists(long id)
        {
            return db.ResponsavelTDB.Count(e => e.idResponsavel == id) > 0;
        }
    }
}