using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpendWise
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ledgerTransactions = TransactionStorage.Load();

            sessionTransactions = ledgerTransactions
                .Select(t => t.Clone())
                .ToList();

            UpdateBalanceAndView();
        }

        List<Transaction> ledgerTransactions = new List<Transaction>(); // history
        List<Transaction> sessionTransactions = new List<Transaction>(); // calculator


        // balance per currency
        Dictionary<string, decimal> balances = new Dictionary<string, decimal>();

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

            var transaction = new Transaction
            {
                Amount = amount,
                Description = DescBox.Text,
                Date = DateTime.Now,
                IsIncome = isIncome,
                Currency = currency
            };

            ledgerTransactions.Add(transaction.Clone());
            TransactionStorage.Save(ledgerTransactions);

            sessionTransactions.Add(transaction);
            UpdateBalanceAndView();

            AmountBox.Clear();
            DescBox.Clear();
        }

        private void UpdateBalanceAndView()
        {
            balances.Clear();

            foreach (var t in sessionTransactions)
            {
                if (!balances.ContainsKey(t.Currency))
                    balances[t.Currency] = 0;

                balances[t.Currency] += t.IsIncome ? t.Amount : -t.Amount;
            }

            // show balances of all currencies
            TotalText.Text = balances.Count == 0
                ? "Balance: 0"
                : string.Join("   ", balances.Select(b => $"{b.Key}: {b.Value}"));

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (FilterBox.SelectedItem == null) return;
            if (TransactionList == null) return;

            TransactionList.Items.Clear();

            string filter =
                ((ComboBoxItem)FilterBox.SelectedItem).Content.ToString();

            IEnumerable<Transaction> query = sessionTransactions;

            if (filter == "Income")
                query = sessionTransactions.Where(t => t.IsIncome);
            else if (filter == "Expense")
                query = sessionTransactions.Where(t => !t.IsIncome);

            var filteredList = query.ToList();

            foreach (var t in filteredList)
                TransactionList.Items.Add(t);

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
            ledgerTransactions.RemoveAll(t =>
                 t.Amount == selected.Amount &&
                 t.Description == selected.Description &&
                 t.Date == selected.Date &&
                 t.Currency == selected.Currency &&
                 t.IsIncome == selected.IsIncome
            );
            TransactionStorage.Save(ledgerTransactions);

            UpdateBalanceAndView();
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionList.Items.Count == 0)
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
                UpdateBalanceAndView();
            }
        }
        private void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow hw = new HistoryWindow(
                TransactionStorage.Load()
            );
            hw.Owner = this;
            hw.ShowDialog();
            ReloadFromLedger();
        }
        private void ReloadFromLedger()
        {
            ledgerTransactions = TransactionStorage.Load();

            sessionTransactions = ledgerTransactions
                .Select(t => t.Clone())
                .ToList();

            UpdateBalanceAndView();
        }


    }
}
