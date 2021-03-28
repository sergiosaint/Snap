using System;
using Snap;
using Snap.Components.SnapDeciders;
using Snap.Components.WinnerDeciders;
using Xunit;

namespace SnapTests
{
  public class SimulatorTests
  {
    [Theory]
    [InlineData( 2, 26 )]
    [InlineData( 3, 17 )]
    [InlineData( 4, 13 )]
    public void SimulatorDealsTheCorrectNumOfCardsToEachPlayer( int numOfPlayers, int expectedNumOfCardsPerPlayer )
    {

      var simulator = new Simulator( numOfPlayers, new EqualChanceToEveryone(), new AnyCard() );
      simulator.Deal();

      foreach ( var playerCards in simulator.PlayerCardsByPlayerIndex.Values )
      {
        Assert.Equal( expectedNumOfCardsPerPlayer, playerCards.Count() );
      }
    }

    [Theory]
    [InlineData( -5, true )]
    [InlineData( 0, true )]
    [InlineData( 1, true )]
    [InlineData( 2, false )]
    [InlineData( 5, false )]
    [InlineData( 52, false )]
    [InlineData( 53, true )]
    public void When_GivenAnIncorrectNumberOfPlayers_ThrowArgumentOutOfRangeException( int numberOfPlayers, bool exceptionExpected )
    {

      if ( exceptionExpected )
      {
        var exception = Assert.Throws<ArgumentOutOfRangeException>( () => new Simulator( numberOfPlayers, new EqualChanceToEveryone(), new AnyCard() ) );
        Assert.Equal(
          $"The simulator expects a number of players between 2 and 52, {numberOfPlayers} was given instead. (Parameter 'numberOfPlayers')",
          exception.Message );
      }
      else
      {
        new Simulator( numberOfPlayers, new EqualChanceToEveryone(), new AnyCard() ); // implicit assert, if it throws an exception the test fails.
      }
    }
  }
}
