﻿using Snap.Utils;

namespace Snap.Entities {

  public class Card {
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card( Suit suit, Rank rank ) {
      Suit = suit;
      Rank = rank;
    }

    public override string ToString() {
      return $"{Rank.GetEnumDescription()}{Suit.GetEnumDescription()}";
    }
  }
}
