using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SpendWise
{
    internal static class TransactionStorage
    {
        private static string ledgerPath = "ledger.json";   // HISTORY (never cleared)
        private static string activePath = "active.json";   // MAIN WINDOW

        // 🔹 HISTORY (permanent data)
        public static void SaveLedger(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(ledgerPath, json);
        }

        public static List<Transaction> LoadLedger()
        {
            if (!File.Exists(ledgerPath))
                return new List<Transaction>();

            var json = File.ReadAllText(ledgerPath);
            return JsonSerializer.Deserialize<List<Transaction>>(json)
                   ?? new List<Transaction>();
        }


        // 🔹 MAIN WINDOW DATA
        public static void SaveActive(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            File.WriteAllText(activePath, json);
        }

        public static List<Transaction> LoadActive()
        {
            if (!File.Exists(activePath))
                return new List<Transaction>();

            var json = File.ReadAllText(activePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json)
                   ?? new List<Transaction>();
        }
    }
}