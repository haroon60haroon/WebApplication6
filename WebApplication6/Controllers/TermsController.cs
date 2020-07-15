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
//using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Authorize(Roles = "Coordinator,Student,Supervisor")]

    public class TermsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Terms

        public IQueryable<Term> GetTerms()
        {
            return db.Terms.Where(t => t.IsActive == "True");
        }

        // GET: api/Terms/5
        [ResponseType(typeof(Term))]
        public IHttpActionResult GetTerm(int id)
        {
            Term term = db.Terms.Find(id);
            if (term == null)
            {
                return NotFound();
            }

            return Ok(term);
        }

        // PUT: api/Terms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTerm(int id, Term term)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != term.Id)
            {
                return BadRequest();
            }
            if (term.Title == "" || term.DateFrom == "" || term.DateTo == "" || term.Status == "")
            {
                return Ok();
            }

            if (String.Compare(term.DateFrom, term.DateTo) > 0)
            {
                return Ok(term);
            }

            if (term.Status != "True" && term.Status != "False")
            {
                return Ok(term);

            }
            try
            {
                db.Entry(term).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception)//DbUpdateConcurrencyException)
            {
                if (!TermExists(id))
                {
                    throw;// return NotFound();
                }
                else
                {
                    return Ok(term);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Terms

        [ResponseType(typeof(Term))]
        public IHttpActionResult PostTerm(Term term)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (term.Title == "" || term.DateFrom == "" || term.DateTo == "" || term.Status == "")
            {
                return Ok(term);
            }

            if (String.Compare(term.DateFrom, term.DateTo) > 0)
            {
                return Ok(term);
            }

            if (term.Status != "True" && term.Status != "False")
            {
                return Ok(term);

            }

            db.Terms.Add(term);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = term.Id }, term);
        }

        // DELETE: api/Terms/5

        [ResponseType(typeof(Term))]
        public IHttpActionResult DeleteTerm(int id)
        {
            Term term = db.Terms.Find(id);
            if (term == null)
            {
                return NotFound();
            }
            term.IsActive = "False";
            //db.Terms.Remove(term);
            db.Entry(term).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(term);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TermExists(int id)
        {
            return db.Terms.Count(e => e.Id == id) > 0;
        }
    }
}