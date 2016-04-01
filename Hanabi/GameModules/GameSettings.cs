namespace Hanabi.GameModules
{
    public static class GameSettings
    {
        public static int DeskCardDistance = 1;
        public static int HandSize = 5;
        public static int MinCards = 11;
        public static int CardMinValue = 1;
        public static int CardMaxValue = 5;
        public enum GameColors
        {
            Red,
            Blue,
            White,
            Green,
            Yellow
        }

        public enum CommandResult
        {
            Ok,
            Error,
            Risky
        }
    }
}
