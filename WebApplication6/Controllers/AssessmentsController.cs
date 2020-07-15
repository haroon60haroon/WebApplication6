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
    public class AssessmentsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Assessments
        public IQueryable<Assessment> GetAssessments()
        {
            return db.Assessments;
        }

        // GET: api/Assessments/5
        [ResponseType(typeof(Assessment))]
        public IHttpActionResult GetAssessment(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return NotFound();
            }

            return Ok(assessment);
        }

        // PUT: api/Assessments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssessment(int id, Assessment assessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assessment.Id)
            {
                return BadRequest();
            }

            db.Entry(assessment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssessmentExists(id))
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

        // POST: api/Assessments
        [ResponseType(typeof(Assessment))]
        public IHttpActionResult PostAssessment(Assessment assessment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assessments.Add(assessment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assessment.Id }, assessment);
        }

        // DELETE: api/Assessments/5
        [ResponseType(typeof(Assessment))]
        public IHttpActionResult DeleteAssessment(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return NotFound();
            }

            db.Assessments.Remove(assessment);
            db.SaveChanges();

            return Ok(assessment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssessmentExists(int id)
        {
            return db.Assessments.Count(e => e.Id == id) > 0;
        }
    }
}