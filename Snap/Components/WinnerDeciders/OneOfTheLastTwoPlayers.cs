using System;

namespace Snap.Components.WinnerDeciders
{
  public class OneOfTheLastTwoPlayers : IWinnerDecider
  {
    public int GetWinnerIndex( int currentPlayerIndex, int numberOfPlayers )
    {
      var previousPlayerIndex = currentPlayerIndex == 0 ? numberOfPlayers - 1 : currentPlayerIndex - 1;

      Random rng = new Random();
      return rng.Next( 0, 2 ) == 0 ? currentPlayerIndex : previousPlayerIndex;
    }
  }
}
