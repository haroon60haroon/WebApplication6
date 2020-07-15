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
    public class ProjectsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Projects
        public IQueryable<Project> GetProjects()
          // public ArrayList GetProjects()
        {
           // string Email = "abdulqadir@gmail.com";//User.Identity.Name;
           // Instructor instructor = (db.Instructors.Where(inst => inst.Email.Equals(Email))).SingleOrDefault();
           // var projects = (db.InstructorProjects.Where(proj => proj.InstructorId.Equals(instructor.Id)));
           //// IList<InstructorProject> projects = (db.InstructorProjects.Where(proj => proj.InstructorId.Equals(instructor.Id))).ToList();

           // ArrayList projList = new ArrayList( );
           // foreach (InstructorProject ip in projects)
           // {
           //     Project p = (db.Projects.Where(pr => pr.Id.Equals(ip.ProjectId))).SingleOrDefault();
           //     projList.Add(p);
           // }
            //  return projList;
            return db.Projects;
        }




        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        [Route("api/Projects/{id}/{typeOfId}")]
        public IHttpActionResult GetProject(int id, String typeOfId)
        {
            dynamic pROJECTS;
            if (typeOfId == "PROJECTID")
            {

                pROJECTS = db.Projects.Find(id);
            }
            if(typeOfId== "TermId")
            {
                var result = from p in db.Projects
                             join g in db.Groups on p.GroupId equals g.Id
                             join s in db.Students on g.StudentId1 equals s.Name
                             where p.TermId == id && p.IsActive == "True" && g.IsActive=="True" 
                             select new {
                                 p.Id,
                                 p.Title,
                                 g.StudentId1,
                                 g.StudentId2,
                                 g.StudentId3,
                                 g.gName
                             };
                return Ok(result);
            }
            if (typeOfId == "ProjectId")
            {
                var result = from p in db.Projects
                             join g in db.Groups on p.GroupId equals g.Id
                             join t in db.Terms on p.TermId equals t.Id
                             join ins in db.InstructorProjects on p.Id equals ins.ProjectId
                             join inst in db.Instructors on ins.InstructorId equals inst.Id
                             join pan in db.Panels on inst.PanelId equals pan.Id
                             where p.Id == id && p.IsActive == "True"
                             select new
                             {
                                 p.Title,
                                 panelname=pan.Name,
                                 g.gName,
                                 termtitle=t.Title,
                                 inst.Name
                                 
                             };
                return Ok(result);
            }
            if (typeOfId == "projectdettaildisplayid")
            {
                List<Project> cLOList = db.Projects.Where(c => c.TermId == id && c.IsActive == "True").ToList();
                pROJECTS = cLOList;
                // cLO = db.CLOes.Where(c => c.PLOId == id).ToList();
                if (cLOList == null)
                {
                    return NotFound();
                }
                return Ok(pROJECTS);
            }
            return Ok();
        } 
    

       //var res = from p in db.Projects
    //          join g in db.Groups on p.GroupId equals g.Id
    //          join s in db.Students on g.TermId equals s.TermId
    //          where p.TermId == id && p.IsActive == "True"
    //          select new
    //          {
    //              p.Title,
    //              g.StudentId1,
    //              g.StudentId2,
    //              g.StudentId3,
    //              g.gName
    //          };


        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.Id)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
             Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            project.IsActive = "False";
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.Id == id) > 0;
        }
    }
}