using Snap.Entities;
using System.Linq;

namespace Snap.Components.SnapDeciders
{
  public class OnlyConsecutiveCards : ISnapDecider
  {
    public bool IsSnap(CardSet centralPile)
    {
      if (centralPile.Count() < 2)
      {
        return false;
      }

      return centralPile.Cards.ElementAt(centralPile.Cards.Count - 1).Rank == centralPile.Cards.ElementAt(centralPile.Cards.Count - 2).Rank;
    }
  }
}
