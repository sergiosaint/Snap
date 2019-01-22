using System;
using System.Collections.Generic;
using System.Linq;

namespace Snap.Entities {

  public class CardDeck {
    public List<Card> Cards { get; }

    public CardDeck() {
      Cards = new List<Card>();

      foreach ( Suit suit in Enum.GetValues( typeof( Suit ) ) ) {
        foreach ( Rank rank in Enum.GetValues( typeof( Rank ) ) ) {
          Cards.Add( new Card( suit, rank ) );
        }
      }
    }

    public void Shuffle() { //Fisher-Yates shuffle, seen in stack overflow
      Random rng = new Random();
      int n = Cards.Count;
      while ( n > 1 ) {
        n--;
        int k = rng.Next( n + 1 );
        var value = Cards[k];
        Cards[k] = Cards[n];
        Cards[n] = value;
      }
    }

    public Card TakeFromBottom() {
      var card = Cards.ElementAt( 0 );

      if ( card != null ) {
        Cards.RemoveAt( 0 );
      }

      return card;
    }
  }

}
