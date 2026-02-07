using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace SpendWise
{
    internal static class TransactionStorage
    {
        private static string filePath = "transactions.json";

        public static void Save(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(filePath, json);
        }

        public static List<Transaction> Load()
        {
            if (!File.Exists(filePath))
                return new List<Transaction>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json)
                   ?? new List<Transaction>();
        }
    }
}
