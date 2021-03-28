namespace Snap.Components.WinnerDeciders
{
  public interface IWinnerDecider
  {
    int GetWinnerIndex( int currentPlayerIndex, int numberOfPlayers );
  }
}
