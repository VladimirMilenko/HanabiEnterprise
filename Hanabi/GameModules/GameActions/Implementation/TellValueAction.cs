using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation;
using Hanabi.GameModules.Validation.Implementation;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.GameActions.Implementation
{
    public class TellValueAction : GameAction
    {
        public override List<IGameValidator> CommandValidators => new List<IGameValidator> {new ValueInformationCorrectnessValidator()};

        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Tell rank";
        public override bool RequireGameCheck => false;
        public override string NativeCommandParams { get; set; }

        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Substring(NativeCommandStart.Length + 1);

            var wordsFromIndexes = 3;
            var args = NativeCommandParams.Split();

            CommandParameters = new CommandParams
            {
                Value = int.Parse(args[0]),
                Indexes =  args.Skip(wordsFromIndexes).Select(int.Parse).ToList()
            };
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
                return GameSettings.CommandResult.Failed;
            CommandParameters.Indexes.ForEach(index =>
            {
                game.NextPlayer.Hand[index].IsKnownValue = true;
                game.NextPlayer.Hand[index].PossibleValues.Clear();
                game.NextPlayer.Hand[index].PossibleValues.Add(CommandParameters.Value);
            });
            Enumerable.Range(0,game.NextPlayer.Hand.Count).Except(CommandParameters.Indexes)
                .ToList()
                .ForEach(index => game.NextPlayer.Hand[index].PossibleValues.Remove(CommandParameters.Value));


            return GameSettings.CommandResult.Success;

        }

        public override void FinalizeCommand(Game game, CommandParams commandParams, GameSettings.CommandResult commandResult, bool constraintViolation)
        {
            game.Statistics.Rounds++;
            game.SwapPlayers();
        }
    }
}