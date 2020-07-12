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
    public class PLOesController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/PLOes
        public string GetPLOes()
        {
            return User.Identity.Name;
        }

        // GET: api/PLOes/5
        [ResponseType(typeof(PLO))]
        [Route("api/PLOes/{id}/{typeOfId}")]
        public IHttpActionResult GetPLO(int id, string typeOfId)
        
        {
            dynamic pLO;
            if (typeOfId == "PLOID")            {
                 pLO = db.PLOes.Find(id);
          //     pLO = db.PLOes.Where(p => p.id == id && p.IsActive == "True");
            }
            else
            {
                 List<PLO> pLOList = db.PLOes.Where(p => p.TermId == id && p.IsActive == "True").ToList();
                pLO = pLOList;
               // pLO = db.PLOes.Where(p => p.TermId == id).ToList();

                if (pLOList == null)
                {
                    return NotFound();
                }
            }

            return Ok(pLO);
        }

        // PUT: api/PLOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPLO(int id, PLO pLO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pLO.id)
            {
                return BadRequest();
            }
            if (pLO.Title == "")
            {
                return Ok(pLO);
            }


            try
            {
                db.Entry(pLO).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception)//DbUpdateConcurrencyException)
            {
                if (!PLOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(pLO);
                    
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PLOes
        [ResponseType(typeof(PLO))]
        public IHttpActionResult PostPLO(PLO pLO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pLO.Title == ""){
                return Ok(pLO);
            }
            db.PLOes.Add(pLO);
            try{
                db.SaveChanges();
            }
            catch (Exception){
                return Ok(pLO);
            }

         //   db.PLOes.Add(pLO);
           // db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pLO.id }, pLO);
        }

        // DELETE: api/PLOes/5
        [ResponseType(typeof(PLO))]
        public IHttpActionResult DeletePLO(int id)
        {
            PLO pLO = db.PLOes.Find(id);
            if (pLO == null)
            {
                return NotFound();
            }
            pLO.IsActive = "False";
            db.Entry(pLO).State = EntityState.Modified;

            db.SaveChanges();

            return Ok(pLO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PLOExists(int id)
        {
            return db.PLOes.Count(e => e.id == id) > 0;
        }
    }
}