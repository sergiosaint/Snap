using System;
using System.Collections.Generic;
using System.Linq;
using Snap.Entities;

namespace Snap {

  public class Simulator {
    private int NumOfPlayers { get; }
    private int allCards;

    public Stack<Card> Pile { get; }
    public List<List<Card>> PlayersCards { get; }
    public CardDeck CardDeck { get; }

    public Simulator( int numOfPlayers ) {
      NumOfPlayers = numOfPlayers;
      Pile = new Stack<Card>();
      PlayersCards = new List<List<Card>>();
      for ( int i = 0; i < NumOfPlayers; i++ ) {
        PlayersCards.Add( new List<Card>() );
      }

      CardDeck = new CardDeck();
      CardDeck.Shuffle();

      allCards = Utils.Utils.GetNumOfAllCards( NumOfPlayers );
    }

    public void Deal() {
      while ( CardDeck.Cards.Count >= NumOfPlayers ) {
        for ( int i = 0; i < NumOfPlayers; i++ ) {
          PlayersCards.ElementAt( i ).Add( CardDeck.TakeFromBottom() );
        }
      }
    }

    private int FindWinner() {
      for ( int i = 0; i < NumOfPlayers; i++ ) {
        if ( PlayersCards.ElementAt( i ).Count == allCards ) {
          return i;
        }
      }

      return -1;
    }

    public void Simulate() {
      while ( FindWinner() == -1 ) {
        Card previousCard = null;
        int previousPlayer = -1;

        for ( int i = 0; i < NumOfPlayers; i++ ) {
          var playerCards = PlayersCards.ElementAt( i );
          if ( playerCards.Any() ) {
            var currentCard = playerCards.Last();
            playerCards.Remove( currentCard );

            Pile.Push( currentCard );
            Console.WriteLine( $"Player {i + 1} added card {currentCard} to the pile." );
            if ( previousCard != null && previousCard.Rank == currentCard.Rank ) {
              Snap( previousPlayer, i );
              if ( FindWinner() != -1 ) {
                break;
              }
            }

            previousPlayer = i;
            previousCard = currentCard;
          }
        }

        if ( PlayersCards.All( stack => !stack.Any() ) ) {
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
      List<Card> winningStack;
      Random rng = new Random();
      var winner = rng.Next( 0, 1 );//Good candidate to be injected in order to be testable
      if ( winner == 0 ) {
        Console.WriteLine( $"Player {player1Index + 1} Snaps the cards!" );
        winningStack = PlayersCards.ElementAt( player1Index );
      }
      else {
        Console.WriteLine( $"Player {player2Index + 1} Snaps the cards!" );
        winningStack = PlayersCards.ElementAt( player2Index );
      }

      while ( Pile.Any() ) {
        winningStack.Insert( 0, Pile.Pop() );
      }
    }
  }
}
