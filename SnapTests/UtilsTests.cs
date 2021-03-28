using Snap.Utils;
using Xunit;

namespace SnapTests
{
  public class UtilsTests
  {
    [Theory]
    [InlineData( 2, 52 )]
    [InlineData( 3, 51 )]
    public void ExpectedCardsInGameCalculatedCorrectly( int numOfPlayers, int expectedResult )
    {
      var result = Utils.GetNumberOfCardsToUseInGame( numOfPlayers );
      Assert.Equal( expectedResult, result );
    }
  }
}
