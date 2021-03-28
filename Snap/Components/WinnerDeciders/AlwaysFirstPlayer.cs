namespace Snap.Components.WinnerDeciders
{
  public class AlwaysFirstPlayer : IWinnerDecider
  {
    public int GetWinnerIndex( int currentPlayerIndex, int numberOfPlayers )
    {
      return 0;
    }
  }
}
