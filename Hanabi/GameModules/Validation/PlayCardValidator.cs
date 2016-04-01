using Hanabi.GameModules.GameActions;

namespace Hanabi.GameModules.Validation
{
    public class PlayCardValidator:IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            var card = game.CurrentPlayer.Hand[commandParams.FirstIndex];
            return game.Desk[commandParams.Color] == card.Value - GameSettings.DeskCardDistance;
        }
    }
}