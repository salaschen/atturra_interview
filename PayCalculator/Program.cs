namespace PayCalculator;

internal class Program {

    private static void Main(string[] args) {
        // Step 1: Read User Input
        Console.Write("Enter your salary package amount: ");
        string input = Console.ReadLine()?? "";
        while (ProcessUserInput.Process(input) == false) {
            Console.WriteLine("Please input a valid number and try again.\n");
            Console.Write("Enter your salary package amount: ");
            input = Console.ReadLine()?? "";
        }

        var paySummary = new PaySummary();
        paySummary.SetGrossPay(ProcessUserInput.amount);

        // Get frequency input
        Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
        input = Console.ReadLine()?? " ";
        while (input.Length != 1 || paySummary.SetFrequency(input[0]) == false) {
            Console.WriteLine("Invalid frequency type, please select one of the available types.\n");
            Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
            input = Console.ReadLine()?? " ";
        }

        // Step 2: Register deductors and compute deductions.
        Console.WriteLine("Calculating salary details...\n");
        paySummary.RegisterDeductors(
            new List<IIncomeDeductor>() {
                new MedicareLevyDeductor(),
                new BudgetRepairLevyDeductor(),
                new IncomeTaxDeductor()
            }
        );

        // Step 3: Print out information.
        Console.WriteLine($"Gross package: {paySummary.RoundUpToCent(paySummary.gross):c}");
        Console.WriteLine($"Superannuation: {paySummary.super:c}\n");

        Console.WriteLine("Deductions:");
        Console.WriteLine(paySummary.GetDeductionsInfo()+"\n");

        Console.WriteLine($"Net income: {paySummary.NetIncome():c}");
        Console.WriteLine(paySummary.PaypacketMessage()+"\n");


        Console.WriteLine("Press any key to end...");
        Console.ReadKey();
    }
}