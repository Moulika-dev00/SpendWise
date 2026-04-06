using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;

namespace SpendWise
{
    public partial class MainWindow : Window
    {
        List<Transaction> ledgerTransactions = new List<Transaction>();
        List<Transaction> sessionTransactions = new List<Transaction>();

        public MainWindow()
        {
            InitializeComponent();

            DatePickerBox.SelectedDate = DateTime.Now;

            ledgerTransactions = TransactionStorage.LoadLedger();
            sessionTransactions = TransactionStorage.LoadActive();

            UpdateBalanceAndView();
        }


        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(AmountBox.Text, out decimal amount))
            {
                MessageBox.Show("Enter valid amount");
                return;
            }

            if (string.IsNullOrWhiteSpace(DescBox.Text))
            {
                MessageBox.Show("Enter description");
                return;
            }

            if (TypeBox.SelectedItem == null)
            {
                MessageBox.Show("Select Income or Expense");
                return;
            }

            if (CurrencyBox.SelectedItem == null)
            {
                MessageBox.Show("Select currency");
                return;
            }

            bool isIncome =
                ((ComboBoxItem)TypeBox.SelectedItem).Content.ToString() == "Income";

            string currency =
                ((ComboBoxItem)CurrencyBox.SelectedItem).Content.ToString();

            string category =
                ((ComboBoxItem)CategoryBox.SelectedItem)?.Content?.ToString() ?? "Other";

            var selectedDate = DatePickerBox.SelectedDate ?? DateTime.Now;

            DateTime finalDate;
            if (selectedDate.Date == DateTime.Now.Date)
            {
                finalDate = DateTime.Now;
            }
            else
            {
                finalDate = selectedDate.Date;
            }

            decimal converted = ConvertToINR(amount, currency);

            var transaction = new Transaction
            {
                OriginalAmount = amount,
                Amount = converted,
                Description = DescBox.Text,
                Date = finalDate,
                IsIncome = isIncome,
                Currency = currency,
                Category = category
            };

            ledgerTransactions.Add(transaction.Clone());
            TransactionStorage.SaveLedger(ledgerTransactions);

            sessionTransactions.Add(transaction);
            TransactionStorage.SaveActive(sessionTransactions);

            UpdateBalanceAndView();

