using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCalculator;

internal class ProcessUserInput {
    // stores the converted user input.
    public static double amount = 0.0;

    /// <summary>
    /// Parse user input into a number.
    /// Return true if input is valid, and result is stored in the amount property.
    /// Return false otherwise.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool Process(string input) {
        // allow one leading $ symbol
        if (input.ToCharArray().Where(ch => ch == '$').Count() >= 2) {
            return false;
        }
        input = input.TrimStart('$');
        if (double.TryParse(input, out double result)) {
            amount = result;
            amount = double.Round(amount, 2);
            return true;
        } else {
            return false;
        }
    }
}