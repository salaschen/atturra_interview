using PayCalculator;

namespace PayCalculatorTest;

public class ProcessUserInputTests {

    [Theory]
    [InlineData("123.45", true, 123.45)]
    [InlineData("$123.45", true, 123.45)]
    [InlineData("  $123.45  ", true, 123.45)]
    [InlineData("123", true, 123.0)]
    [InlineData("$0.99", true, 0.99)]
    [InlineData("$-123.45", true, -123.45)]
    [InlineData("0", true, 0.0)]
    [InlineData("$0.00", true, 0.0)]
    [InlineData("$00123.45", true, 123.45)]
    [InlineData("$123.", true, 123.0)]
    [InlineData("$123.450", true, 123.45)]
    [InlineData("abc123", false, 0.0)]
    [InlineData("123.abc", false, 0.0)]
    [InlineData("$", false, 0.0)]
    [InlineData("$$123", false, 0.0)]
    [InlineData("123,456.78", true, 123456.78)]
    [InlineData("", false, 0.0)]
    public void TestParse(string input, bool parseOk, double expect) {
        var result = ProcessUserInput.Process(input);
        Assert.Equal(parseOk, result);
        if (parseOk) {
            Assert.Equal(expect, ProcessUserInput.amount);
        }
    }
}