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
    [Authorize]
    public class MarksController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Marks
        public IQueryable<Mark> GetMarks()
        {
            return db.Marks;
        }

        // GET: api/Marks/5
        [ResponseType(typeof(Mark))]
        [Route("api/Marks/{id}/{typeOfId}")]

        public IHttpActionResult GetMark(int id,String typeOfId)
        {
            if (typeOfId == "ProjectId")
            {
                var res = from p in db.Projects
                          join s in db.Students on p.Id equals s.ProjectId
                          join m in db.Marks on s.Id equals m.StudentId
                          join q in db.Questions on m.QuestionId equals q.Id
                          where p.Id == id && m.ExamId==3025
                        select new { 
                            q.Id,
                            q.Title,
                           studentid= s.Id,
                            s.Name,
                        
                        };


                return Ok(res);
            }
            
            return Ok();
        }

        // PUT: api/Marks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMark(int id, Mark mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mark.Id)
            {
                return BadRequest();
            }

            db.Entry(mark).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(id))
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

        // POST: api/Marks
        [ResponseType(typeof(Mark))]
        public IHttpActionResult PostMark(Mark mark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marks.Add(mark);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MarkExists(mark.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mark.Id }, mark);
        }

        // DELETE: api/Marks/5
        [ResponseType(typeof(Mark))]
        public IHttpActionResult DeleteMark(int id)
        {
            Mark mark = db.Marks.Find(id);
            if (mark == null)
            {
                return NotFound();
            }

            db.Marks.Remove(mark);
            db.SaveChanges();

            return Ok(mark);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarkExists(int id)
        {
            return db.Marks.Count(e => e.Id == id) > 0;
        }
    }
}