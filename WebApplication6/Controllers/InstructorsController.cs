﻿using System;
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
    public class InstructorsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Instructors
        public IQueryable<Instructor> GetInstructors()
        {
            return db.Instructors.Where(t => t.IsActive == "True" && t.Term.Status=="True");

        }

        // GET: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        [Route("api/Instructors/{id}/{typeOfId}")]

        public IHttpActionResult GetInstructor(int id,string typeOfId)
        {
            dynamic iSTRUCTORS;
            
            if (typeOfId == "INSTRUCTORID")
            {
                iSTRUCTORS = db.Instructors.Find(id);

                //  iSTRUCTORS = db.Instructors.Where(t => t.IsActive == "True");
                return Ok(iSTRUCTORS);

            }
             if (typeOfId == "PanelId")
            {
                List<Instructor> iSTRUCTORList = db.Instructors.Where(e => e.PanelId == id && e.IsActive == "True").ToList();
                // eXAMS = db.Exams.Where(e => e.TermId == id).ToList();
                iSTRUCTORS = iSTRUCTORList;
                if (iSTRUCTORList == null)
                {
                    return NotFound();
                }
                return Ok(iSTRUCTORS);
            }
            if (typeOfId == "ProjectId") {
                var result = from p in db.Projects
                             join g in db.Groups on p.GroupId equals g.Id
                             join pan in db.Panels on g.PanelId equals pan.Id
                             join inst in db.Instructors on pan.Id equals inst.PanelId
                             where p.Id == id && g.PanelId==inst.PanelId
                             select new
                             {
                                 inst.Name,
                                 inst.Designation,
                                 inst.Email
                             };
                return Ok(result);

            }
            if (typeOfId == "intresult") {

                var result = from i in db.Instructors
                             where i.Email == User.Identity.Name
                             select new
                             {
                                 i.Id
 
                             };


                return Ok(result);
            }
            if (typeOfId == "intpanel")
            {

                var result = from i in db.Instructors
                             where i.Email == User.Identity.Name
                             select new
                             {
                                
                                 i.PanelId
                             };


                return Ok(result);
            }

            else
            {
                var result = from i in db.Instructors
                             where i.Email == User.Identity.Name
                             select new
                             {
                                 i.Id
                             };


                return Ok(result);
            }

        }

        // PUT: api/Instructors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInstructor(int id, Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructor.Id)
            {
                return BadRequest();
            }
            if (instructor.Name == "" || instructor.Email == "" || instructor.Designation == "")
            {
                return Ok(instructor);
            }


            try
            {
                db.Entry(instructor).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!InstructorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(instructor);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Instructors
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult PostInstructor(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (instructor.Name == "" || instructor.Email == "" || instructor.PanelId== -1 || instructor.Designation==""|| instructor.TermId == -1)
            {
                return Ok(instructor);
            }
            db.Instructors.Add(instructor);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Ok(instructor);
            }
          //  db.Instructors.Add(instructor);
           // db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = instructor.Id }, instructor);
        }

        // DELETE: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult DeleteInstructor(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            instructor.IsActive = "False";
            db.Entry(instructor).State = EntityState.Modified;
            db.SaveChanges();


            return Ok(instructor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.Instructors.Count(e => e.Id == id) > 0;
        }
    }
}