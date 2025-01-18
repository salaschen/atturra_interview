using PayCalculator;

namespace PayCalculatorTest;

public class MedicareLevyDeductorTests {

    [Theory]
    [InlineData(0, 0)]
    [InlineData(21335, 0)]
    [InlineData(25000, 367)]
    [InlineData(40000, 800)]
    [InlineData(59361, 1188)]
    public void Test1(double taxableIncome, double expect) {
        var deductor = new MedicareLevyDeductor();
        var result = deductor.Deduct(taxableIncome);
        Assert.Equal(expect, result);
    }
}