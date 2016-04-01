namespace Hanabi.GameModules
{
    public class GameStatistics
    {
        public int Rounds { get; set; }
        private int _riskySteps;
        public int RiskySteps { get; set; }

        public int CardsPlayed { get; set; }
    }
}