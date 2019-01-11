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
    public class DocumentoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Documento
        public IQueryable<DocumentoTDB> GetDocumentoTDB()
        {
            return db.DocumentoTDB;
        }

        // GET: api/Documento/5
        [ResponseType(typeof(DocumentoTDB))]
        public IHttpActionResult GetDocumentoTDB(long id)
        {
            DocumentoTDB documentoTDB = db.DocumentoTDB.Find(id);
            if (documentoTDB == null)
            {
                return NotFound();
            }

            return Ok(documentoTDB);
        }

        // PUT: api/Documento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDocumentoTDB(long id, DocumentoTDB documentoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentoTDB.idDocumento)
            {
                return BadRequest();
            }

            db.Entry(documentoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoTDBExists(id))
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

        // POST: api/Documento
        [ResponseType(typeof(DocumentoTDB))]
        public IHttpActionResult PostDocumentoTDB(DocumentoTDB documentoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DocumentoTDB.Add(documentoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = documentoTDB.idDocumento }, documentoTDB);
        }

        // DELETE: api/Documento/5
        [ResponseType(typeof(DocumentoTDB))]
        public IHttpActionResult DeleteDocumentoTDB(long id)
        {
            DocumentoTDB documentoTDB = db.DocumentoTDB.Find(id);
            if (documentoTDB == null)
            {
                return NotFound();
            }

            db.DocumentoTDB.Remove(documentoTDB);
            db.SaveChanges();

            return Ok(documentoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocumentoTDBExists(long id)
        {
            return db.DocumentoTDB.Count(e => e.idDocumento == id) > 0;
        }
    }
}