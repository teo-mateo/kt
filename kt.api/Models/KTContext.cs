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

        public virtual DbSet<Deck> Decks { get; set; }
        public virtual DbSet<CardSide> CardSides { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
    }
}