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
    public class PerguntasSocioEconomicasController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/PerguntasSocioEconomicas
        public IQueryable<PerguntasSocioEconomicasTDB> GetPerguntasSocioEconomicasTDB()
        {
            return db.PerguntasSocioEconomicasTDB;
        }

        // GET: api/PerguntasSocioEconomicas/5
        [ResponseType(typeof(PerguntasSocioEconomicasTDB))]
        public IHttpActionResult GetPerguntasSocioEconomicasTDB(int id)
        {
            PerguntasSocioEconomicasTDB perguntasSocioEconomicasTDB = db.PerguntasSocioEconomicasTDB.Find(id);
            if (perguntasSocioEconomicasTDB == null)
            {
                return NotFound();
            }

            return Ok(perguntasSocioEconomicasTDB);
        }

        [Route("api/PerguntasSocioEconomicas/Aluno/{IdAluno}")]
        [ResponseType(typeof(PerguntasSocioEconomicasTDB))]
        public IEnumerable<PerguntasSocioEconomicasTDB> GetPerguntasSocioEconomicasByIdAluno(Int32 idAluno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"Select
	                        IdPerguntas, IdAluno, CarroProprio, CasaComodo, QuemTrabalha
                        From
	                        PerguntasSocioEconomicasTDB
                        Where 
	                        IdAluno = @IdAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdAluno";
            email1.Value = idAluno;
            email1.SourceColumn = "IdAluno";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<PerguntasSocioEconomicasTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // PUT: api/PerguntasSocioEconomicas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerguntasSocioEconomicasTDB(int id, PerguntasSocioEconomicasTDB perguntasSocioEconomicasTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perguntasSocioEconomicasTDB.idPerguntas)
            {
                return BadRequest();
            }

            db.Entry(perguntasSocioEconomicasTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerguntasSocioEconomicasTDBExists(id))
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

        // POST: api/PerguntasSocioEconomicas
        [ResponseType(typeof(PerguntasSocioEconomicasTDB))]
        public IHttpActionResult PostPerguntasSocioEconomicasTDB(PerguntasSocioEconomicasTDB perguntasSocioEconomicasTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PerguntasSocioEconomicasTDB.Add(perguntasSocioEconomicasTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = perguntasSocioEconomicasTDB.idPerguntas }, perguntasSocioEconomicasTDB);
        }

        // DELETE: api/PerguntasSocioEconomicas/5
        [ResponseType(typeof(PerguntasSocioEconomicasTDB))]
        public IHttpActionResult DeletePerguntasSocioEconomicasTDB(int id)
        {
            PerguntasSocioEconomicasTDB perguntasSocioEconomicasTDB = db.PerguntasSocioEconomicasTDB.Find(id);
            if (perguntasSocioEconomicasTDB == null)
            {
                return NotFound();
            }

            db.PerguntasSocioEconomicasTDB.Remove(perguntasSocioEconomicasTDB);
            db.SaveChanges();

            return Ok(perguntasSocioEconomicasTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerguntasSocioEconomicasTDBExists(int id)
        {
            return db.PerguntasSocioEconomicasTDB.Count(e => e.idPerguntas == id) > 0;
        }
    }
}