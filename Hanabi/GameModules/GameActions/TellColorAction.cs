using System;
using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation;

namespace Hanabi.GameModules.GameActions
{
    public class TellColorAction : GameAction
    {
        public override List<IGameValidator> CommandValidators => new List<IGameValidator> {new ColorInformationCorrectnessValidator()};

        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Tell color";
        public override string NativeCommandParams { get; set; }

        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Skip(NativeCommandStart.Length + 1).ToString();

            int wordsFromIndexes = 3;
            var args = NativeCommandParams.Split();

            CommandParameters = new CommandParams
            {
                Color = (GameSettings.GameColors) Enum.Parse(typeof (GameSettings.GameColors), args[0]),
                Indexes =  args.Skip(wordsFromIndexes).Select(int.Parse).ToList()
            };
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
                return GameSettings.CommandResult.Error;
            CommandParameters.Indexes.ForEach(index =>
            {
                game.NextPlayer.Hand[index].IsKnownColor = true;
                game.NextPlayer.Hand[index].PossibleColors.Clear();
                game.NextPlayer.Hand[index].PossibleColors.Add(CommandParameters.Color);
            });
            Enumerable.Range(0,game.NextPlayer.Hand.Count).Except(CommandParameters.Indexes)
                .ToList()
                .ForEach(index => game.NextPlayer.Hand[index].PossibleColors.Remove(CommandParameters.Color));
            return GameSettings.CommandResult.Ok;

        }
    }
}