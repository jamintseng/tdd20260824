namespace ConsoleApp1;

internal class DiscountCalculator
{
    private const int CurrencyDecimals = 2;

    // 門檻由高至低排列，取第一個符合者。新增級距請維持此順序。
    private static readonly (decimal Threshold, decimal Rate)[] Tiers =
    [
        (5000m, 0.8m),
        (1000m, 0.9m),
    ];

    public decimal Calculate(decimal amount)
    {
        if (amount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        foreach (var (threshold, rate) in Tiers)
        {
            if (amount > threshold)
            {
                return RoundToCents(amount * rate);
            }
        }

        return amount;
    }

    private static decimal RoundToCents(decimal value)
        => decimal.Round(value, CurrencyDecimals, MidpointRounding.AwayFromZero);
}
