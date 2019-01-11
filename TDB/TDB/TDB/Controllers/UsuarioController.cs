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
    public class UsuarioController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Usuario
        public IQueryable<UsuarioTDB> GetUsuarioTDB()
        {
            return db.UsuarioTDB;
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(UsuarioTDB))]
        public IHttpActionResult GetUsuarioTDB(long id)
        {
            UsuarioTDB usuarioTDB = db.UsuarioTDB.Find(id);
            if (usuarioTDB == null)
            {
                return NotFound();
            }

            return Ok(usuarioTDB);
        }

        // PUT: api/Usuario/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuarioTDB(long id, UsuarioTDB usuarioTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarioTDB.IdUsuario)
            {
                return BadRequest();
            }

            db.Entry(usuarioTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioTDBExists(id))
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

        // POST: api/Usuario
        [ResponseType(typeof(UsuarioTDB))]
        public IHttpActionResult PostUsuarioTDB(UsuarioTDB usuarioTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UsuarioTDB.Add(usuarioTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuarioTDB.IdUsuario }, usuarioTDB);
        }

        [Route("api/Usuario/Login")]
        [ResponseType(typeof(UsuarioTDB))]
        public IEnumerable<UsuarioTDB> PostUsuarioLogin(UsuarioTDB user)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"Select
	                        IdUsuario, NomeUsuario, EmailUsuario, SenhaUsuario, StatusUsuario
                        FROM
	                        UsuarioTDB
                        WHERE
	                        EmailUsuario = @email
	                        AND SenhaUsuario = @pw");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@email";
            email1.Value = user.emailUsuario;
            email1.SourceColumn = "EmailUsuario";
            //dbCommand.Parameters.Add(email1);

            IDataParameter pwUser = new SqlParameter();
            pwUser.DbType = DbType.String;
            pwUser.ParameterName = "@pw";
            pwUser.Value = user.senhaUsuario;
            pwUser.SourceColumn = "SenhaUsuario";

            var resultado = db.Database.SqlQuery<UsuarioTDB>(str.ToString(),
                email1, pwUser).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // DELETE: api/Usuario/5
        [ResponseType(typeof(UsuarioTDB))]
        public IHttpActionResult DeleteUsuarioTDB(long id)
        {
            UsuarioTDB usuarioTDB = db.UsuarioTDB.Find(id);
            if (usuarioTDB == null)
            {
                return NotFound();
            }

            db.UsuarioTDB.Remove(usuarioTDB);
            db.SaveChanges();

            return Ok(usuarioTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioTDBExists(long id)
        {
            return db.UsuarioTDB.Count(e => e.IdUsuario == id) > 0;
        }
    }
}