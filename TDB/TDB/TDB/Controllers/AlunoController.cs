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
    public class AlunoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Aluno
        public IQueryable<AlunoTDB> GetAlunoTDB()
        {
            return db.AlunoTDB;
        }

        [Route("api/Aluno/Evento/{IdEvento}")]
        [ResponseType(typeof(AlunoPersonalizadoTDB))]
        public IEnumerable<AlunoPersonalizadoTDB> GetAlunoByEvento(Int32 IdEvento)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"SELECT
	                        EventoTDB.IdEvento,
	                        AlunoTDB.IdAluno,
	                        AlunoTDB.NomeAluno,
	                        ResponsavelTDB.NomeResponsavel,
	                        ResponsavelTDB.CPFResponsavel,
                            AlunoTDB.idInstituicao,
                            AlunoTDB.serieAluno,
                            AlunoTDB.turnoAluno,
                            AlunoTDB.sexoAluno,
                            AlunoTDB.dataNascimentoAluno,
                            AlunoTDB.rendaFamiliaAluno,
                            AlunoTDB.planoSaudeAluno,
                            AlunoTDB.planoDentalAluno
                        FROM
	                        EventoTDB INNER JOIN AlunoTDB
		                        ON EventoTDB.IdEvento = AlunoTDB.IdEvento
	                        INNER JOIN AlunoResponsavelTDB
		                        ON AlunoResponsavelTDB.IdAluno = AlunoTDB.IdAluno
	                        INNER JOIN ResponsavelTDB
		                        ON ResponsavelTDB.IdResponsavel = AlunoResponsavelTDB.IdResponsavel
                        WHERE
	                        EventoTDB.IdEvento = @IdEvento
                        Order by AlunoTDB.NomeAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdEvento";
            email1.Value = IdEvento;
            email1.SourceColumn = "IdEvento";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<AlunoPersonalizadoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        [Route("api/Aluno/ByAlunoId/{IdAluno}")]
        [ResponseType(typeof(AlunoPersonalizadoTDB))]
        public IEnumerable<AlunoPersonalizadoTDB> GetAlunoByAlunoId(Int32 IdAluno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"SELECT
	                        EventoTDB.IdEvento,
	                        AlunoTDB.IdAluno,
	                        AlunoTDB.NomeAluno,
	                        ResponsavelTDB.NomeResponsavel,
	                        ResponsavelTDB.CPFResponsavel,
                            AlunoTDB.idInstituicao,
                            AlunoTDB.serieAluno,
                            AlunoTDB.turnoAluno,
                            AlunoTDB.sexoAluno,
                            AlunoTDB.dataNascimentoAluno,
                            AlunoTDB.rendaFamiliaAluno,
                            AlunoTDB.planoSaudeAluno,
                            AlunoTDB.planoDentalAluno
                        FROM
	                        EventoTDB INNER JOIN AlunoTDB
		                        ON EventoTDB.IdEvento = AlunoTDB.IdEvento
	                        INNER JOIN AlunoResponsavelTDB
		                        ON AlunoResponsavelTDB.IdAluno = AlunoTDB.IdAluno
	                        INNER JOIN ResponsavelTDB
		                        ON ResponsavelTDB.IdResponsavel = AlunoResponsavelTDB.IdResponsavel
                        WHERE
	                        AlunoTDB.IdAluno = @IdAluno
                        Order by AlunoTDB.NomeAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdAluno";
            email1.Value = IdAluno;
            email1.SourceColumn = "IdAluno";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<AlunoPersonalizadoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // GET: api/Aluno/5
        [ResponseType(typeof(AlunoTDB))]
        public IHttpActionResult GetAlunoTDB(long id)
        {
            AlunoTDB alunoTDB = db.AlunoTDB.Find(id);
            if (alunoTDB == null)
            {
                return NotFound();
            }

            return Ok(alunoTDB);
        }

        // PUT: api/Aluno/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlunoTDB(long id, AlunoTDB alunoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alunoTDB.idAluno)
            {
                return BadRequest();
            }

            db.Entry(alunoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoTDBExists(id))
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

        // POST: api/Aluno
        [ResponseType(typeof(AlunoTDB))]
        public IHttpActionResult PostAlunoTDB(AlunoTDB alunoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlunoTDB.Add(alunoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alunoTDB.idAluno }, alunoTDB);
        }

        // DELETE: api/Aluno/5
        [ResponseType(typeof(AlunoTDB))]
        public IHttpActionResult DeleteAlunoTDB(long id)
        {
            AlunoTDB alunoTDB = db.AlunoTDB.Find(id);
            if (alunoTDB == null)
            {
                return NotFound();
            }

            db.AlunoTDB.Remove(alunoTDB);
            db.SaveChanges();

            return Ok(alunoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlunoTDBExists(long id)
        {
            return db.AlunoTDB.Count(e => e.idAluno == id) > 0;
        }
    }
}