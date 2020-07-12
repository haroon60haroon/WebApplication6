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
    public class ArtefactsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Artefacts
        public IQueryable<Artefact> GetArtefacts()
        {

            return db.Artefacts;
                }

        // GET: api/Artefacts/5
        [ResponseType(typeof(Artefact))]
        [Route("api/Artefacts/{id}/{typeOfId}")]

        public IHttpActionResult GetArtefact(int id, string typeOfId)
        {

            dynamic aRTEFACT;
            if (typeOfId == "Proposal Defense")
            {
                aRTEFACT = db.Artefacts.Where(a=> a.Title=="Proposal Defense");
            }
            if(typeOfId == "Document")
            {
                aRTEFACT = db.Artefacts.Where(a=>a.Title=="Document");
            }
            else
            {
                List<Artefact> aRTEFACTList = db.Artefacts.Where(a => a.Title == "Proposal Defense").ToList();
                aRTEFACT = aRTEFACTList;

                if (aRTEFACTList == null)
                {
                    return NotFound();
                }
            }

            return Ok(aRTEFACT);
            
        }

        // PUT: api/Artefacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtefact(int id, Artefact artefact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artefact.Id)
            {
                return BadRequest();
            }

            db.Entry(artefact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtefactExists(id))
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

        // POST: api/Artefacts
        [ResponseType(typeof(Artefact))]
        public IHttpActionResult PostArtefact(Artefact artefact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artefacts.Add(artefact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artefact.Id }, artefact);
        }

        // DELETE: api/Artefacts/5
        [ResponseType(typeof(Artefact))]
        public IHttpActionResult DeleteArtefact(int id)
        {
            Artefact artefact = db.Artefacts.Find(id);
            if (artefact == null)
            {
                return NotFound();
            }

            db.Artefacts.Remove(artefact);
            db.SaveChanges();

            return Ok(artefact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtefactExists(int id)
        {
            return db.Artefacts.Count(e => e.Id == id) > 0;
        }
    }
}