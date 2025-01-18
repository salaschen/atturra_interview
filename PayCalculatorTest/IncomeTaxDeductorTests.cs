using PayCalculator;

namespace PayCalculatorTest;
public class IncomeTaxDeductorTests {

    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, 0)]
    [InlineData(25000, 1292)]
    [InlineData(45000, 6172)]
    [InlineData(95000, 22782)]
    [InlineData(200_000, 63_632)]
    public void Test1(double taxableIncome, double expect) {
        var deductor = new IncomeTaxDeductor();
        var result = deductor.Deduct(taxableIncome);
        Assert.Equal(result, expect);
    }
}