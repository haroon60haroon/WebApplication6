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
    public class PanelsController : ApiController
    {
        private CUSTFYPEntities1 db = new CUSTFYPEntities1();

        // GET: api/Panels
        public IQueryable<Panel> GetPanels()
        {
           // return db.Panels;
            return db.Panels.Where(t => t.IsActive == "True");

        }

        // GET: api/Panels/5
        [ResponseType(typeof(Panel))]
        [Route("api/Panels/{id}/{typeOfId}")]
        public IHttpActionResult GetPanel(int id,string typeOfId)
        {
            dynamic pANEL;
            if (typeOfId == "PANELID")
            {

                pANEL = db.Panels.Find(id);


                //     pANEL = db.PANELs.Where(p => p.id == id && p.IsActive == "True");

            }
            else
            {
                List<Panel> pANELList = db.Panels.Where(p => p.TermId == id && p.IsActive == "True").ToList();
                pANEL = pANELList;
                // pANEL = db.PANELs.Where(p => p.TermId == id).ToList();

                if (pANELList == null)
                {
                    return NotFound();
                }
            }

            return Ok(pANEL);




        }

        // PUT: api/Panels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPanel(int id, Panel panel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != panel.Id)
            {
                return BadRequest();
            }
            if (panel.Name == "")
            {
                return Ok(panel);
            }

            try
            {
                db.Entry(panel).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!PanelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(panel);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Panels
        [ResponseType(typeof(Panel))]
        public IHttpActionResult PostPanel(Panel panel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (panel.Name == "")
            {
                return Ok(panel);
            }

            db.Panels.Add(panel);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Ok(panel);
            }
           // db.Panels.Add(panel);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = panel.Id }, panel);
        }

        // DELETE: api/Panels/5
        [ResponseType(typeof(Panel))]
        public IHttpActionResult DeletePanel(int id)
        {
            Panel panel = db.Panels.Find(id);
            if (panel == null)
            {
                return NotFound();
            }
            panel.IsActive = "False";
            db.Entry(panel).State = EntityState.Modified;
          db.SaveChanges();
            return Ok(panel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PanelExists(int id)
        {
            return db.Panels.Count(e => e.Id == id) > 0;
        }
    }
}