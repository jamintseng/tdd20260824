namespace ConsoleApp1;

internal class TennisGame
{
    private static readonly string[] ScoreNames = ["Love", "Fifteen", "Thirty"];

    public string Score(int player1, int player2)
    {
        return $"{ScoreNames[player1]}-All";
    }
}
