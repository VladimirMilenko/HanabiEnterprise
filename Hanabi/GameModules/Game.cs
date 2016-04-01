using System;
using System.Collections.Generic;
using Hanabi.GameModules.GameActions;
using Hanabi.GameModules.GameActions.Abstract;
using Hanabi.GameModules.GameActions.Implementation;
using Hanabi.IO;

namespace Hanabi.GameModules
{
    public class Game
    {
        public Dictionary<GameSettings.GameColors, int> Desk { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player NextPlayer { get; private set; }
        public Queue<Card> Deck { get; private set; }
        public GameStatistics Statistics { get; set; }
        private GameState _currentGameState;
        private GameState CurrentGameState
        {
            get
            {
                return _currentGameState;
            }
            set
            {
                _currentGameState = value;
            }
        }

        private IList<GameAction> _gameActions;
        private IOService _ioService;
        private StepDeterminerService _stepDeterminerService;


        public Game(IOService ioService, IList<GameAction> gameActions)
        {
            _gameActions = gameActions;
            _ioService = ioService;
            _stepDeterminerService = new StepDeterminerService(this, _gameActions);
            Statistics = new GameStatistics();
        }

        private bool _waitingForNewGameStart = false;
        public void Run()
        {
            string command;

            while ((command = _ioService.ReadLine()) != null)
            {

                var commandResult = _stepDeterminerService.ProcessCommand(command);
                if (_waitingForNewGameStart && _currentGameState == GameState.Finished) continue;

                if (commandResult == GameState.Finished)
                {
                    _waitingForNewGameStart = true;
                    _currentGameState = GameState.Finished;
                }
                if (_waitingForNewGameStart)
                {
                    _ioService.WriteLine($"Turn: {Statistics.Rounds}, cards: {Statistics.CardsPlayed}, with risk: {Statistics.RiskySteps}");
                }
            }
        }

        public void PickNewCardFromDeck(int index)
        {
            CurrentPlayer.Hand.RemoveAt(index);
            CurrentPlayer.Hand.Add(Deck.Dequeue());
        }
        public void SwapPlayers()
        {
            var temp = CurrentPlayer;
            CurrentPlayer = NextPlayer;
            NextPlayer = temp;
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
            CurrentGameState = GameState.Playing;
            Statistics = new GameStatistics();
            _waitingForNewGameStart = false;
        }
    }

    public enum GameState
    {
        Playing,
        Finished
    }

}
