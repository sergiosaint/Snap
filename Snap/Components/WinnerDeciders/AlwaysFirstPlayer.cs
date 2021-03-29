using System.Collections.Generic;
using Snap.Entities;

namespace Snap.Components.WinnerDeciders
{
  public class AlwaysFirstPlayer : IWinnerDecider
  {
    public Player GetSnapWinner( Player currentPlayer, Dictionary<int, Player> playersById )
    {
      return playersById[1];
    }
  }
}
