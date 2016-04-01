using Hanabi.GameModules.GameActions;

namespace Hanabi.GameModules.Validation
{
    public class StartGameValidator :IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            return commandParams.Cards.Count >= GameSettings.MinCards;
        }
    }
}