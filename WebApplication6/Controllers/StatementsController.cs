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
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Authorize(Roles = "Coordinator,Student,Supervisor")]
    public class StatementsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Statements
        public IQueryable<Statement> GetStatements()
        {
            return db.Statements;
        }

        // GET: api/Statements/5
        [ResponseType(typeof(Statement))]
        public IHttpActionResult GetStatement(int id)
        {
            Statement statement = db.Statements.Find(id);
            if (statement == null)
            {
                return NotFound();
            }

            return Ok(statement);
        }

        // PUT: api/Statements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatement(int id, Statement statement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statement.Id)
            {
                return BadRequest();
            }

            db.Entry(statement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatementExists(id))
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

        // POST: api/Statements
        [ResponseType(typeof(Statement))]
        public IHttpActionResult PostStatement(Statement statement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statements.Add(statement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = statement.Id }, statement);
        }

        // DELETE: api/Statements/5
        [ResponseType(typeof(Statement))]
        public IHttpActionResult DeleteStatement(int id)
        {
            Statement statement = db.Statements.Find(id);
            if (statement == null)
            {
                return NotFound();
            }

            db.Statements.Remove(statement);
            db.SaveChanges();

            return Ok(statement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatementExists(int id)
        {
            return db.Statements.Count(e => e.Id == id) > 0;
        }
    }
}