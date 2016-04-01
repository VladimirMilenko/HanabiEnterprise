using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.GameActions.Abstract
{
    public abstract class GameAction
    {
        public abstract List<IGameValidator> CommandValidators { get; }
        public abstract CommandParams CommandParameters { get; set; }
        public abstract string NativeCommandStart { get; }
        public abstract bool RequireGameCheck { get; }
        public abstract string NativeCommandParams { get; set; }
        public abstract void SetParametersFromInitString(string initString);
        public abstract GameSettings.CommandResult ProcessCommand(Game game);
        public abstract void FinalizeCommand(Game game, CommandParams commandParams, GameSettings.CommandResult commandResult, bool constraintViolation=false);
    }

    public class CommandParams
    {
        public GameSettings.GameColors Color;
        public List<int> Indexes;
        public List<Card> Cards;
        public int Value;
        public int FirstIndex => Indexes.FirstOrDefault();
    }
}