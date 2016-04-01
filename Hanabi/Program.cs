using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanabi.GameModules;
using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.GameActions.Implementation;
using Hanabi.IO;
using Hanabi.IO.ConsoleIO;

namespace Hanabi
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(new ConsoleIOService(), new List<GameAction>
            {
                new StartNewGameAction(),
                new PlayCardAction(),
                new DropCardAction(),
                new TellColorAction(),
                new TellValueAction()
            } );
            game.Run();
        }
    }
}
