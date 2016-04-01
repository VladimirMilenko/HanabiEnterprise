using System;
using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation;

namespace Hanabi.GameModules.GameActions
{
    public class DropCardAction:GameAction
    {
        public override List<IGameValidator> CommandValidators => new List<IGameValidator> {new DropCardValidator()};
        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Drop card";
        public override string NativeCommandParams { get; set; }
        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Skip(NativeCommandStart.Length + 1).ToString();
            CommandParameters = new CommandParams();
            CommandParameters.Indexes.Add(int.Parse(NativeCommandParams));
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
                throw new ArgumentException($"Unable to throw missing card, index: ${CommandParameters.FirstIndex}");
            game.CurrentPlayer.Hand.RemoveAt(CommandParameters.FirstIndex);
            game.CurrentPlayer.Hand.Add(game.Deck.Dequeue());
            return GameSettings.CommandResult.Ok;
        }
    }
}