using System;
using System.Collections.Generic;
using System.Linq;
using Snap.Components.SnapDeciders;
using Snap.Components.WinnerDeciders;
using Snap.Entities;

namespace Snap
{
  public class Simulator
  {
    private readonly int _numberOfPlayers;
    private readonly int _numberOfCardsUsedInGame;
    private readonly IWinnerDecider _winnerDecider;
    private readonly ISnapDecider _snapDecider;

    public CardSet CentralPile { get; }
    public Dictionary<int, Player> PlayersById { get; }
    public ICollection<Player> Players => PlayersById.Values;
    public CardSet CardDeck { get; } // To proper test this should be injected too

    public Simulator( int numberOfPlayers, IWinnerDecider winnerDecider, ISnapDecider snapDecider )
    {

      if ( numberOfPlayers < 2 || numberOfPlayers > 52 )
      {
        throw new ArgumentOutOfRangeException( nameof( numberOfPlayers ), $"The simulator expects a number of players between 2 and 52, {numberOfPlayers} was given instead." );
      }

      _numberOfPlayers = numberOfPlayers;
      _winnerDecider = winnerDecider;
      _snapDecider = snapDecider;

      CentralPile = new CardSet();
      PlayersById = new Dictionary<int, Player>();
      for ( int playerIndex = 0; playerIndex < _numberOfPlayers; playerIndex++ )
      {
        var player = new Player();
        PlayersById.Add( player.Id, player );
      }

      CardDeck = new CardSet();
      CardDeck.InitializeWithFullDeck();
      CardDeck.Shuffle();

      _numberOfCardsUsedInGame = Utils.Utils.GetNumberOfCardsToUseInGame( _numberOfPlayers );
    }

    public void Deal()
    {
      while ( CardDeck.Cards.Count >= _numberOfPlayers )
      {
        foreach ( var player in Players )
        {
          player.Cards.AddToTop( CardDeck.TakeFromBottom() );
        }
      }
    }

    public void Simulate()
    {
      while ( true )
      {
        foreach ( var currentPlayer in Players )
        {
          if ( currentPlayer.HasCards() )
          {
            var currentCard = currentPlayer.Cards.TakeFromTop();
            CentralPile.AddToTop( currentCard );
            Console.WriteLine( $"Player {currentPlayer.Id} added card {currentCard} to the pile." );

            if ( _snapDecider.IsSnap( CentralPile ) )
            {
              var winner = _winnerDecider.GetSnapWinner( currentPlayer, PlayersById );
              Snap( winner );

              if ( winner.Cards.Count() == _numberOfCardsUsedInGame )
              {
                Console.WriteLine( $"Player {winner.Id} Won!" );
                return;
              }
            }
          }
        }

        if ( Players.All( p => !p.HasCards() ) )
        {
          Console.WriteLine( "No more Cards Available" );
          return;
        }
      }
    }


    private void Snap( Player winnerPlayer )
    {
      Console.WriteLine( $"Player {winnerPlayer.Id} Snaps the cards!" );

      while ( CentralPile.Any() )
      {
        winnerPlayer.Cards.AddToBottom( CentralPile.TakeFromTop() );
      }
    }
  }
}
