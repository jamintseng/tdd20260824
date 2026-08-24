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

    [TestCase(0)]
    [TestCase(-1)]
    public void Calculate_NonPositiveAmount_ThrowsArgumentOutOfRange(decimal amount)
    {
        var sut = new DiscountCalculator();

        Assert.That(() => sut.Calculate(amount),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Calculate_DiscountedAmountHasFractionalCents_RoundsToTwoDecimalPlaces()
    {
        var sut = new DiscountCalculator();

        var result = sut.Calculate(1234.56m);

        Assert.That(result, Is.EqualTo(1111.10m));
    }

    [Test]
    public void Calculate_DiscountedAmountIsExactlyHalfCent_RoundsAwayFromZero()
    {
        var sut = new DiscountCalculator();

        var result = sut.Calculate(1000.05m);

        Assert.That(result, Is.EqualTo(900.05m));
    }
}
