using PayCalculator;

namespace PayCalculatorTest; 
public class PaySummaryTests {
    
    [Theory]
    [InlineData(65000, 59360.73)]
    [InlineData(0, 0)]
    [InlineData(18000, 16438.36)]
    public void TestNetIncome(double gross, double expect) {
        var summary = new PaySummary();
        summary.SetGrossPay(gross);
        Assert.Equal(expect, summary.NetIncome());
    }

    [Fact]
    public void ExampleTest() {
        // Arrange
        var summary = new PaySummary();

        // Act
        summary.SetFrequency('m');
        summary.SetGrossPay(65000);
        summary.RegisterDeductors(
            new List<IIncomeDeductor>() {
                new MedicareLevyDeductor(),
                new BudgetRepairLevyDeductor(),
                new IncomeTaxDeductor()
            }
        );

        // Assert
        Assert.Equal(65000, summary.gross);
        Assert.Equal(5639.27, summary.super);
        Assert.True(Math.Abs(59360.73-summary.taxableIncome) < 0.01);
        Assert.Equal(47333.73, summary.NetIncome());
        Assert.Equal("Pay packet: $3,944.48 per month", summary.PaypacketMessage());
    }
}
