using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation;

namespace Hanabi.GameModules.GameActions
{
    public abstract class GameAction
    {
        public abstract List<IGameValidator> CommandValidators { get; }
        public abstract CommandParams CommandParameters { get; set; }
        public abstract string NativeCommandStart { get; }
        public abstract string NativeCommandParams { get; set; }
        public abstract void SetParametersFromInitString(string initString);
        public abstract GameSettings.CommandResult ProcessCommand(Game game);
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