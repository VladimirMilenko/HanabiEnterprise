using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;

namespace Hanabi.GameModules.Validation.Interfaces
{
    public interface IGameValidator
    {
        bool IsValid(Game game, CommandParams commandParams);
    }
}