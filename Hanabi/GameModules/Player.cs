using System.Collections.Generic;
using System.Linq;

namespace Hanabi.GameModules
{
    public class Player
    {
        public List<Card> Hand { get; set; }

        public Player(List<Card> hand)
        {
            Hand = hand;
        }

        public static Player FromString(string input)
        {
            var cards = input.Split(' ');
            var playerCards = new List<Card>(GameSettings.HandSize);
            playerCards.AddRange(cards.Select(Card.FromString));
            return new Player(playerCards);
        }
    }
}
