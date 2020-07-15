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
    public class GroupsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Groups
        public string GetGroups()
        {
            return User.Identity.Name;
        }

        // GET: api/Groups/5
        [ResponseType(typeof(Group))]
        [Route("api/Groups/{id}/{typeOfId}")]
        public IHttpActionResult GetGroup(int id, String typeOfId)
        {
            dynamic group;
            if (typeOfId == "GroupId")
            {

                group = db.Groups.Find(id);


                //     pLO = db.PLOes.Where(p => p.id == id && p.IsActive == "True");

            }

            else
            {
                List<Group> groupList = db.Groups.Where(p => p.TermId == id && p.IsActive == "True").ToList();
                group = groupList;
                // pLO = db.PLOes.Where(p => p.TermId == id).ToList();

                if (groupList == null)
                {
                    return NotFound();
                }
            }

            return Ok(group);

        }

        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroup(int id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.Id)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [ResponseType(typeof(Group))]
        public IHttpActionResult PostGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groups.Add(group);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = group.Id }, group);
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(int id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }
            group.IsActive = "False";
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.Id == id) > 0;
        }
    }
}