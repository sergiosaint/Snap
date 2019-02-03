using NUnit.Framework;
using Snap;

namespace UnitTests {
  [TestFixture]
  public class SimulatorTests
  {

    [Test]
    [TestCase( 2, 26 )]
    [TestCase( 3, 17 )]
    [TestCase( 4, 13 )]
    public void SimulatorDealsTheCorrectNumOfCardsToEachPlayer( int numOfPlayers, int expectedNumOfCardsPerPlayer) {

      var simulator = new Simulator( numOfPlayers );
      simulator.Deal();

      foreach (var playerCards in simulator.PlayerCards )
      {
        Assert.That( playerCards.Count, Is.EqualTo( expectedNumOfCardsPerPlayer ) );
      }
    }
  }
}
