using System;
using System.Collections.Generic;
using Hanabi.GameModules.GameActions;
using Hanabi.IO;

namespace Hanabi.GameModules
{
    public class Game
    {
        public Dictionary<GameSettings.GameColors, int> Desk { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player NextPlayer { get; private set; }
        public Queue<Card> Deck { get; private set; }

        private IList<GameAction> _gameActions;
        private IOService _ioService;
        private StepDeterminerService _stepDeterminerService;

        public Game(IOService ioService, IList<GameAction> gameActions)
        {
            _gameActions = gameActions;
            _ioService = ioService;
            _stepDeterminerService = new StepDeterminerService(this, _gameActions);
        }

        public void Run()
        {
            bool endOfStream = false;
            while (!endOfStream)
            {
                string command = _ioService.ReadLine();
                endOfStream = command == null;
                if (endOfStream) break;
                _stepDeterminerService.ProcessCommand(command);
            }
        }
        public void StartNewGame(Player firstPlayer, Player secondPlayer, Queue<Card> deck)
        {
            CurrentPlayer = firstPlayer;
            NextPlayer = secondPlayer;
            Deck = deck;
            Desk = new Dictionary<GameSettings.GameColors, int>(Enum.GetValues(typeof (GameSettings.GameColors)).Length);
            foreach (GameSettings.GameColors color in Enum.GetValues(typeof(GameSettings.GameColors)))
            {
                //For better extensibility, in case if card's values starts not from 1
                Desk[color] = GameSettings.CardMinValue - 1;
            }
        }
    }

    internal enum GameState
    {
        Playing,
        Finished
    }

}
