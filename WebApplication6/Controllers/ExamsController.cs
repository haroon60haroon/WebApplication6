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
    public class ExamsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Exams
        public IQueryable<Exam> GetExams()
        {
            return db.Exams.Where(t => t.IsActive == "True");

        }

        // GET: api/Exams/5
        [ResponseType(typeof(Exam))]
        [Route("api/Exams/{id}/{typeOfId}")]
        public IHttpActionResult GetExam(int id,string typeOfId)
        {
            dynamic eXAMS;
            if (typeOfId == "EXAMID")
            {
                // eXAMS = db.Exams.Where(t => t.IsActive == "True");
                eXAMS = db.Exams.Find(id);


            }
            else
            {
                List<Exam> eXAMList = db.Exams.Where(e => e.TermId == id && e.IsActive == "True").ToList();
                // eXAMS = db.Exams.Where(e => e.TermId == id).ToList();
                eXAMS = eXAMList;
                if (eXAMList == null)
                {
                    return NotFound();
                }
            }

            return Ok(eXAMS);
        }

        // PUT: api/Exams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExam(int id, Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exam.Id)
            {
                return BadRequest();
            }
            if (exam.Title == "" || exam.AnnouncedDate == "" || exam.DeadIine == "" || exam.Weightage == 0)
            {
                return Ok(exam);
            }

            if (String.Compare(exam.AnnouncedDate, exam.DeadIine) > 0)
            {
                return Ok(exam);
            }

            try
            {
                db.Entry(exam).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(exam);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Exams
        [ResponseType(typeof(Exam))]
        public IHttpActionResult PostExam(Exam exam)
        {
          //  if (!ModelState.IsValid)
          //  {
          //      return BadRequest(ModelState);
          //  }
            if (exam.Title == "" || exam.AnnouncedDate == "" || exam.DeadIine == "" || exam.Weightage == 0)
            {
                return Ok(exam);
            }

            if (String.Compare(exam.AnnouncedDate, exam.DeadIine) > 0)
            {
                return Ok(exam);
            }

            db.Exams.Add(exam);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

         //   db.Exams.Add(exam);
           // db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exam.Id }, exam);
        }

        // DELETE: api/Exams/5
        [ResponseType(typeof(Exam))]
        public IHttpActionResult DeleteExam(int id)
        {
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return NotFound();
            }
            exam.IsActive = "False";
            db.Entry(exam).State = EntityState.Modified;

            db.SaveChanges();
            return Ok(exam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamExists(int id)
        {
            return db.Exams.Count(e => e.Id == id) > 0;
        }
    }
}