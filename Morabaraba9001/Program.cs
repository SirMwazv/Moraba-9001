using System;
using Morabaraba9001.Data;
using Morabaraba9001.Display;

namespace Morabaraba9001
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameManager();    //initialize a new game instance
            game.init();
            game.RunGame();
        }
    }
}

