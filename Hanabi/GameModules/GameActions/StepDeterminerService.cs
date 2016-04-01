using System.Collections.Generic;
using System.Linq;

namespace Hanabi.GameModules.GameActions
{
    public class StepDeterminerService
    {
        private readonly IList<GameAction> _gameActions;
        private readonly Game _game;
        public StepDeterminerService(Game game, IList<GameAction> gameActions)
        {
            _gameActions = gameActions;
            _game = game;
        }

        public void ProcessCommand(string command)
        {
            var gameAction = _gameActions.First(x => command.StartsWith(x.NativeCommandStart));
            gameAction.SetParametersFromInitString(command);
            gameAction.ProcessCommand(_game);
        }
    }
}