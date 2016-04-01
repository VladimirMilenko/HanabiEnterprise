using System;
using System.Collections.Generic;
using System.Linq;
using Hanabi.GameModules;

namespace Hanabi.GameModules
{
    public class Card
    {
        public GameSettings.GameColors CardColor;
        public int Value;
        private bool _isKnownColor;

        public bool IsKnownColor
        {
            get { return _isKnownColor || PossibleColors.Count == 1; }
            set { _isKnownColor = value; }
        }

        private bool _isKnownValue;

        public bool IsKnownValue
        {
            get
            {
                return _isKnownValue || PossibleValues.Count == 1;
            }
            set { _isKnownValue = value; }
        }

        public bool IsKnown() => IsKnownValue && IsKnownColor;
        public List<GameSettings.GameColors> PossibleColors { get; set; }
        public List<int> PossibleValues { get; set; }
        

        public Card(GameSettings.GameColors color, int value)
        {
            CardColor = color;
            Value = value;
            _isKnownColor = false;
            _isKnownValue = false;
            PossibleColors = Enum.GetValues(typeof(GameSettings.GameColors)).OfType<GameSettings.GameColors>().ToList();

            PossibleValues = new List<int>();
            PossibleValues.AddRange(Enumerable.Range(GameSettings.CardMinValue, GameSettings.CardMaxValue-GameSettings.CardMinValue + 1));
        }
        public static Card FromString(string inputString)
        {
            char cardColorIndentifier = inputString[0];

            string cardColorName = Enum.GetNames(typeof (GameSettings.GameColors)).FirstOrDefault(x => x[0] == cardColorIndentifier);
            if (cardColorName == null)
            {
                throw new ArgumentException($"Unknown card color identifier {cardColorIndentifier}");
            }
            GameSettings.GameColors cardColor = (GameSettings.GameColors)Enum.Parse(typeof(GameSettings.GameColors), cardColorName);

            int cardValue;
            if(!int.TryParse(inputString[1].ToString(),out cardValue) || cardValue < GameSettings.CardMinValue || cardValue > GameSettings.CardMaxValue)
            {
                throw new ArgumentException($"Wrong card value {inputString[1]}");
            }
            return new Card(cardColor, cardValue);
        }
    }
    
}
