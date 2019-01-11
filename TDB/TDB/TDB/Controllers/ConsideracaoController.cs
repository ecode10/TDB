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
    public class ConsideracaoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Consideracao
        public IQueryable<ConsideracaoTDB> GetConsideracaoTDB()
        {
            return db.ConsideracaoTDB;
        }

        // GET: api/Consideracao/5
        [ResponseType(typeof(ConsideracaoTDB))]
        public IHttpActionResult GetConsideracaoTDB(long id)
        {
            ConsideracaoTDB consideracaoTDB = db.ConsideracaoTDB.Find(id);
            if (consideracaoTDB == null)
            {
                return NotFound();
            }

            return Ok(consideracaoTDB);
        }

        [Route("api/Consideracao/Aluno/{IdAluno}")]
        [ResponseType(typeof(ConsideracaoTDB))]
        public IEnumerable<ConsideracaoTDB> GetConsideracaoByIdAluno(Int32 IdAluno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"Select
	                        IdConsideracao, Idaluno, DescricaoConsideracao, DataConsideracao
                        From
	                        ConsideracaoTDB
                        Where 
	                        ConsideracaoTDB.IdAluno = @IdAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdAluno";
            email1.Value = IdAluno;
            email1.SourceColumn = "IdAluno";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<ConsideracaoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // PUT: api/Consideracao/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConsideracaoTDB(long id, ConsideracaoTDB consideracaoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consideracaoTDB.idConsideracao)
            {
                return BadRequest();
            }

            db.Entry(consideracaoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsideracaoTDBExists(id))
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

        // POST: api/Consideracao
        [ResponseType(typeof(ConsideracaoTDB))]
        public IHttpActionResult PostConsideracaoTDB(ConsideracaoTDB consideracaoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ConsideracaoTDB.Add(consideracaoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = consideracaoTDB.idConsideracao }, consideracaoTDB);
        }

        // DELETE: api/Consideracao/5
        [ResponseType(typeof(ConsideracaoTDB))]
        public IHttpActionResult DeleteConsideracaoTDB(long id)
        {
            ConsideracaoTDB consideracaoTDB = db.ConsideracaoTDB.Find(id);
            if (consideracaoTDB == null)
            {
                return NotFound();
            }

            db.ConsideracaoTDB.Remove(consideracaoTDB);
            db.SaveChanges();

            return Ok(consideracaoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsideracaoTDBExists(long id)
        {
            return db.ConsideracaoTDB.Count(e => e.idConsideracao == id) > 0;
        }
    }
}