            AmountBox.Clear();
            DescBox.Clear();
            CategoryBox.SelectedIndex = -1;
        }

        private void UpdateBalanceAndView()
        {
            ApplyFilter();

            if (ledgerTransactions.Count > 0)
            {
                SpendingInsightText.Text =
                    InsightService.GetMonthlyComparison(ledgerTransactions);
                   
                SavingsInsightText.Text =
                    InsightService.GetSavingsInsight(ledgerTransactions);
            }
            else
            {
                SpendingInsightText.Text = "";
                SavingsInsightText.Text = "";
            }

            decimal totalBalance = ledgerTransactions
                .Sum(t => t.IsIncome ? t.Amount : -t.Amount);
            CardBalanceText.Text = $"₹{totalBalance:F2}";

            var topCategory = ledgerTransactions
                .Where(t => !t.IsIncome)
                .GroupBy(t => t.Category)
                .OrderByDescending(g => g.Sum(x => x.Amount))
                .FirstOrDefault();

            if (topCategory != null)
            {
                CardCategoryText.Text =
                    InsightService.GetTopCategory(ledgerTransactions)
                    .Replace("Top Category: ", "");
            }
            else
            {
                CardCategoryText.Text = "No data";
            }

            var latestMonth = ledgerTransactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .LastOrDefault();

            decimal monthlySavings = 0;

            if (latestMonth != null)
            {
                decimal income = latestMonth.Where(t => t.IsIncome).Sum(t => t.Amount);
                decimal expense = latestMonth.Where(t => !t.IsIncome).Sum(t => t.Amount);

                monthlySavings = income - expense;
            }

            CardSavingsText.Text =
              monthlySavings >= 0
              ? $"↑ ₹{monthlySavings:F2}"
              : $"↓ ₹{Math.Abs(monthlySavings):F2}";

            CardSavingsText.Foreground =
                monthlySavings >= 0
                ? Brushes.Green
                : Brushes.Red;

            LoadCategoryChart();
            LoadMonthlyChart();
        }

        private void ApplyFilter()
        {
            if (FilterBox.SelectedItem == null || TransactionList == null)
                return;

            TransactionList.Items.Clear();

            string filter =
                ((ComboBoxItem)FilterBox.SelectedItem).Content.ToString();

            string categoryFilter =
                ((ComboBoxItem)CategoryFilterBox.SelectedItem)?.Content?.ToString();

            IEnumerable<Transaction> query = sessionTransactions;

            if (filter == "Income")
                query = query.Where(t => t.IsIncome);
            else if (filter == "Expense")
                query = query.Where(t => !t.IsIncome);

            if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All")
                query = query.Where(t => t.Category == categoryFilter);

            var filteredList = query.ToList();

            foreach (var t in filteredList)
                TransactionList.Items.Add(t);

            if (filteredList.Count == 0)
            {
                ListTotalText.Text = "No data";
                return;
            }

            if (filter == "Income")
                ListTotalText.Text = $"Income Total: {filteredList.Sum(t => t.Amount)}";
            else if (filter == "Expense")
                ListTotalText.Text = $"Expense Total: {filteredList.Sum(t => t.Amount)}";
            else
                ListTotalText.Text = $"Transactions: {filteredList.Count}";
        }

        private void FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void DeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionList.SelectedItem == null)
            {
                MessageBox.Show("Please select a transaction to delete");
                return;
            }

            Transaction selected = (Transaction)TransactionList.SelectedItem;

            sessionTransactions.Remove(selected);
            TransactionStorage.SaveActive(sessionTransactions);

            UpdateBalanceAndView();
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            if (sessionTransactions.Count == 0)
            {
                MessageBox.Show("No transactions to clear");
                return;
            }

            var confirm = MessageBox.Show(
                "This will remove all transactions. Continue?",
                "Confirm Clear All",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {

                sessionTransactions.Clear();
                TransactionStorage.SaveActive(sessionTransactions);
                UpdateBalanceAndView();
            }
        }
        private void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            var ledger = TransactionStorage.LoadLedger();

            if (ledger.Count == 0 && sessionTransactions.Count > 0)
            {
                ledger = sessionTransactions
                    .Select(t => t.Clone())
                    .ToList();

                TransactionStorage.SaveLedger(ledger);
            }

            HistoryWindow hw = new HistoryWindow(ledger);
            hw.Owner = this;
            hw.ShowDialog();

            ledgerTransactions = TransactionStorage.LoadLedger();

            if (ledgerTransactions.Count == 0 && sessionTransactions.Count > 0)
            {
                ledgerTransactions = sessionTransactions
                    .Select(t => t.Clone())
                    .ToList();

                TransactionStorage.SaveLedger(ledgerTransactions);
            }

            UpdateBalanceAndView();
        }
       
        private void LoadCategoryChart()
        {
            var expenses = ledgerTransactions.Where(t => !t.IsIncome);

            if (!expenses.Any())
            {
                CategoryChart.Series = new List<ISeries>();
                return;
            }

            CategoryChart.Series = expenses
                .GroupBy(t => t.Category)
                .Select(g => new PieSeries<double>
                {
                    Values = new double[] { (double)g.Sum(x => x.Amount) },
                    Name = g.Key
                })
                .ToList<ISeries>();
        }
        private void LoadMonthlyChart()
        {
            var monthly = ledgerTransactions
                .Where(t => !t.IsIncome)
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Month = $"{g.Key.Month}/{g.Key.Year}",
                    Total = g.Sum(x => x.Amount)
                })
                .ToList();

            if (!monthly.Any())
            {
                MonthlyChart.Series = new List<ISeries>();
                return;
            }

            MonthlyChart.Series = new ISeries[]
            {
              new ColumnSeries<double>
              {
                Values = monthly.Select(x => (double)x.Total).ToArray(),
                Name = "Expenses"
              }
            };

            MonthlyChart.XAxes = new[]
            {
               new Axis
               {
                  Labels = monthly.Select(x => x.Month).ToArray()
               }
            };
        }
        private decimal ConvertToINR(decimal amount, string currency)
        {
            if (currency.Contains("USD"))
                return amount * 93.2884m;

            if (currency.Contains("EUR"))
                return amount * 107.515m;

            return amount;
        }
     
    }
}
