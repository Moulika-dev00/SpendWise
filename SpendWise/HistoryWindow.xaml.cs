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
                 OriginalAmount = t.OriginalAmount,
                 Description = t.Description,
                 Date = t.Date,
                 IsIncome = t.IsIncome,
                 Currency = t.Currency,
                 Category = t.Category
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

            decimal totalIncomeINR = incomes.Sum(t => t.Amount);
            decimal totalExpenseINR = expenses.Sum(t => t.Amount);
            decimal netINR = totalIncomeINR - totalExpenseINR;

            IncomeTotalText.Text = $"Total Income:\nINR  (₹): {totalIncomeINR:F2}";
            ExpenseTotalText.Text = $"Total Expense:\nINR  (₹): {totalExpenseINR:F2}";
            NetTotalText.Text = $"Net Balance:\nINR  (₹): {netINR:F2}";

            RefreshUI();
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
            TransactionStorage.SaveLedger(allTransactions);

            RefreshUI();
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

            TransactionStorage.SaveLedger(allTransactions);

            LoadMonths();

            if (MonthBox.Items.Count > 0)
                MonthBox.SelectedIndex = 0;

            RefreshUI();
        }

        void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void RefreshUI()
        {
            if (MonthBox.SelectedItem == null)
            {
                IncomeList.Items.Clear();
                ExpenseList.Items.Clear();
                IncomeTotalText.Text = "";
                ExpenseTotalText.Text = "";
                NetTotalText.Text = "";
                return;
            }

            string selectedMonth = MonthBox.SelectedItem.ToString();

            var monthData = allTransactions
                .Where(t => t.Date.ToString("MMMM yyyy") == selectedMonth)
                .ToList();
    
            IncomeList.Items.Clear();
            ExpenseList.Items.Clear();

            foreach (var i in monthData.Where(t => t.IsIncome))
                IncomeList.Items.Add(i);

            foreach (var e in monthData.Where(t => !t.IsIncome))
                ExpenseList.Items.Add(e);

            // totals
            decimal totalIncomeINR = monthData
                  .Where(t => t.IsIncome)
                  .Sum(t => t.Amount);

            decimal totalExpenseINR = monthData
                .Where(t => !t.IsIncome)
                .Sum(t => t.Amount);

            decimal netINR = totalIncomeINR - totalExpenseINR;

            IncomeTotalText.Text = $"Total Income:\nINR (₹): {totalIncomeINR:F2}";
            ExpenseTotalText.Text = $"Total Expense:\nINR (₹): {totalExpenseINR:F2}";
            NetTotalText.Text = $"Net Balance:\nINR (₹): {netINR:F2}";

        }
    }

}
