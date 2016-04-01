using System;
using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation;

namespace Hanabi.GameModules.GameActions
{
    public class TellValueAction : GameAction
    {
        public override List<IGameValidator> CommandValidators => new List<IGameValidator> {new ValueInformationCorrectnessValidator()};

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
                game.NextPlayer.Hand[index].IsKnownValue = true;
                game.NextPlayer.Hand[index].PossibleValues.Clear();
                game.NextPlayer.Hand[index].PossibleValues.Add(CommandParameters.Value);
            });
            Enumerable.Range(0,game.NextPlayer.Hand.Count).Except(CommandParameters.Indexes)
                .ToList()
                .ForEach(index => game.NextPlayer.Hand[index].PossibleValues.Remove(CommandParameters.Value));
            return GameSettings.CommandResult.Ok;

        }
    }
}