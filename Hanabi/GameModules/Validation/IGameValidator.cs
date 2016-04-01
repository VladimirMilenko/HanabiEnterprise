using Hanabi.GameModules.GameActions;

namespace Hanabi.GameModules.Validation
{
    public interface IGameValidator
    {
        bool IsValid(Game game, CommandParams commandParams);
    }
}