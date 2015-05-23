﻿using System;
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
        public Deck Deck { get; set; }
        public CardStatus Status { get; set; }

        public CardSide Front { get; set; }
        public CardSide Back { get; set; }
    }
}