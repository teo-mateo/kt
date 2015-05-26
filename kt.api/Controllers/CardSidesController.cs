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
using kt.api.Models;

namespace kt.api.Controllers
{
    public class CardSidesController : ApiController
    {
        private KTContext db = new KTContext();

        // GET: api/CardSides
        public IQueryable<CardSide> GetCardSides()
        {
            return db.CardSides;
        }

        // GET: api/CardSides/5
        [ResponseType(typeof(CardSide))]
        public IHttpActionResult GetCardSide(int id)
        {
            CardSide cardSide = db.CardSides.Find(id);
            if (cardSide == null)
            {
                return NotFound();
            }

            return Ok(cardSide);
        }

        // PUT: api/CardSides/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCardSide(int id, CardSide cardSide)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardSide.Id)
            {
                return BadRequest();
            }

            db.Entry(cardSide).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardSideExists(id))
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

        // POST: api/CardSides
        [ResponseType(typeof(CardSide))]
        public IHttpActionResult PostCardSide(CardSide cardSide)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CardSides.Add(cardSide);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cardSide.Id }, cardSide);
        }

        // DELETE: api/CardSides/5
        [ResponseType(typeof(CardSide))]
        public IHttpActionResult DeleteCardSide(int id)
        {
            CardSide cardSide = db.CardSides.Find(id);
            if (cardSide == null)
            {
                return NotFound();
            }

            db.CardSides.Remove(cardSide);
            db.SaveChanges();

            return Ok(cardSide);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardSideExists(int id)
        {
            return db.CardSides.Count(e => e.Id == id) > 0;
        }
    }
}