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
    public class DecksController : ApiController
    {
        private KTContext db = new KTContext();

        // GET: api/Decks
        public IQueryable<Deck> GetDecks()
        {
            return db.Decks;
        }

        // GET: api/Decks/5
        [ResponseType(typeof(Deck))]
        public IHttpActionResult GetDeck(int id)
        {
            Deck deck = db.Decks
                .Include(p => p.Cards)
                .Include(p => p.Cards.Select(c => c.Front))
                .Include(p => p.Cards.Select(c => c.Back))
                .Where(p=>p.Id == id).FirstOrDefault();

            if (deck == null)
            {
                return NotFound();
            }

            return Ok(deck);
        }

        // PUT: api/Decks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeck(int id, Deck deck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deck.Id)
            {
                return BadRequest();
            }

            db.Entry(deck).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(id))
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

        // POST: api/Decks
        [ResponseType(typeof(Deck))]
        public IHttpActionResult PostDeck(Deck deck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Decks.Add(deck);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deck.Id }, deck);
        }

        // DELETE: api/Decks/5
        [ResponseType(typeof(Deck))]
        public IHttpActionResult DeleteDeck(int id)
        {
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return NotFound();
            }

            db.Decks.Remove(deck);
            db.SaveChanges();

            return Ok(deck);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeckExists(int id)
        {
            return db.Decks.Count(e => e.Id == id) > 0;
        }
    }
}