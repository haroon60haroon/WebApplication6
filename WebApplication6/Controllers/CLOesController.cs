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
    public class CLOesController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/CLOes
        public IQueryable<CLO> GetCLOes()
        {
            return db.CLOes.Where(t => t.IsActive == "True");

        }

        // GET: api/CLOes/5
        [ResponseType(typeof(CLO))]
        [Route("api/CLOes/{id}/{typeOfId}")]

        public IHttpActionResult GetCLO(int id,String typeOfId)
        {
            dynamic cLO;
            if (typeOfId == "TERMId")
            {
               
                List<CLO> cLOList = db.CLOes.Where(c => c.TermId == id && c.IsActive == "True").ToList();
                cLO = cLOList;
                // cLO = db.CLOes.Where(c => c.PLOId == id).ToList();
                if (cLOList == null)
                {
                    return NotFound();
                }

            }
           else if (typeOfId == "CLOID")
            {

                cLO = db.CLOes.Find(id);


                //     pLO = db.PLOes.Where(p => p.id == id && p.IsActive == "True");

            }
            else
            {
                List<CLO> cLOList = db.CLOes.Where(c => c.PLOId == id && c.IsActive == "True").ToList();
                cLO = cLOList;
                // cLO = db.CLOes.Where(c => c.PLOId == id).ToList();
                if (cLOList == null)
                {
                    return NotFound();
                }
            }

            return Ok(cLO);

      }

        // PUT: api/CLOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCLO(int id, CLO cLO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cLO.id)
            {
                return BadRequest();
            }
            if (cLO.Title == "")
            {
                return Ok(cLO);
            }



            try
            {
                db.Entry(cLO).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!CLOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(cLO);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CLOes
        [ResponseType(typeof(CLO))]
        public IHttpActionResult PostCLO(CLO cLO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (cLO.Title == "" || cLO.PLOId == 0 || cLO.TermId == -1)
                {
                    return Ok(cLO);
                }
            }
            catch (Exception)
            {
                throw;
            }

         
            db.CLOes.Add(cLO);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Ok(cLO);
            }

          //  db.CLOes.Add(cLO);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cLO.id }, cLO);
        }

        // DELETE: api/CLOes/5
        [ResponseType(typeof(CLO))]
        public IHttpActionResult DeleteCLO(int id)
        {
            CLO cLO = db.CLOes.Find(id);
            if (cLO == null)
            {
                return NotFound();
            }

            cLO.IsActive = "False";
            db.Entry(cLO).State = EntityState.Modified;
            db.SaveChanges();
          

            return Ok(cLO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CLOExists(int id)
        {
            return db.CLOes.Count(e => e.id == id) > 0;
        }
    }
}