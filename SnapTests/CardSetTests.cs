using System.Linq;
using Snap.Entities;
using Xunit;

namespace SnapTests
{
  public class CardSetTests
  {
    [Fact]
    public void When_Created_TheCardSetIsEmpty()
    {
      var cardDeck = new CardSet();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.Empty( cards );
    }

    [Fact]
    public void When_InitializeWithFullDeck_TheCardDeckHas52UniqueCards()
    {
      var cardDeck = new CardSet();
      cardDeck.InitializeWithFullDeck();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.Equal( 52, cards.Count() );
    }

    [Fact]
    public void When_Shuffled_KeepsTheUniqueCardCount()
    {
      var cardDeck = new CardSet();
      cardDeck.InitializeWithFullDeck();
      cardDeck.Shuffle();
      var cards = cardDeck.Cards.Select( c => c.ToString() ).Distinct();
      Assert.Equal( 52, cards.Count() );
    }
  }
}
