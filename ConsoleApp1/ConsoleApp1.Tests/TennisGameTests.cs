namespace ConsoleApp1.Tests;

[TestFixture]
public class TennisGameTests
{
    [TestCase(0, 0, "Love-All")]
    [TestCase(1, 1, "Fifteen-All")]
    [TestCase(2, 2, "Thirty-All")]
    public void Score_EqualScoresBelowForty_ReturnsScoreAll(
        int player1, int player2, string expected)
    {
        var sut = new TennisGame();

        var result = sut.Score(player1, player2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(3, 3)]
    [TestCase(4, 4)]
    public void Score_EqualScoresAtFortyOrAbove_ReturnsDeuce(int player1, int player2)
    {
        var sut = new TennisGame();

        var result = sut.Score(player1, player2);

        Assert.That(result, Is.EqualTo("Deuce"));
    }

    [TestCase(1, 0, "Fifteen-Love")]
    [TestCase(2, 1, "Thirty-Fifteen")]
    [TestCase(3, 2, "Forty-Thirty")]
    public void Score_DifferentScoresBelowForty_ReturnsBothScores(
        int player1, int player2, string expected)
    {
        var sut = new TennisGame();

        var result = sut.Score(player1, player2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(4, 3, "Advantage player1")]
    [TestCase(3, 4, "Advantage player2")]
    public void Score_BothAtFortyAndOnePointLead_ReturnsAdvantage(
        int player1, int player2, string expected)
    {
        var sut = new TennisGame();

        var result = sut.Score(player1, player2);

        Assert.That(result, Is.EqualTo(expected));
    }
}
