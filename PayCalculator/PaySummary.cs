using System.Text;

namespace PayCalculator;

public class PaySummary {
    public double gross = 0;
    public double super = 0;
    public double taxableIncome = 0; // Taxable income unrounded.
    public double taxableForDeductions = 0.0; // Taxable income rounded for deductions calculation.
    private double superContributionRate = 0.095; // 9.5% super
    public PayFrequency frequency;
    private List<IIncomeDeductor> incomeDeductors;
    private Dictionary<string, double> DeductionMap;

    public PaySummary() {
        incomeDeductors = new List<IIncomeDeductor>();
        DeductionMap = new();
    }

    public void SetGrossPay(double _gross) {
        gross = _gross;
        if (gross <= 0) {
            gross = 0;
            return;
        }

        // unrounded taxable income.
        taxableIncome = gross / (1 + superContributionRate);

        // super should be rounded up to the nearest cent,
        // and is based on an unrounded taxable income.
        super = RoundToNearestCent(gross - taxableIncome);

        // Taxable income rounded down to the nearest dollar when calculating deductions.
        taxableForDeductions = Math.Floor(taxableIncome);
    }

    public bool SetFrequency(char ch) {
        switch (ch) {
            case 'm':
            case 'M':
                frequency = PayFrequency.Monthly;
                return true;

            case 'w':
            case 'W':
                frequency = PayFrequency.Weekly;
                return true;

            case 'f':
            case 'F':
                frequency = PayFrequency.Fortnightly;
                return true;

            default:
                return false;
        }
    }

    public void RegisterDeductors(ICollection<IIncomeDeductor> deductors) {
        incomeDeductors.AddRange(deductors);
        ComputeDeductions(); // update deductions information.
    }

    private void ComputeDeductions() {
        foreach (var deductor in incomeDeductors) {
            var amount = deductor.Deduct(taxableForDeductions);
            var category = deductor.DeductName();
            DeductionMap[category] = amount;
        }
    }

    public string GetDeductionsInfo() {
        var builder = new StringBuilder();
        foreach (var key in DeductionMap.Keys) {
            builder.Append($"{key}: {DeductionMap[key]:c}\n");
        }
        return builder.ToString();
    }

    public double NetIncome() {
        double deductions = 0.0;
        foreach (var key in DeductionMap.Keys) {
            deductions += DeductionMap[key];
        }
        return RoundUpToCent(gross - super - deductions);
    }

    public string PaypacketMessage() {
        double net = NetIncome();
        string label = "Pay packet";
        double pay = 0;
        switch (frequency) {
            case PayFrequency.Weekly:
                pay = RoundUpToCent(net / 365 * 7);
                return $"{label}: {pay:c} per week";

            case PayFrequency.Fortnightly:
                pay = RoundUpToCent(net / 365 * 14);
                return $"{label}: {pay:c} per fortnight";

            case PayFrequency.Monthly:
                pay = RoundUpToCent(net / 12);
                return $"{label}: {pay:c} per month";

            default:
                return "Error when generating pay packet message.";
        }
    }

    public double RoundUpToCent(double original) {
        return Math.Ceiling(original * 100) / 100.0;
    }

    public double RoundToNearestCent(double original) {
        return Math.Round(original * 100) / 100.0;
    }

    public enum PayFrequency {
        Weekly,
        Fortnightly,
        Monthly
    }
}