using System;
using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules.Validation;

namespace Hanabi.GameModules.GameActions
{
    public class StartNewGameAction : GameAction
    {
        public override CommandParams CommandParameters { get; set; }
        public override string NativeCommandStart => "Start new game with deck";
        public override string NativeCommandParams { get; set; }

        public override List<IGameValidator> CommandValidators => new List<IGameValidator> { new StartGameValidator() };

        public override void SetParametersFromInitString(string initString)
        {
            NativeCommandParams = initString.Skip(NativeCommandStart.Length + 1).ToString();
            CommandParameters = new CommandParams { Cards = new List<Card>() };
            CommandParameters.Cards.AddRange(NativeCommandParams.Split(' ').Select(Card.FromString));
        }

        public override GameSettings.CommandResult ProcessCommand(Game game)
        {
            if (!CommandValidators.TrueForAll(x => x.IsValid(game, CommandParameters)))
                throw new ArgumentException(
                    $"Invalid count of cards {CommandParameters.Cards.Count}. Must be greater or equal than {GameSettings.MinCards}");

            Player firstPlayer = new Player(CommandParameters.Cards.Take(GameSettings.HandSize)
                .ToList()
                );
            Player secondPlayer = new Player(CommandParameters.Cards.Skip(GameSettings.HandSize)
                .Take(GameSettings.HandSize)
                .ToList()
                );
            Queue<Card> deck = new Queue<Card>(CommandParameters.Cards.Skip(GameSettings.HandSize*2)
                .ToList()
                );
            game.StartNewGame(firstPlayer, secondPlayer, deck);

            return GameSettings.CommandResult.Ok;
        }
    }
}