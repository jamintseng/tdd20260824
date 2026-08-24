namespace ConsoleApp1;

internal class TennisGame
{
    private static readonly string[] ScoreNames = ["Love", "Fifteen", "Thirty", "Forty"];

    public string Score(int player1, int player2)
    {
        if (player1 < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(player1));
        }

        if (player2 < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(player2));
        }

        if (player1 == player2)
        {
            return player1 >= 3 ? "Deuce" : $"{ScoreNames[player1]}-All";
        }

        var leader = player1 > player2 ? "player1" : "player2";

        if (Math.Max(player1, player2) >= 4 && Math.Abs(player1 - player2) >= 2)
        {
            return $"Win for {leader}";
        }

        if (player1 >= 3 && player2 >= 3)
        {
            return $"Advantage {leader}";
        }

        return $"{ScoreNames[player1]}-{ScoreNames[player2]}";
    }
}
