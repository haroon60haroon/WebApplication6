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
    public class ProjectExamsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/ProjectExams
        public IQueryable<ProjectExam> GetProjectExams()
        {
            return db.ProjectExams;
        }

        // GET: api/ProjectExams/5
        [ResponseType(typeof(ProjectExam))]
        [Route("api/ProjectExams/{id}/{typeOfId}")]

        public IHttpActionResult GetProjectExam(int id)
        {

            ProjectExam projectExam = db.ProjectExams.Find(id);
            if (projectExam == null)
            {
                return NotFound();
            }

            return Ok(projectExam);
        }

        // PUT: api/ProjectExams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectExam(int id, ProjectExam projectExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectExam.Id)
            {
                return BadRequest();
            }

            db.Entry(projectExam).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExamExists(id))
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

        // POST: api/ProjectExams
        [ResponseType(typeof(ProjectExam))]
        public IHttpActionResult PostProjectExam(ProjectExam projectExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectExams.Add(projectExam);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectExam.Id }, projectExam);
        }

        // DELETE: api/ProjectExams/5
        [ResponseType(typeof(ProjectExam))]
        public IHttpActionResult DeleteProjectExam(int id)
        {
            ProjectExam projectExam = db.ProjectExams.Find(id);
            if (projectExam == null)
            {
                return NotFound();
            }

            db.ProjectExams.Remove(projectExam);
            db.SaveChanges();

            return Ok(projectExam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExamExists(int id)
        {
            return db.ProjectExams.Count(e => e.Id == id) > 0;
        }
    }
}