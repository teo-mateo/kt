using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using kt.api.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace kt.api.tests
{
    public static class TestUtility
    {
        public static Card AddCard(this Deck deck, CardStatus status)
        {
            Card c = new Card()
            {
                Id = Guid.NewGuid().ToString(), 
                Status = status,
                Front = new CardSide() {   Content = "front", Id = ++id },
                Back = new CardSide() { Content = "back", Id = ++id }, 
                DeckId = deck.Id
            };
            deck.Cards.Add(c);
            return c;
        }

        public static int id = 0;
    }

    public static class MockExtensions
    {
        /// <summary>
        /// set up an IQueryable mocked object with the given IQueryable
        /// </summary>
        public static void SetupIQueryable<T>(this Mock<T> mock, IQueryable iqueryable) where T:class, IQueryable
        {
            mock.Setup(r => r.GetEnumerator()).Returns(iqueryable.GetEnumerator());
            mock.Setup(r => r.Provider).Returns(iqueryable.Provider);
            mock.Setup(r => r.Expression).Returns(iqueryable.Expression);
            mock.Setup(r => r.ElementType).Returns(iqueryable.ElementType);
        }
    }

    [TestClass]
    public class UnitTest1
    {
        Mock<KTContext> _mockDb;
        List<Deck> _decks = new List<Deck>();
        List<Card> _cards = new List<Card>();
        List<Test> _tests = new List<Test>();

        [TestInitialize]
        public void Initialize()
        {
            Deck d1 = new Deck(){ Id = 1, CreationDate = DateTime.Now, Cards = new List<Card>()};
            for (int i = 0; i < 20; i++)
            {
                d1.AddCard(CardStatus.NOK);
                d1.AddCard(CardStatus.OK);
                d1.AddCard(CardStatus.SOSO);
            }


            _decks.Add(d1);
            _cards.AddRange(d1.Cards);

            var set_decks = new Mock<DbSet<Deck>>();
            set_decks.SetupIQueryable(_decks.AsQueryable());


            var set_cards = new Mock<DbSet<Card>>();
            set_cards.SetupIQueryable(_cards.AsQueryable());

            var set_tests = new Mock<DbSet<Test>>();
            set_tests.SetupIQueryable(_tests.AsQueryable());

            _mockDb = new Mock<KTContext>();
            _mockDb.Setup(m => m.Decks).Returns(set_decks.Object);
            _mockDb.Setup(m => m.Cards).Returns(set_cards.Object);
            _mockDb.Setup(m => m.Tests).Returns(set_tests.Object);

        }

        [TestMethod]
        public void NewTest_AllCards()
        {
            var newTest = Test.CreateNew(_mockDb.Object, 1);

            Assert.IsTrue(newTest.Cards.Count == 15);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.NOK).Count() == 5);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.OK).Count() == 5);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.SOSO).Count() == 5);
        }

        [TestMethod]
        public void NewTest_After1Day()
        {
            //adding a new test 1 day ago should give us 15 NOK, 0 SOSO, 0 OK
            _tests.Clear();
            _tests.Add(new Test() 
            {
                Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)), 
                Deck = _mockDb.Object.Decks.First() 
            });

            var newTest = Test.CreateNew(_mockDb.Object, 1);
            Assert.IsTrue(newTest.Cards.Count == 15);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.OK).Count() == 0);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.SOSO).Count() == 0);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.NOK).Count() == 15);   
        }

        [TestMethod]
        public void NewTest_After3Days()
        {
            //adding a new test 3 days ago should give us 10 NOK, 5 SOSO, 0 OK
            _tests.Clear();
            _tests.Add(new Test()
            {
                Date = DateTime.Now.Subtract(TimeSpan.FromDays(3)),
                Deck = _mockDb.Object.Decks.First()
            });

            var newTest = Test.CreateNew(_mockDb.Object, 1);
            Assert.IsTrue(newTest.Cards.Count == 15);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.OK).Count() == 0);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.SOSO).Count() == 5);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.NOK).Count() == 10);
        }

        [TestMethod]
        public void NewTest_After7Days()
        {
            //adding a new test 7 days ago should give us 5 NOK, 5 SOSO, 5 OK
            _tests.Clear();
            _tests.Add(new Test()
            {
                Date = DateTime.Now.Subtract(TimeSpan.FromDays(7)),
                Deck = _mockDb.Object.Decks.First()
            });

            var newTest = Test.CreateNew(_mockDb.Object, 1);
            Assert.IsTrue(newTest.Cards.Count == 15);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.OK).Count() == 5);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.SOSO).Count() == 5);
            Assert.IsTrue(newTest.Cards.Where(p => p.Card.Status == CardStatus.NOK).Count() == 5);
        }
    }
}
