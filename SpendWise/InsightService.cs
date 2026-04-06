using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise
{
    internal static class InsightService
    {
        public static string GetTopCategory(List<Transaction> transactions)
        {
            var expenses = transactions.Where(t => !t.IsIncome);

            if (!expenses.Any()) return "No expense data";

            var top = expenses
                .GroupBy(t => t.Category)
                .OrderByDescending(g => g.Sum(x => x.Amount))
                .First();

            return $"Top Category: {top.Key} ({top.Sum(x => x.Amount)})";
        }
        public static string GetMonthlyComparison(List<Transaction> transactions)
        {
            var grouped = transactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .ToList();

            if (grouped.Count < 2) return "Not enough data";

            var last = grouped[^1];
            var prev = grouped[^2];

            decimal lastExp = last.Where(t => !t.IsIncome).Sum(t => t.Amount);
            decimal prevExp = prev.Where(t => !t.IsIncome).Sum(t => t.Amount);

            if (prevExp == 0) return "No spending last month";

            decimal diff = lastExp - prevExp;

            decimal percent = ((lastExp - prevExp) / prevExp) * 100;

            return diff >= 0
               ? $"↑ Spending +₹{diff:F0} ({percent:F0}%)"
               : $"↓ Spending -₹{Math.Abs(diff):F0} ({percent:F0}%)";
        }
        public static string GetSavingsInsight(List<Transaction> transactions)
        {
            var grouped = transactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .ToList();

            if (grouped.Count < 2) return "No savings data";

            var last = grouped[^1];
            var prev = grouped[^2];

            decimal lastIncome = last.Where(t => t.IsIncome).Sum(t => t.Amount);
            decimal lastExpense = last.Where(t => !t.IsIncome).Sum(t => t.Amount);

            decimal prevIncome = prev.Where(t => t.IsIncome).Sum(t => t.Amount);
            decimal prevExpense = prev.Where(t => !t.IsIncome).Sum(t => t.Amount);

            decimal lastSavings = lastIncome - lastExpense;
            decimal prevSavings = prevIncome - prevExpense;

            if (prevSavings == 0) return "No previous savings data";

            decimal diff = lastSavings - prevSavings;
            decimal percent = Math.Abs((lastSavings - prevSavings) /prevSavings) * 100;

            return diff >= 0
                ? $"↑ Savings +₹{diff:F0} ({percent:F0}%)"
                : $"↓ Savings -₹{Math.Abs(diff):F0} ({percent:F0}%)";
        }
    }
}
