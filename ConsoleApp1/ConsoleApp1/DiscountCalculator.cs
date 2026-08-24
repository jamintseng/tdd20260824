namespace ConsoleApp1;

internal class DiscountCalculator
{
    public decimal Calculate(decimal amount)
    {
        if (amount > 1000m)
        {
            return amount * 0.9m;
        }

        return amount;
    }
}
