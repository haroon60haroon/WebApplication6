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
    public class QuestionsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Questions
        public IQueryable<Question> GetQuestions()
        {
            return db.Questions.Where(t => t.IsActive == "True");

        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        [Route("api/Questions/{id}/{typeOfId}")]

        public IHttpActionResult GetQuestion(int id,string typeOfId)
        {
            dynamic qUESTIONS;
            if (typeOfId == "QUESTIONID")
            {
                // qUESTIONS = db.Questions.Where(t => t.IsActive == "True");
                qUESTIONS = db.Questions.Find(id);
                return Ok(qUESTIONS);
            }
            if (typeOfId == "EXAMID")
            {
                // qUESTIONS = db.Questions.Where(t => t.IsActive == "True");
                List<Question> qUESTIONList = db.Questions.Where(p => p.ExamId == id && p.IsActive == "True").ToList();
                qUESTIONS = qUESTIONList;

                //qUESTIONS = db.Questions.Find(id);
                if (qUESTIONList == null)
                {
                    return NotFound();
                }
            }
            if (typeOfId == "ProjectId")
            {

                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive=="True" && e.Title =="Proposal Defense" && t.Status=="True" && t.IsActive=="True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
            }
            //if (typeOfId=="ProjectId") {

            //    var result = from e in db.Exams
            //                 join q in db.Questions on e.TermId equals q.TermId
            //                 join c in db.CLOes on e.TermId equals c.TermId
            //                 join s in db.Students on  q.TermId equals s.TermId
            //                 join p in db.Projects on s.ProjectId equals p.Id
            //                 where p.Id==id && q.ExamId==e.Id && q.CLOId==c.id && q.ExamId==3025
            //                 select new
            //                 {   pid=p.Id,   
            //                     q.Id,
            //                       eid= e.Id,
            //                     q.Title,
            //                     clotitle=c.Title,
            //                     stdid=s.Id,
            //                     s.RegNo,
            //                     s.Name

            //                 };
            //    return Ok(result);
            //}
            if (typeOfId == "ProjectIdformid1")
            {
                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive == "True" && e.Title == "Mid Exam 1" && t.Status == "True" && t.IsActive == "True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
                //var result = from e in db.Exams
                //             join q in db.Questions on e.TermId equals q.TermId
                //             join c in db.CLOes on e.TermId equals c.TermId
                //             join s in db.Students on q.TermId equals s.TermId
                //             join p in db.Projects on s.ProjectId equals p.Id
                //             where p.Id == id && q.ExamId == e.Id && q.CLOId == c.id && q.ExamId == 3026
                //             select new
                //             {
                //                 pid = p.Id,
                //                 q.Id,
                //                 eid = e.Id,
                //                 q.Title,
                //                 clotitle = c.Title,
                //                 stdid = s.Id,
                //                 s.RegNo,
                //                 s.Name

                //             };
                //return Ok(result);
            }
            if (typeOfId == "ProjectIdforattendence1")
            {
                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive == "True" && e.Title == "Attendence 1" && t.Status == "True" && t.IsActive == "True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
                //var result = from e in db.Exams
                //             join q in db.Questions on e.TermId equals q.TermId
                //             join c in db.CLOes on e.TermId equals c.TermId
                //             join s in db.Students on q.TermId equals s.TermId
                //             join p in db.Projects on s.ProjectId equals p.Id
                //             where p.Id == id && q.ExamId == e.Id && q.CLOId == c.id && q.ExamId == 4032
                //             select new
                //             {
                //                 pid = p.Id,
                //                 q.Id,
                //                 eid = e.Id,
                //                 q.Title,
                //                 clotitle = c.Title,
                //                 stdid = s.Id,
                //                 s.RegNo,
                //                 s.Name

                //             };
                //return Ok(result);
            }
            if (typeOfId == "ProjectIdforfinal1")
            {
                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive == "True" && e.Title == "Final Exam 1" && t.Status == "True" && t.IsActive == "True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
                //var result = from e in db.Exams
                //             join q in db.Questions on e.TermId equals q.TermId
                //             join c in db.CLOes on e.TermId equals c.TermId
                //             join s in db.Students on q.TermId equals s.TermId
                //             join p in db.Projects on s.ProjectId equals p.Id
                //             where p.Id == id && q.ExamId == e.Id && q.CLOId == c.id && q.ExamId == 3027
                //             select new
                //             {
                //                 pid = p.Id,
                //                 q.Id,
                //                 eid = e.Id,
                //                 q.Title,
                //                 clotitle = c.Title,
                //                 stdid = s.Id,
                //                 s.RegNo,
                //                 s.Name

                //             };
                //return Ok(result);
            }
            if (typeOfId == "ProjectIdformid2")
            {
                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive == "True" && e.Title == "Mid Exam 2" && t.Status == "True" && t.IsActive == "True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
                //var result = from e in db.Exams
                //             join q in db.Questions on e.TermId equals q.TermId
                //             join c in db.CLOes on e.TermId equals c.TermId
                //             join s in db.Students on q.TermId equals s.TermId
                //             join p in db.Projects on s.ProjectId equals p.Id
                //             where p.Id == id && q.ExamId == e.Id && q.CLOId == c.id && q.ExamId == 3028
                //             select new
                //             {
                //                 pid = p.Id,
                //                 q.Id,
                //                 eid = e.Id,
                //                 q.Title,
                //                 clotitle = c.Title,
                //                 stdid = s.Id,
                //                 s.RegNo,
                //                 s.Name

                //             };
                //return Ok(result);
            }
            if (typeOfId == "ProjectIdforattendence2")
            {
                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive == "True" && e.Title == "Attendence 2" && t.Status == "True" && t.IsActive == "True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
                //var result = from e in db.Exams
                //             join q in db.Questions on e.TermId equals q.TermId
                //             join c in db.CLOes on e.TermId equals c.TermId
                //             join s in db.Students on q.TermId equals s.TermId
                //             join p in db.Projects on s.ProjectId equals p.Id
                //             where p.Id == id && q.ExamId == e.Id && q.CLOId == c.id && q.ExamId == 4034
                //             select new
                //             {
                //                 pid = p.Id,
                //                 q.Id,
                //                 eid = e.Id,
                //                 q.Title,
                //                 clotitle = c.Title,
                //                 stdid = s.Id,
                //                 s.RegNo,
                //                 s.Name

                //             };
                //return Ok(result);
            }
            if (typeOfId == "ProjectIdforfinal2")
            {
                var result = from e in db.Exams
                             join q in db.Questions on e.TermId equals q.TermId
                             join c in db.CLOes on e.TermId equals c.TermId
                             join t in db.Terms on e.TermId equals t.Id
                             where q.ExamId == e.Id && q.CLOId == c.id && q.IsActive == "True" && e.Title == "Final Exam 2" && t.Status == "True" && t.IsActive == "True"
                             select new
                             {
                                 q.Id,
                                 q.Title,
                                 clotitle = c.Title
                             };
                return Ok(result);
                //var result = from e in db.Exams
                //             join q in db.Questions on e.TermId equals q.TermId
                //             join c in db.CLOes on e.TermId equals c.TermId
                //             join s in db.Students on q.TermId equals s.TermId
                //             join p in db.Projects on s.ProjectId equals p.Id
                //             where p.Id == id && q.ExamId == e.Id && q.CLOId == c.id && q.ExamId == 3029
                //             select new
                //             {
                //                 pid = p.Id,
                //                 q.Id,
                //                 eid = e.Id,
                //                 q.Title,
                //                 clotitle = c.Title,
                //                 stdid = s.Id,
                //                 s.RegNo,
                //                 s.Name

                //             };
                //return Ok(result);
            }
            else
            {
                List<Question> qUESTIONList = db.Questions.Where(p => p.ExamId == id && p.IsActive == "True").ToList();
                // List<Question> qUESTIONList = db.Questions.Where(p => p.TermId == id && p.IsActive == "True").ToList();
                qUESTIONS = qUESTIONList;
                // pLO = db.PLOes.Where(p => p.TermId == id).ToList();

                if (qUESTIONList == null)
                {
                    return NotFound();
                }
            }

            return Ok(qUESTIONS);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }
            if (question.Title == "" || question.ExamId == -1 || question.CLOId == -1 || question.TermId == -1)
            {
                return Ok(question);
            }

            try
            {
                db.Entry(question).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(question);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            //   if (!ModelState.IsValid)
            //  {
            //      return BadRequest(ModelState);
            //  }

           
            if (question.Title == "" || question.ExamId==-1 || question.CLOId == -1 || question.TermId == -1)
            {
                return Ok(question);
            }

        
        db.Questions.Add(question);
            try
            {
                db.SaveChanges();
            }
            catch (Exception) {
                throw; 
            }

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            question.IsActive = "False";
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Questions.Count(e => e.Id == id) > 0;
        }
    }
}