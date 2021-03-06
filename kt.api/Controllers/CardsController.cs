﻿using System;
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
    public class CardsController : ApiController
    {
        private KTContext db = new KTContext();

        // GET: api/Cards
        public IQueryable<Card> GetCards()
        {
            return db.Cards
                .Include(o=>o.Front)
                .Include(o=>o.Back);
        }

        // GET: api/Cards/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult GetCard(string id)
        {
            Card card = db.Cards
                .Include(o=>o.Front)
                .Include(o=>o.Back)
                .First(p=>p.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/Cards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCard(string id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.Id)
            {
                return BadRequest();
            }

            db.Entry(card).State = EntityState.Modified;
            db.Entry(card.Front).State = EntityState.Modified;
            db.Entry(card.Back).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        [ResponseType(typeof(Card))]
        public IHttpActionResult PostCard(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.CardSides.Any(p=>p.Id == card.Front.Id))
            {
                db.CardSides.Attach(card.Front);
                db.Entry(card.Front).State = EntityState.Unchanged;
            }

            if (db.CardSides.Any(p=>p.Id == card.Back.Id))
            {
                db.CardSides.Attach(card.Back);
                db.Entry(card.Back).State = EntityState.Unchanged;
            }

            db.Cards.Add(card);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult DeleteCard(string id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok(card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardExists(string id)
        {
            return db.Cards.Count(e => e.Id == id) > 0;
        }
    }
}