using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Memory

{
    class Program
    {
        static void Main(string[] args)
        {
            var valuesInputcontrol = new ValuesInputControl();
            var gameResults = new GameResults();
            var coordinates = new Coordinates();
            var memoryGame = new MemoryGame(gameResults, coordinates, valuesInputcontrol);
            memoryGame.StartGame();
        }
    }
}
