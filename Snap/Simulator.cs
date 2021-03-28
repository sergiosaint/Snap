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
    public Dictionary<int, CardSet> PlayerCardsByPlayerIndex { get; }
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
      PlayerCardsByPlayerIndex = new Dictionary<int, CardSet>();
      for ( int playerIndex = 0; playerIndex < _numberOfPlayers; playerIndex++ )
      {
        PlayerCardsByPlayerIndex.Add( playerIndex, new CardSet() );
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
        foreach ( var playerCards in PlayerCardsByPlayerIndex.Values )
        {
          playerCards.AddToTop( CardDeck.TakeFromBottom() );
        }
      }
    }

    private bool MoreThanOnePlayerHasCards()
    {
      return PlayerCardsByPlayerIndex.Values.Count( c => c.Cards.Any() ) > 1;
    }

    private int FindWinnerIndex()
    {
      var winnerKvp = PlayerCardsByPlayerIndex.FirstOrDefault( kvp => kvp.Value.Cards.Count == _numberOfCardsUsedInGame );

      if ( winnerKvp.Equals( default( KeyValuePair<int, CardSet> ) ) )
      {
        return -1;
      }

      return winnerKvp.Key;
    }

    public void Simulate()
    {
      int currentPlayerIndex = 0;
      while ( MoreThanOnePlayerHasCards() )
      {
        var currentPlayerCards = PlayerCardsByPlayerIndex[currentPlayerIndex];
        if ( currentPlayerCards.Any() )
        {
          var currentCard = currentPlayerCards.TakeFromTop();
          CentralPile.AddToTop( currentCard );
          Console.WriteLine( $"Player {currentPlayerIndex + 1} added card {currentCard} to the pile." );

          if ( _snapDecider.IsSnap( CentralPile ) )
          {
            var winnerIndex = _winnerDecider.GetWinnerIndex( currentPlayerIndex, _numberOfPlayers );
            Snap( winnerIndex );

            if ( FindWinnerIndex() != -1 )
            {
              break;
            }
          }
        }

        currentPlayerIndex++;
        if ( currentPlayerIndex >= _numberOfPlayers )
        {
          currentPlayerIndex = 0;
        }

        if ( PlayerCardsByPlayerIndex.Values.All( stack => !stack.Any() ) )
        {
          Console.WriteLine( "No more Cards Available" );
          break;
        }
      }

      if ( FindWinnerIndex() != -1 )
      {
        Console.WriteLine( $"Player{FindWinnerIndex() + 1} Won!" );
      }
    }

    private void Snap( int winnerIndex )
    {
      Console.WriteLine( $"Player {winnerIndex + 1} Snaps the cards!" );
      CardSet winningStack = PlayerCardsByPlayerIndex[winnerIndex];

      while ( CentralPile.Any() )
      {
        winningStack.AddToBottom( CentralPile.TakeFromTop() );
      }
    }
  }
}
