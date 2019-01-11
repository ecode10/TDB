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
using TDB.DAO;
using TDB.Models;

namespace TDB.Controllers
{
    public class TipoFotoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/TipoFoto
        public IQueryable<TipoFotoTDB> GetTipoFotoTDB()
        {
            return db.TipoFotoTDB;
        }

        // GET: api/TipoFoto/5
        [ResponseType(typeof(TipoFotoTDB))]
        public IHttpActionResult GetTipoFotoTDB(long id)
        {
            TipoFotoTDB tipoFotoTDB = db.TipoFotoTDB.Find(id);
            if (tipoFotoTDB == null)
            {
                return NotFound();
            }

            return Ok(tipoFotoTDB);
        }

        // PUT: api/TipoFoto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoFotoTDB(long id, TipoFotoTDB tipoFotoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoFotoTDB.idTipoFoto)
            {
                return BadRequest();
            }

            db.Entry(tipoFotoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoFotoTDBExists(id))
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

        // POST: api/TipoFoto
        [ResponseType(typeof(TipoFotoTDB))]
        public IHttpActionResult PostTipoFotoTDB(TipoFotoTDB tipoFotoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoFotoTDB.Add(tipoFotoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoFotoTDB.idTipoFoto }, tipoFotoTDB);
        }

        // DELETE: api/TipoFoto/5
        [ResponseType(typeof(TipoFotoTDB))]
        public IHttpActionResult DeleteTipoFotoTDB(long id)
        {
            TipoFotoTDB tipoFotoTDB = db.TipoFotoTDB.Find(id);
            if (tipoFotoTDB == null)
            {
                return NotFound();
            }

            db.TipoFotoTDB.Remove(tipoFotoTDB);
            db.SaveChanges();

            return Ok(tipoFotoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoFotoTDBExists(long id)
        {
            return db.TipoFotoTDB.Count(e => e.idTipoFoto == id) > 0;
        }
    }
}