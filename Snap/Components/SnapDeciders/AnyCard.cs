using Snap.Entities;
using System.Linq;

namespace Snap.Components.SnapDeciders
{
  public class AnyCard : ISnapDecider
  {
    public bool IsSnap(CardSet centralPile)
    {
      if (centralPile.Count() < 2)
      {
        return false;
      }

      var currentCardRank = centralPile.Cards.ElementAt(centralPile.Cards.Count - 1).Rank;
      return centralPile.Cards.Count(c => c.Rank == currentCardRank) > 1;
    }
  }
}
