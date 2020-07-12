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
    public class AnnouncementsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Announcements
        public IQueryable<Announcement> GetAnnouncements()
        {
            return db.Announcements.Where(a => a.IsActive_ == "True" && a.Term.Status =="True");
        }

        // GET: api/Announcements/5
        [ResponseType(typeof(Announcement))]
        [Route("api/Announcements/{id}/{typeOfId}")]

        public IHttpActionResult GetAnnouncement(int id, string typeOfId)
        {
            dynamic aNNOUNCEMENT;
            if (typeOfId == "ANNOUNCEMENTID")
            {

                aNNOUNCEMENT = db.Announcements.Find(id);


                //     pLO = db.PLOes.Where(p => p.id == id && p.IsActive == "True");

            }
            else
            {
                List<Announcement> aNNOUNCEMENTList = db.Announcements.Where(p => p.TermId == id && p.IsActive_ == "True" && p.Term.Status=="True").ToList();
                aNNOUNCEMENT = aNNOUNCEMENTList;
                // pLO = db.PLOes.Where(p => p.TermId == id).ToList();

                if (aNNOUNCEMENTList == null)
                {
                    return NotFound();
                }
            }

            return Ok(aNNOUNCEMENT);
        }

        // PUT: api/Announcements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnnouncement(int id, Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != announcement.Id)
            {
                return BadRequest();
            }

            db.Entry(announcement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
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

        // POST: api/Announcements
        [ResponseType(typeof(Announcement))]
        public IHttpActionResult PostAnnouncement(Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Announcements.Add(announcement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = announcement.Id }, announcement);
        }

        // DELETE: api/Announcements/5
        [ResponseType(typeof(Announcement))]
        public IHttpActionResult DeleteAnnouncement(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }

            announcement.IsActive_ = "False";
            db.Entry(announcement).State = EntityState.Modified; db.SaveChanges();

            return Ok(announcement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnnouncementExists(int id)
        {
            return db.Announcements.Count(e => e.Id == id) > 0;
        }
    }
}