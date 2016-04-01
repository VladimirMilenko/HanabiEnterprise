using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.Validation.Implementation
{
    public class PlayCardValidator:IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            var card = game.CurrentPlayer.Hand[commandParams.FirstIndex];
            return game.Desk[card.CardColor] == card.Value - GameSettings.DeskCardDistance;
        }
    }
}