using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace kt.api.Models
{
    public class Test
    {
        public int Id { get; set; }
        public Deck Deck { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<TestCard> Cards {get;set;}

        public static Test CreateNew(KTContext db, int deckId)
        {
            //populate with cards
            //5 from NOK always
            //5 from SOSO if date diff > 3
            //  else 5 from NOK
            //5 from ok if date diff > 7
            //  else 5 from NOK

            //date diff = date since last test for this deck

            int dateDiff = 999;

            Test lastTestForDeck = db.Tests.OrderByDescending(p => p.Date).FirstOrDefault(p => p.Deck.Id == deckId);
            if (lastTestForDeck != null)
            {
                dateDiff = DateTime.Now.Subtract(lastTestForDeck.Date).Days;
            }

            int take_nok = 5;



            IEnumerable<Card> nok;
            IEnumerable<Card> soso = new List<Card>();
            IEnumerable<Card> ok = new List<Card>();

            if (dateDiff >= 3)
            {
                soso = (from c in db.Cards
                        where c.DeckId == deckId && c.Status == CardStatus.SOSO
                        select c)
                            .OrderBy(p => Guid.NewGuid())
                            .Take(5);
            }
            else // 5 more NOW
            {
                take_nok += 5;
            }


            if (dateDiff >= 7)
            {
                ok = (from c in db.Cards
                      where c.DeckId == deckId && c.Status == CardStatus.OK
                      select c)
                          .OrderBy(p => Guid.NewGuid())
                          .Take(5);
            }
            else
            {
                take_nok += 5;
            }

            nok = (from c in db.Cards
                    where c.DeckId == deckId && c.Status == CardStatus.NOK
                    select c)
                      .OrderBy(p => Guid.NewGuid()) // order randomly
                      .Take(take_nok);

            //union and mix it up
            var cards = nok.Union(soso).Union(ok).OrderBy(p => Guid.NewGuid());

            return new Test()
            {
                Date = DateTime.Now,
                Cards = cards.Select(c => new TestCard() { Card = c }).ToList()
            };

        }
    }

    public class TestCard
    {
        public int Id { get; set; }
        [XmlIgnore]
        public int TestId { get;set; }
        public Card Card { get; set; }
        bool? Ok { get; set; }
    }
}