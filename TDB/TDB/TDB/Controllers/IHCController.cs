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
    public class IHCController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/IHC
        public IQueryable<IHCTDB> GetIHCTDB()
        {
            return db.IHCTDB;
        }

        // GET: api/IHC/5
        [ResponseType(typeof(IHCTDB))]
        public IHttpActionResult GetIHCTDB(long id)
        {
            IHCTDB iHCTDB = db.IHCTDB.Find(id);
            if (iHCTDB == null)
            {
                return NotFound();
            }

            return Ok(iHCTDB);
        }

        // PUT: api/IHC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIHCTDB(long id, IHCTDB iHCTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iHCTDB.idIHC)
            {
                return BadRequest();
            }

            db.Entry(iHCTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IHCTDBExists(id))
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

        [Route("api/IHC/Aluno/{IdAluno}")]
        [ResponseType(typeof(IHCTDB))]
        public IEnumerable<IHCTDB> GetIHCByAluno(Int32 IdAluno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"Select
	                        IdIHC, IdAluno, ControleDoenca, 
                            ControleLesao, TratamentoEspecializado,
                            NecessidadeEspecialista,
                            PacienteComDor, descricaoEspecializado
                        From
	                        IHCTDB
                        Where 
	                        IdAluno = @IdAluno");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@IdAluno";
            email1.Value = IdAluno;
            email1.SourceColumn = "IdAluno";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<IHCTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // POST: api/IHC
        [ResponseType(typeof(IHCTDB))]
        public IHttpActionResult PostIHCTDB(IHCTDB iHCTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IHCTDB.Add(iHCTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iHCTDB.idIHC }, iHCTDB);
        }

        // DELETE: api/IHC/5
        [ResponseType(typeof(IHCTDB))]
        public IHttpActionResult DeleteIHCTDB(long id)
        {
            IHCTDB iHCTDB = db.IHCTDB.Find(id);
            if (iHCTDB == null)
            {
                return NotFound();
            }

            db.IHCTDB.Remove(iHCTDB);
            db.SaveChanges();

            return Ok(iHCTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IHCTDBExists(long id)
        {
            return db.IHCTDB.Count(e => e.idIHC == id) > 0;
        }
    }
}