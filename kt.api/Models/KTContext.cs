using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace kt.api.Models
{
    public class KTContext : DbContext
    {
        public KTContext()
            : base(WebApiApplication.GetConnectionString())
        {

        }

        public DbSet<Deck> Decks { get; set; }
        public DbSet<CardSide> CardSides { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}