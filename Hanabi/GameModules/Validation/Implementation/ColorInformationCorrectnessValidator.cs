using System.Linq;
using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.Validation.Implementation
{
    public class ColorInformationCorrectnessValidator : IGameValidator
    {
        public bool IsValid(Game game, CommandParams commandParams)
        {
            bool correctColor = commandParams.Indexes.TrueForAll(index => game.NextPlayer.Hand[index].CardColor == commandParams.Color);
            bool fullIndexes = Enumerable.Range(0, game.NextPlayer.Hand.Count)
                    .Where(index => game.NextPlayer.Hand[index].CardColor == commandParams.Color)
                    .SequenceEqual(commandParams.Indexes);
            return correctColor && fullIndexes;

        }
    }
}
