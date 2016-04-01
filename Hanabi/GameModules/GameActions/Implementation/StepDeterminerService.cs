using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation.Implementation;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.GameActions.Implementation
{
    public class StepDeterminerService
    {
        private readonly IList<GameAction> _gameActions;
        private List<IGameValidator> _gameValidators => new List<IGameValidator>() {new GameDefaultValidator()}; 
        private readonly Game _game;
        public StepDeterminerService(Game game, IList<GameAction> gameActions)
        {
            _gameActions = gameActions;
            _game = game;
        }

        public GameState ProcessCommand(string command)
        {
            var gameAction = _gameActions.First(x => command.StartsWith(x.NativeCommandStart));
            gameAction.SetParametersFromInitString(command);
            var commandResult = gameAction.ProcessCommand(_game);
            bool constraintViolation = false;
            if (gameAction.RequireGameCheck)
            {
                if (!_gameValidators.TrueForAll(x => x.IsValid(_game, gameAction.CommandParameters)))
                {
                    constraintViolation = true;
                }
            }
            gameAction.FinalizeCommand(_game, gameAction.CommandParameters, commandResult, constraintViolation);

            return commandResult == GameSettings.CommandResult.Failed || constraintViolation  ? GameState.Finished : GameState.Playing;

        }
    }
}