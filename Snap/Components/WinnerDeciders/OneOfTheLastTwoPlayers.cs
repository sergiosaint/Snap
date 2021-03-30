using System;
using System.Collections.Generic;
using Snap.Entities;

namespace Snap.Components.WinnerDeciders
{
  public class OneOfTheLastTwoPlayers : IWinnerDecider
  {
    private readonly Random _random = new Random();

    public Player GetSnapWinner( Player currentPlayer, Dictionary<int, Player> playersById )
    {
      var numberOfPlayers = playersById.Count;
      var previousPlayerId = currentPlayer.Id == 1 ? numberOfPlayers : currentPlayer.Id - 1;
      var winnerId = _random.Next( 0, 2 ) == 0 ? currentPlayer.Id : previousPlayerId;
      return playersById[winnerId];
    }
  }
}
