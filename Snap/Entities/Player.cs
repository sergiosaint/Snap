namespace Snap.Entities
{
  public class Player
  {
    private static int _playerCounter = 0;
    public CardSet Cards { get; }
    public int Id { get; }

    public Player()
    {
      Cards = new CardSet();
      Id = ++_playerCounter;
    }

    public bool HasCards()
    {
      return Cards.Any();
    }

  }
}
