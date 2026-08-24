namespace ConsoleApp1.Tests;

[TestFixture]
public class DiscountCalculatorTests
{
    [Test]
    public void Calculate_AmountOver1000_Returns10PercentOff()
    {
        var sut = new DiscountCalculator();

        var result = sut.Calculate(1500m);

        Assert.That(result, Is.EqualTo(1350m));
    }

    [Test]
    public void Calculate_AmountNotOver1000_ReturnsOriginalAmount()
    {
        var sut = new DiscountCalculator();

        var result = sut.Calculate(1000m);

        Assert.That(result, Is.EqualTo(1000m));
    }
}
