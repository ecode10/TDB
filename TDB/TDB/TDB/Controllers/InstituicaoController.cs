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
    public class InstituicaoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Instituicao
        public IQueryable<InstituicaoTDB> GetInstituicaoTDB()
        {
            return db.InstituicaoTDB;
        }

        // GET: api/Instituicao/5
        [ResponseType(typeof(InstituicaoTDB))]
        public IHttpActionResult GetInstituicaoTDB(long id)
        {
            InstituicaoTDB instituicaoTDB = db.InstituicaoTDB.Find(id);
            if (instituicaoTDB == null)
            {
                return NotFound();
            }

            return Ok(instituicaoTDB);
        }

        [Route("api/Instituicao/nome/{nome}")]
        [ResponseType(typeof(UsuarioTDB))]
        public IEnumerable<InstituicaoTDB> GetInstituicaoByNome(String nome)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"Select
	                        IdInstituicao, NomeInstituicao, EnderecoInstituicao, CidadeInstituicao, EstadoInstituicao, CEPInstituicao, StatusInstituicao, TelefoneInstituicao
                        From
	                        InstituicaoTDB
                        Where
	                        InstituicaoTDB.NomeInstituicao like @nomeInstituicao
                        Order by InstituicaoTDB.NomeInstituicao");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@nomeInstituicao";
            email1.Value = '%' + nome + '%';
            email1.SourceColumn = "NomeInstituicao";
            //dbCommand.Parameters.Add(email1);
            

            var resultado = db.Database.SqlQuery<InstituicaoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // PUT: api/Instituicao/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstituicaoTDB(long id, InstituicaoTDB instituicaoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instituicaoTDB.idInstituicao)
            {
                return BadRequest();
            }

            db.Entry(instituicaoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituicaoTDBExists(id))
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

        // POST: api/Instituicao
        [ResponseType(typeof(InstituicaoTDB))]
        public IHttpActionResult PostInstituicaoTDB(InstituicaoTDB instituicaoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InstituicaoTDB.Add(instituicaoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = instituicaoTDB.idInstituicao }, instituicaoTDB);
        }

        // DELETE: api/Instituicao/5
        [ResponseType(typeof(InstituicaoTDB))]
        public IHttpActionResult DeleteInstituicaoTDB(long id)
        {
            InstituicaoTDB instituicaoTDB = db.InstituicaoTDB.Find(id);
            if (instituicaoTDB == null)
            {
                return NotFound();
            }

            db.InstituicaoTDB.Remove(instituicaoTDB);
            db.SaveChanges();

            return Ok(instituicaoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstituicaoTDBExists(long id)
        {
            return db.InstituicaoTDB.Count(e => e.idInstituicao == id) > 0;
        }
    }
}