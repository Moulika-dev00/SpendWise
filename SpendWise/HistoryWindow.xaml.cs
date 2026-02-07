using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpendWise
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        List<Transaction> allTransactions;

        internal HistoryWindow(List<Transaction> transactions)
        {
            InitializeComponent();
            allTransactions = transactions
            .Select(t => new Transaction
             {
                 Amount = t.Amount,
                 Description = t.Description,
                 Date = t.Date,
                 IsIncome = t.IsIncome,
                 Currency = t.Currency
             })
             .ToList();

            LoadMonths();
        }
        void LoadMonths()
        {
            var months = allTransactions
                .Select(t => t.Date.ToString("MMMM yyyy"))
                .Distinct()
                .ToList();

            MonthBox.ItemsSource = months;
            if (months.Count > 0)
                MonthBox.SelectedIndex = 0;
        }
        void MonthChanged(object sender, EventArgs e)
        {
            IncomeList.Items.Clear();
            ExpenseList.Items.Clear();

            if (MonthBox.SelectedItem == null) return;

            string selectedMonth = MonthBox.SelectedItem.ToString();

            var monthData = allTransactions
                .Where(t => t.Date.ToString("MMMM yyyy") == selectedMonth)
                .ToList();

            var incomes = monthData.Where(t => t.IsIncome).ToList();
            var expenses = monthData.Where(t => !t.IsIncome).ToList();

            foreach (var i in incomes)
                IncomeList.Items.Add(i);

            foreach (var e2 in expenses)
                ExpenseList.Items.Add(e2);

            var incomeTotals = incomes
                .GroupBy(t => t.Currency)
                .Select(g => $"{g.Key}: {g.Sum(x => x.Amount)}");

            var expenseTotals = expenses
                .GroupBy(t => t.Currency)
                .Select(g => $"{g.Key}: {g.Sum(x => x.Amount)}");

            IncomeTotalText.Text = "Total Income:\n" + string.Join("\n", incomeTotals);
            ExpenseTotalText.Text = "Total Expense:\n" + string.Join("\n", expenseTotals);

            var netTotals = monthData
                .GroupBy(t => t.Currency)
                .Select(g =>
                {
                     decimal net = g.Sum(t => t.IsIncome ? t.Amount : -t.Amount);
                     return $"{g.Key}: {net}";
               });

                NetTotalText.Text = "Net Balance:\n" + string.Join("\n", netTotals);
        }
        void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Transaction selected =
                IncomeList.SelectedItem as Transaction ??
                ExpenseList.SelectedItem as Transaction;

            if (selected == null)
            {
                MessageBox.Show("Select a transaction to delete");
                return;
            }

            allTransactions.Remove(selected);
            TransactionStorage.Save(allTransactions);

            MonthChanged(null, null); // refresh view
        }
        void ClearMonth_Click(object sender, RoutedEventArgs e)
        {
            if (MonthBox.SelectedItem == null) return;

            var confirm = MessageBox.Show(
                "Clear all transactions for this month?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            string selectedMonth = MonthBox.SelectedItem.ToString();

            allTransactions = allTransactions
                .Where(t => t.Date.ToString("MMMM yyyy") != selectedMonth)
                .ToList();

            TransactionStorage.Save(allTransactions);

            LoadMonths();
            MonthChanged(null, null);
        }

        void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
