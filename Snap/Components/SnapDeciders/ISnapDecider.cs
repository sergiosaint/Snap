using Snap.Entities;

namespace Snap.Components.SnapDeciders
{
  public interface ISnapDecider
  {
    bool IsSnap(CardSet centralPile);
  }
}
