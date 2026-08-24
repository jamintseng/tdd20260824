namespace ConsoleApp1;

internal class DiscountCalculator
{
    public decimal Calculate(decimal amount)
    {
        if (amount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (amount > 5000m)
        {
            return decimal.Round(amount * 0.8m, 2, MidpointRounding.AwayFromZero);
        }

        if (amount > 1000m)
        {
            return decimal.Round(amount * 0.9m, 2, MidpointRounding.AwayFromZero);
        }

        return amount;
    }
}
