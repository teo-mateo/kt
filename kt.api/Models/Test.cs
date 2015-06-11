using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kt.api.Models
{
    public class Test
    {
        public int Id { get; set; }
        public Deck Deck { get; set; }
        public DateTime Date { get; set; }
        public int CountNOK { get; set; }
        public int CountSOSO { get; set; }
        public int CountOK { get; set; }
    }
}