namespace ConsoleApp1;

internal class TennisGame
{
    private static readonly string[] ScoreNames = ["Love", "Fifteen", "Thirty", "Forty"];

    public string Score(int player1, int player2)
    {
        if (player1 == player2)
        {
            return player1 >= 3 ? "Deuce" : $"{ScoreNames[player1]}-All";
        }

        return $"{ScoreNames[player1]}-{ScoreNames[player2]}";
    }
}
