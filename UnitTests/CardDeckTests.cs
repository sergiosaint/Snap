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
      var cardDeck = new CardDeck();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.That(cards.Count(), Is.EqualTo(52) );
    }

    [Test]
    public void WhenShuffledTheCardDeckHas52UniqueCards() {
      var cardDeck = new CardDeck();
      cardDeck.Shuffle();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.That( cards.Count(), Is.EqualTo( 52 ) );
    }
  }
}
