using PayCalculator;

namespace PayCalculatorTest;

public class BudgetRepairLevyDeductorTests {

    [Theory]
    [InlineData(0, 0)]
    [InlineData(180_000, 0)]
    [InlineData(200_000, 400)]
    public void Test1(double taxableIncome, double expect) {
        var deductor = new BudgetRepairLevyDeductor();
        var result = deductor.Deduct(taxableIncome);
        Assert.Equal(expect, result);
    }
}