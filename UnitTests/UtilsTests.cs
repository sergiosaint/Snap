using NUnit.Framework;
using Snap.Utils;

namespace UnitTests {
  [TestFixture]
  public class UtilsTests {
    [Test]
    [TestCase( 2,52 )]
    [TestCase( 3, 51 )]
    public void ExpectedCardsInGameCalculatedCorrectly(int numOfPlayers, int expectedResult)
    {
      var result = Utils.GetNumOfAllCards(numOfPlayers);
      Assert.That( result, Is.EqualTo( expectedResult ) );
    }
  }
}
