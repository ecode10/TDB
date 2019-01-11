using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
    public class FotoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Foto
        public IQueryable<FotoTDB> GetFotoTDB()
        {
            return db.FotoTDB;
        }

        // GET: api/Foto/5
        [ResponseType(typeof(FotoTDB))]
        public IHttpActionResult GetFotoTDB(long id)
        {
            FotoTDB fotoTDB = db.FotoTDB.Find(id);
            if (fotoTDB == null)
            {
                return NotFound();
            }

            return Ok(fotoTDB);
        }

        [Route("api/Foto/Aluno/{IdAluno}")]
        [ResponseType(typeof(FotoTDB))]
        public IEnumerable<FotoTDB> GetFotoByAluno(Int32 idAluno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"SELECT
	                        IdFoto, IdTipoFoto, IdAluno, NomeFoto, StatusFoto
                        FROM
	                        FotoTDB
                        WHERE IdAluno = @IdAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdAluno";
            email1.Value = idAluno;
            email1.SourceColumn = "IdAluno";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<FotoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // PUT: api/Foto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFotoTDB(long id, FotoTDB fotoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fotoTDB.idFoto)
            {
                return BadRequest();
            }

            db.Entry(fotoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoTDBExists(id))
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

        // POST: api/Foto
        [ResponseType(typeof(FotoTDB))]
        public IHttpActionResult PostFotoTDB(FotoTDB fotoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FotoTDB.Add(fotoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fotoTDB.idFoto }, fotoTDB);
        }

        // DELETE: api/Foto/5
        [ResponseType(typeof(FotoTDB))]
        public IHttpActionResult DeleteFotoTDB(long id)
        {
            FotoTDB fotoTDB = db.FotoTDB.Find(id);
            if (fotoTDB == null)
            {
                return NotFound();
            }

            db.FotoTDB.Remove(fotoTDB);
            db.SaveChanges();

            return Ok(fotoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FotoTDBExists(long id)
        {
            return db.FotoTDB.Count(e => e.idFoto == id) > 0;
        }
    }
}