using Hanabi.GameModules.GameActions;

namespace Hanabi.GameModules.Validation
{
    public class DropCardValidator:IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            return game.CurrentPlayer.Hand.Count > commandParams.FirstIndex;
        }
    }
}