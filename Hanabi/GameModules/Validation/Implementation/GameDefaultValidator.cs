using System.Linq;
using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.Validation.Implementation
{
    class GameDefaultValidator:IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            if (game.Desk.All(pair => pair.Value == 5)) return false;
            return game.Deck.Count > 1;
        }
    }
}
