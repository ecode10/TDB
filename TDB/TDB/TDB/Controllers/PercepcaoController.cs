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
    public class PercepcaoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Percepcao
        public IQueryable<PercepcaoTDB> GetPercepcaoTDB()
        {
            return db.PercepcaoTDB;
        }

        // GET: api/Percepcao/5
        [ResponseType(typeof(PercepcaoTDB))]
        public IHttpActionResult GetPercepcaoTDB(long id)
        {
            PercepcaoTDB percepcaoTDB = db.PercepcaoTDB.Find(id);
            if (percepcaoTDB == null)
            {
                return NotFound();
            }

            return Ok(percepcaoTDB);
        }

        // PUT: api/Percepcao/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPercepcaoTDB(long id, PercepcaoTDB percepcaoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != percepcaoTDB.idPercepcao)
            {
                return BadRequest();
            }

            db.Entry(percepcaoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PercepcaoTDBExists(id))
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

        [Route("api/Percepcao/Aluno/{IdAluno}")]
        [ResponseType(typeof(PercepcaoTDB))]
        public IEnumerable<PercepcaoTDB> GetPercepcaoByIdAluno(Int32 idAluno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"SELECT
	                        IdPercepcao, IdAluno, TemCarie, VergonhaDosDentes, FoiAoDentista, Onde
                        FROM
	                        PercepcaoTDB
                        WHERE
	                        IdAluno = @IdAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdAluno";
            email1.Value = idAluno;
            email1.SourceColumn = "IdAluno";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<PercepcaoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // POST: api/Percepcao
        [ResponseType(typeof(PercepcaoTDB))]
        public IHttpActionResult PostPercepcaoTDB(PercepcaoTDB percepcaoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PercepcaoTDB.Add(percepcaoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = percepcaoTDB.idPercepcao }, percepcaoTDB);
        }

        // DELETE: api/Percepcao/5
        [ResponseType(typeof(PercepcaoTDB))]
        public IHttpActionResult DeletePercepcaoTDB(long id)
        {
            PercepcaoTDB percepcaoTDB = db.PercepcaoTDB.Find(id);
            if (percepcaoTDB == null)
            {
                return NotFound();
            }

            db.PercepcaoTDB.Remove(percepcaoTDB);
            db.SaveChanges();

            return Ok(percepcaoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PercepcaoTDBExists(long id)
        {
            return db.PercepcaoTDB.Count(e => e.idPercepcao == id) > 0;
        }
    }
}