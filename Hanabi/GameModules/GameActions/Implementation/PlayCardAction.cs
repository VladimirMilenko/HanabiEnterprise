using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.Validation;
using Hanabi.GameModules.Validation.Implementation;
using Hanabi.GameModules.Validation.Interfaces;

namespace Hanabi.GameModules.GameActions.Implementation
{
    public class PlayCardAction : GameAction
    {
        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Play card";
        public override bool RequireGameCheck => true;
        public override string NativeCommandParams { get; set; }

        public override List<IGameValidator> CommandValidators => new List<IGameValidator> { new PlayCardValidator() };

        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Substring(NativeCommandStart.Length + 1);
            CommandParameters = new CommandParams { Indexes = new List<int>() };
            CommandParameters.Indexes.Add(int.Parse(NativeCommandParams));
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
            {
                //In this case we can't throw an exception cuz it will crush the program, so we will return unavailability of step
                return GameSettings.CommandResult.Failed;
            }
            var card = game.CurrentPlayer.Hand[CommandParameters.FirstIndex];
            var knownTable = game.Desk.Values.All(value => value == game.Desk.Values.First());

            var possibleColorsEqual = !card.PossibleColors.Select(color => game.Desk[color]).Distinct().Skip(1).Any();

            if ((possibleColorsEqual || knownTable) &&
                card.IsKnownValue)
            {
                game.Desk[card.CardColor]++;
                return GameSettings.CommandResult.Success;
            }
            game.Desk[card.CardColor]++;
            var result = card.IsKnown()
                ? GameSettings.CommandResult.Success
                : GameSettings.CommandResult.Risky;

            return result;
        }

        public override void FinalizeCommand(Game game, CommandParams commandParams, GameSettings.CommandResult commandResult, bool constraintViolation)
        {
            game.Statistics.Rounds++;
            if (commandResult == GameSettings.CommandResult.Failed)// || constraintViolation)
                return;
            if (commandResult == GameSettings.CommandResult.Risky)
            {
                game.Statistics.RiskySteps++;
            }
            if (constraintViolation)
            {
                game.Statistics.CardsPlayed++;
                return;
            }
            game.Statistics.CardsPlayed++;
            game.PickNewCardFromDeck(commandParams.FirstIndex);
            game.SwapPlayers();
        }
    }
}