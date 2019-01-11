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
    public class EventoController : ApiController
    {
        private MinhaConexao db = new MinhaConexao();

        // GET: api/Evento
        public IQueryable<EventoTDB> GetEventoTDB()
        {
            return db.EventoTDB;
        }

        // GET: api/Evento/5
        [ResponseType(typeof(EventoTDB))]
        public IHttpActionResult GetEventoTDB(long id)
        {
            EventoTDB eventoTDB = db.EventoTDB.Find(id);
            if (eventoTDB == null)
            {
                return NotFound();
            }

            return Ok(eventoTDB);
        }


        [Route("api/Evento/Instituicao/{IdInstituicao}")]
        [ResponseType(typeof(EventoTDB))]
        public IEnumerable<EventoTDB> GetEventoByInstituicao(Int32 idInstituicao)
        {
            StringBuilder str = new StringBuilder();
            str.Append(@"Select
	                        EventoTDB.DataFimEvento, EventoTDB.DataInicioEvento, EventoTDB.IdEvento, EventoTDB.IdInstituicao, EventoTDB.NomeEvento, EventoTDB.StatusEvento,
                            EventoTDB.LocalEvento, EventoTDB.ObservacaoEvento
                        From
	                        EventoTDB
                        Where EventoTDB.IdInstituicao = @idInstituicao
                        Order By EventoTDB.NomeEvento");

            //SqlCommand dbCommand = new SqlCommand(str.ToString());

            IDataParameter email1 = new SqlParameter();
            email1.DbType = DbType.String;
            email1.ParameterName = "@idInstituicao";
            email1.Value = idInstituicao;
            email1.SourceColumn = "IdInstituicao";
            //dbCommand.Parameters.Add(email1);


            var resultado = db.Database.SqlQuery<EventoTDB>(str.ToString(),
                email1).AsEnumerable();

            if (resultado == null)
                return null;

            return resultado;
            //return CreatedAtRoute("api/Usuarios/Login", new { id = user.IdUsuario }, user);
        }

        // PUT: api/Evento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventoTDB(long id, EventoTDB eventoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventoTDB.idEvento)
            {
                return BadRequest();
            }

            db.Entry(eventoTDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoTDBExists(id))
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

        // POST: api/Evento
        [ResponseType(typeof(EventoTDB))]
        public IHttpActionResult PostEventoTDB(EventoTDB eventoTDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventoTDB.Add(eventoTDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventoTDB.idEvento }, eventoTDB);
        }

        // DELETE: api/Evento/5
        [ResponseType(typeof(EventoTDB))]
        public IHttpActionResult DeleteEventoTDB(long id)
        {
            EventoTDB eventoTDB = db.EventoTDB.Find(id);
            if (eventoTDB == null)
            {
                return NotFound();
            }

            db.EventoTDB.Remove(eventoTDB);
            db.SaveChanges();

            return Ok(eventoTDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoTDBExists(long id)
        {
            return db.EventoTDB.Count(e => e.idEvento == id) > 0;
        }
    }
}