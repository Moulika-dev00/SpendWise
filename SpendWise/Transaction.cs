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
        public decimal OriginalAmount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public string Currency { get; set; }
        public string Category { get; set; }


        public Transaction Clone()
        {
            return new Transaction
            {
                Amount = this.Amount,
                OriginalAmount = this.OriginalAmount,
                Description = this.Description,
                Date = this.Date,
                IsIncome = this.IsIncome,
                Currency = this.Currency,
                Category = this. Category,
            };
        }

        public override string ToString()
        {
            bool isToday = Date.Date == DateTime.Now.Date;

            string datePart = isToday
                ? Date.ToString("dd MMM HH:mm")   
                : Date.ToString("dd MMM yyyy");

            return $"{(IsIncome ? "+" : "-")} {Currency} {OriginalAmount} | {Category} | {Description} | {datePart}";
        }
    }
}

