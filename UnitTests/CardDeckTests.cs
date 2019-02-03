using System.Linq;
using NUnit.Framework;
using Snap;
using Snap.Entities;

namespace UnitTests {
  [TestFixture]
  public class CardDeckTests {
    [Test]
    public void WhenCreatedTheCardDeckHas52UniqueCards()
    {
      var cardDeck = new CardSet();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.That(cards.Count(), Is.EqualTo(52) );
    }

    [Test]
    public void WhenShuffledTheCardDeckHas52UniqueCards() {
      var cardDeck = new CardSet();
      cardDeck.Shuffle();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.That( cards.Count(), Is.EqualTo( 52 ) );
    }

    [Test]
    public void When() {
      var cardDeck = new CardSet();
      for(var i =0; i<55; i++ )
      {
        cardDeck.TakeFromBottom();
      }
    }
  }
}
