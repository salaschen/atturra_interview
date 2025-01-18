using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PayCalculator; 
public class IncomeTaxDeductor: IIncomeDeductor {
    public double Deduct(double original) {
        if (original >= 180_001) {
            return marginalTax(original, 0.47, 54232, 180_000);
        } else if (original >= 87001) {
            return marginalTax(original, 0.37, 19822, 87000);
        } else if (original >= 37001) {
            return marginalTax(original, 0.325, 3572, 37000);
        } else if (original >= 18201) {
            return marginalTax(original, 0.19, 0, 18200);
        } else {
            return 0;
        }

        // local method
        double marginalTax(double original, double marginalRate, double deduction, double limit) {
            return deduction + marginalRate * (original - limit);
        }
    }

    public string DeductName() => "Income Tax";
}
