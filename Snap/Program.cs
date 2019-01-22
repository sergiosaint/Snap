using System;

namespace Snap {
  class Program {
    static void Main( string[] args ) {
      Console.WriteLine( "Insert number of players" );
      int numOfPlayers;
      Int32.TryParse( Console.ReadLine(), out numOfPlayers );
      if (numOfPlayers < 2)
      {
        numOfPlayers = 2;
      }

      var simulator = new Simulator( numOfPlayers );
      simulator.Deal();
      simulator.Simulate();

      Console.WriteLine( "Press any key to exit" );
      Console.ReadKey();
    }
  }
}
