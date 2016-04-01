using System;
using System.Collections.Generic;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation;
using Hanabi.GameModules.Validation.Implementation;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.GameActions.Implementation
{
    public class DropCardAction:GameAction
    {
        public override List<IGameValidator> CommandValidators => new List<IGameValidator> {new DropCardValidator()};
        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Drop card";
        public override bool RequireGameCheck => true;
        public override string NativeCommandParams { get; set; }
        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Substring(NativeCommandStart.Length + 1);
            CommandParameters = new CommandParams() {Indexes =  new List<int>()};
            CommandParameters.Indexes.Add(int.Parse(NativeCommandParams));
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
                throw new ArgumentException($"Unable to throw missing card, index: ${CommandParameters.FirstIndex}");

            return GameSettings.CommandResult.Success;
        }

        public override void FinalizeCommand(Game game, CommandParams commandParams, GameSettings.CommandResult commandResult, bool constraintViolation)
        {
            game.Statistics.Rounds++;
            game.PickNewCardFromDeck(commandParams.FirstIndex);
            game.SwapPlayers();
        }
    }
}