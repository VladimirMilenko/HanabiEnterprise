using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation;

namespace Hanabi.GameModules.GameActions
{
    public class PlayCardAction : GameAction
    {
        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Play card";
        public override string NativeCommandParams { get; set; }

        public override List<IGameValidator> CommandValidators => new List<IGameValidator> { new PlayCardValidator() };

        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Skip(NativeCommandStart.Length + 1).ToString();
            CommandParameters = new CommandParams { Cards = new List<Card>() };
            CommandParameters.Cards.Add(Card.FromString(NativeCommandParams));
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
            {
                //In this case we can't throw an exception cuz it will crush the program, so we will return unavailability of step
                return GameSettings.CommandResult.Error;

                //throw new ArgumentException(
                //    $"Unable to play card {CommandParameters.Cards[0].Value} with color {CommandParameters.Cards[0].Color}");
            }
            var knownTable = game.Desk.Values.All(value => value == game.Desk.Values.First());
            
            var possibleColorsEqual = !game.CurrentPlayer.Hand[CommandParameters.FirstIndex].PossibleColors.Select(color => game.Desk[color]).Distinct().Skip(1).Any();

            if ((possibleColorsEqual || knownTable) &&
                game.CurrentPlayer.Hand[CommandParameters.FirstIndex].IsKnownValue)
                return GameSettings.CommandResult.Ok;

            return game.CurrentPlayer.Hand[CommandParameters.FirstIndex].IsKnown() ? GameSettings.CommandResult.Ok : GameSettings.CommandResult.Risky;
        }
    }
}