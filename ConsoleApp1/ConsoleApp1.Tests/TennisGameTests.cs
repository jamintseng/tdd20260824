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
}
