using System;
using System.Collections.Generic;
using Snap.Entities;

namespace Snap.Components.WinnerDeciders
{
  public class EqualChanceToEveryone : IWinnerDecider
  {
    private readonly Random _random = new Random();

    public Player GetSnapWinner( Player currentPlayer, Dictionary<int, Player> playersById )
    {
      var numberOfPlayers = playersById.Count;
      var winnerId = _random.Next( 1, numberOfPlayers + 1);
      return playersById[winnerId];
    }
  }
}
