using System.Collections.Generic;
using Snap.Entities;

namespace Snap.Components.WinnerDeciders
{
  public interface IWinnerDecider
  {
    Player GetSnapWinner( Player currentPlayer, Dictionary<int, Player> playersById );
  }
}
