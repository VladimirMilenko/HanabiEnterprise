using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.Validation.Implementation
{
    public class StartGameValidator :IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            return commandParams.Cards.Count >= GameSettings.MinCards;
        }
    }
}