using System;
using System.Collections.Generic;
using System.Linq;

namespace Snap.Entities
{
  public class CardSet
  {
    public List<Card> Cards { get; }

    public CardSet()
    {
      Cards = new List<Card>();
    }

    public void Shuffle()
    { //Fisher-Yates shuffle, seen in stack overflow
      Random rng = new Random();
      int n = Cards.Count;
      while ( n > 1 )
      {
        n--;
        int k = rng.Next( n + 1 );
        var value = Cards[k];
        Cards[k] = Cards[n];
        Cards[n] = value;
      }
    }

    public void InitializeWithFullDeck()
    {
      Cards.Clear();

      foreach ( Suit suit in Enum.GetValues( typeof( Suit ) ) )
      {
        foreach ( Rank rank in Enum.GetValues( typeof( Rank ) ) )
        {
          Cards.Add( new Card( suit, rank ) );
        }
      }
    }

    public Card TakeFromBottom()
    {

      if ( Cards.Any() )
      {
        var card = Cards.ElementAt( 0 );
        Cards.RemoveAt( 0 );
        return card;
      }

      return null;
    }

    public Card TakeFromTop()
    {

      if ( Cards.Any() )
      {
        var lastIndex = Cards.Count - 1;
        var card = Cards.ElementAt( lastIndex );
        Cards.RemoveAt( lastIndex );
        return card;
      }

      return null;
    }

    public void AddToTop( Card card )
    {
      Cards.Add( card );
    }

    public void AddToBottom( Card card )
    {
      Cards.Insert( 0, card );
    }

    public bool Any()
    {
      return Cards.Any();
    }

    public int Count()
    {
      return Cards.Count;
    }
  }
}
