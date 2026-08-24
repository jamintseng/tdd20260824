namespace ConsoleApp1;

internal class TennisGame
{
    private static readonly string[] ScoreNames = ["Love", "Fifteen", "Thirty", "Forty"];

    private readonly string _player1Name;
    private readonly string _player2Name;

    public TennisGame() : this("player1", "player2")
    {
    }

    public TennisGame(string player1Name, string player2Name)
    {
        _player1Name = player1Name;
        _player2Name = player2Name;
    }

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

        var leader = player1 > player2 ? _player1Name : _player2Name;

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
