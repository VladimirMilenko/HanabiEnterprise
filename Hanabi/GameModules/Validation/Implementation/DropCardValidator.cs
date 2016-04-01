using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.Validation.Implementation
{
    public class DropCardValidator:IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            return game.CurrentPlayer.Hand.Count > commandParams.FirstIndex;
        }
    }
}