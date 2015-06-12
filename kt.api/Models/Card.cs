using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kt.api.Models
{
    public enum CardStatus
    {
        NOK = 1,
        SOSO = 2, 
        OK = 3
    }

    public class Card
    {
        public string Id { get; set; }
        public int DeckId { get; set; }
        public CardStatus Status { get; set; }

        public virtual CardSide Front { get; set; }
        public virtual CardSide Back { get; set; }
        public DateTime? LastShown { get; set; }

    }
}