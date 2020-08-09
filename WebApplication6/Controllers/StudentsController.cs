using System;
using System.Collections;
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
    public class StudentsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Students
        public string  GetStudents()
        {
           return  User.Identity.Name;

        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        [Route("api/Students/{id}/{typeOfId}")]
        public IHttpActionResult GetStudent(int id, String typeOfId)
        {
            dynamic sTUDENTS;
            if (typeOfId == "STUDENTID")
            {
                sTUDENTS = db.Students.Find(id);

            }
            //if (typeOfId == "GROUPID")
            //{
            //    ArrayList studentList = new ArrayList();

            //    var result = from s in db.Students
            //                 join g in db.Groups on s.Name equals g.StudentId1
            //                 join g1 in db.Groups on s.Name equals g1.StudentId2
            //                 join g2 in db.Groups on s.Name equals g2.StudentId3
            //                 where s.Term.Status == "True" && s.Term.IsActive == "True"
            //                 select new
            //                 {
            //                     studentList.Add(s.Id)


            //                 };
            //    return Ok(studentList);

            //}



            if (typeOfId == "ProjectId")
            {
                var result = from p in db.Projects
                             join g in db.Groups on p.GroupId equals g.Id
                             join s in db.Students on p.Id equals s.ProjectId
                             where p.Id == id && p.GroupId==g.Id
                             select new
                             {    s.Id,
                                 s.RegNo,
                                 s.Name,
                                 s.Email,
                                 s.PhoneNo,
                                 s.Cgpa,
                                 s.CreditHourEnrolled,
                                 s.CreditHourCompleted
                             };
                return Ok(result);

            }
            
            else
            {
                List<Student> sTUDENTList = db.Students.Where(s => s.TermId == id && s.Term.IsActive == "True").ToList();
                sTUDENTS = sTUDENTList;
                if (sTUDENTList == null)
                {
                    return NotFound();
                }
            }

            return Ok(sTUDENTS);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.Id == id) > 0;
        }
    }
}