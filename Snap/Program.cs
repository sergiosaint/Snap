using System;
using Snap.Components.SnapDeciders;
using Snap.Components.WinnerDeciders;

namespace Snap
{
  class Program
  {
    static void Main( string[] args )
    {
      Console.WriteLine( "Insert number of players (2-52)" );
      int numOfPlayers;
      Int32.TryParse( Console.ReadLine(), out numOfPlayers );
      if ( numOfPlayers < 2 || numOfPlayers > 52 )
      {
        Console.WriteLine( "An invalid number of players has been added, the default number of 2 players will be used." );
        numOfPlayers = 2;
      }

      var simulator = new Simulator( numOfPlayers, new EqualChanceToEveryone(), new AnyCard() );
      simulator.Deal();
      simulator.Simulate();

      Console.WriteLine( "Press any key to exit" );
      Console.ReadKey();
    }
  }
}
