using System;

namespace Snap.Components.WinnerDeciders
{
  public class EqualChanceToEveryone : IWinnerDecider
  {
    public int GetWinnerIndex( int currentPlayerIndex, int numberOfPlayers )
    {
      Random rng = new Random();
      return rng.Next( 0, numberOfPlayers );
    }
  }
}
