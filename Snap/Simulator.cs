using System;
using System.Collections.Generic;
using System.Linq;
using Snap.Entities;

namespace Snap {

  public class Simulator {
    private int NumOfPlayers { get; }
    private int allCards;

    public Stack<Card> Pile { get; }
    public List<CardSet> PlayerCards { get; }
    public CardSet CardDeck { get; }

    public Simulator( int numOfPlayers ) {
      NumOfPlayers = numOfPlayers;
      Pile = new Stack<Card>();
      PlayerCards = new List<CardSet>();
      for ( int i = 0; i < NumOfPlayers; i++ ) {
        PlayerCards.Add( new CardSet() );
      }

      CardDeck = new CardSet();
      CardDeck.InitializeWithFullDeck();
      CardDeck.Shuffle();

      allCards = Utils.Utils.GetNumOfAllCards( NumOfPlayers );
    }

    public void Deal() {
      while ( CardDeck.Cards.Count >= NumOfPlayers ) {
        for ( int i = 0; i < NumOfPlayers; i++ ) {
          PlayerCards.ElementAt( i ).AddToTop( CardDeck.TakeFromBottom() );
        }
      }
    }

    private bool MoreThanOnePlayerHasCards() {
      int PlayersWithCards = 0;
      for ( int i = 0; i < NumOfPlayers; i++ ) {
        if ( PlayerCards.ElementAt( i ).Any() ) {
          PlayersWithCards++;
        }
      }

      return PlayersWithCards > 1;
    }

    private int FindWinner() {
      for ( int i = 0; i < NumOfPlayers; i++ ) {
        if ( PlayerCards.ElementAt( i ).Count() == allCards ) {
          return i;
        }
      }

      return -1;
    }

    public void Simulate() {
      Card previousCard = null;
      int previousPlayer = -1;
      int currentPlayer = 0;
      while ( MoreThanOnePlayerHasCards() ) {

          var playerCards = PlayerCards.ElementAt( currentPlayer );
          if ( playerCards.Any() ) {
            var currentCard = playerCards.TakeFromTop();
            Pile.Push( currentCard );
            Console.WriteLine( $"Player {currentPlayer + 1} added card {currentCard} to the pile." );
            if ( previousCard != null && previousCard.Rank == currentCard.Rank ) {
              Snap( previousPlayer, currentPlayer );
              if ( FindWinner() != -1 ) {
                break;
              }
            }

            previousPlayer = currentPlayer;
            previousCard = currentCard;
          }

          currentPlayer++;
          if (currentPlayer >= NumOfPlayers)
          {
            currentPlayer = 0;
          }

        if ( PlayerCards.All( stack => !stack.Any() ) ) {
          Console.WriteLine( $"No more Cards Available" );
          break;
        }
      }

      if ( FindWinner() != -1 ) {
        Console.WriteLine( $"Player{FindWinner() + 1} Won!" );
      }
    }

    private bool IsSnap( Card currentCard ) {
      return Pile.Contains( currentCard );
    }

    public void Snap( int player1Index, int player2Index ) {
      CardSet winningStack;
      Random rng = new Random();
      var winner = rng.Next( 0, 1 );//Good candidate to be injected in order to be testable
      if ( winner == 0 ) {
        Console.WriteLine( $"Player {player1Index + 1} Snaps the cards!" );
        winningStack = PlayerCards.ElementAt( player1Index );
      }
      else {
        Console.WriteLine( $"Player {player2Index + 1} Snaps the cards!" );
        winningStack = PlayerCards.ElementAt( player2Index );
      }

      while ( Pile.Any() ) {
        winningStack.AddToBottom( Pile.Pop() );
      }
    }
  }
}
