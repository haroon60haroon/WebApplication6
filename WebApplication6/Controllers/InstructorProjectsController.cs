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
    public class InstructorProjectsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/InstructorProjects
        public IQueryable<InstructorProject> GetInstructorProjects()
        {
            return db.InstructorProjects;
        }

        // GET: api/InstructorProjects/5
        [ResponseType(typeof(InstructorProject))]
        public IHttpActionResult GetInstructorProject(int id)
        {
            InstructorProject instructorProject = db.InstructorProjects.Find(id);
            if (instructorProject == null)
            {
                return NotFound();
            }

            return Ok(instructorProject);
        }

        // PUT: api/InstructorProjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstructorProject(int id, InstructorProject instructorProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructorProject.Id)
            {
                return BadRequest();
            }

            db.Entry(instructorProject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorProjectExists(id))
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

        // POST: api/InstructorProjects
        [ResponseType(typeof(InstructorProject))]
        public IHttpActionResult PostInstructorProject(InstructorProject instructorProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InstructorProjects.Add(instructorProject);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = instructorProject.Id }, instructorProject);
        }

        // DELETE: api/InstructorProjects/5
        [ResponseType(typeof(InstructorProject))]
        public IHttpActionResult DeleteInstructorProject(int id)
        {
            InstructorProject instructorProject = db.InstructorProjects.Find(id);
            if (instructorProject == null)
            {
                return NotFound();
            }

            db.InstructorProjects.Remove(instructorProject);
            db.SaveChanges();

            return Ok(instructorProject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorProjectExists(int id)
        {
            return db.InstructorProjects.Count(e => e.Id == id) > 0;
        }
    }
}