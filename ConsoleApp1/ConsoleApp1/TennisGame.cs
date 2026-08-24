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

        if (Math.Max(player1, player2) >= 4 && Math.Abs(player1 - player2) >= 2)
        {
            return player1 > player2 ? "Win for player1" : "Win for player2";
        }

        if (player1 >= 3 && player2 >= 3)
        {
            return player1 > player2 ? "Advantage player1" : "Advantage player2";
        }

        return $"{ScoreNames[player1]}-{ScoreNames[player2]}";
    }
}
