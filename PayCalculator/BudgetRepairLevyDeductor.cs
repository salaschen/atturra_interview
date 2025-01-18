using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCalculator; 
public class BudgetRepairLevyDeductor: IIncomeDeductor {
    public string DeductName() => "Budget Repair Levy";

    public double Deduct(double original) {
        if (original >= 180_001) {
            return Math.Ceiling(0.02 * (original - 180_000));
        } else {
            return 0;
        }
    }
}
