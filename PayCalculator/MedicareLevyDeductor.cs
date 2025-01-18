namespace PayCalculator;

public class MedicareLevyDeductor: IIncomeDeductor {
    public string DeductName() => "Medicare Levy";

    public double Deduct(double original) {
        if (original >= 26669) {
            return Math.Ceiling(0.02 * original);
        } else if (original >= 21336) {
            return Math.Ceiling(0.1 * (original - 21335));
        } else {
            return 0;
        }
    }
}