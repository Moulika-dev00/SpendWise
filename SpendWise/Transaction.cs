using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise
{
    internal class Transaction
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public string Currency { get; set; }


        public Transaction Clone()
        {
            return new Transaction
            {
                Amount = this.Amount,
                Description = this.Description,
                Date = this.Date,
                IsIncome = this.IsIncome,
                Currency = this.Currency
            };
        }

        public override string ToString()
        {
            string sign = IsIncome ? "Income" : "Expense";
            return $"{sign} | {Currency} {Amount} | {Description} | {Date:dd MMM yyyy HH:mm}";
        }
    }
}

