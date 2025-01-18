namespace PayCalculator;

internal class Program {

    private static void Main(string[] args) {
        Console.Write("Enter your salary package amount: ");
        string input = Console.ReadLine()?? "";

        // Step 1: Read User Input
        while (ProcessUserInput.Process(input) == false) {
            Console.WriteLine("Please input a valid number and try again.\n");
            Console.Write("Enter your salary package amount: ");
            input = Console.ReadLine()?? "";
        }

        var paySummary = new PaySummary();
        paySummary.SetGrossPay(ProcessUserInput.amount);

        Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
        input = Console.ReadLine()?? " ";
        while (input.Length != 1 || paySummary.SetFrequency(input[0]) == false) {
            Console.WriteLine("Invalid frequency type, please select one of the available types.\n");
            Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
            input = Console.ReadLine()?? " ";
        }

        // Step 2: Register deductors
        paySummary.RegisterDeductors(
            new List<IIncomeDeductor>() {
                new IncomeTaxDeductor()
            }
        );

        // Step 3: 
        Console.WriteLine(paySummary.GetDeductionsInfo());

        // Debug print
        Console.WriteLine($"total={paySummary.gross}, taxable={paySummary.taxableIncome}, super={paySummary.super}");
        Console.WriteLine($"taxable (rounded) = {paySummary.taxableForDeductions}");
        Console.WriteLine($"frequency={paySummary.frequency}");

        // Tests
        // TestProcess(); // Debug
    }



    // Test code.
    private static void TestProcess() {
        var inputList = new List<(string, bool)>() {
            ("$100", true), ("$$25", false), 
            ("$1.5", true), ("$1000,000,000", true),
            ("$1.564", true), ("$1.567", true),
            ("", false)
        };

        foreach (var input in inputList) {
            var result = ProcessUserInput.Process(input.Item1);
            Console.WriteLine($"result={result}, expect={input.Item2}, amount={ProcessUserInput.amount}");
        }
    }
}