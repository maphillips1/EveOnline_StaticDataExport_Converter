using EveStaticDataExportConverter.Classes.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter
{
    static internal class Utility
    {
        public static void LogElapsedTime(Stopwatch stopwatch, string messageStart)
        {
            string formattedString = string.Format("{2} Took {4} hours {0} minutes, {1} seconds {3} milliseconds.", 
                                                    stopwatch.Elapsed.Minutes, 
                                                    stopwatch.Elapsed.Seconds,
                                                    messageStart,
                                                    stopwatch.Elapsed.Milliseconds,
                                                    stopwatch.Elapsed.Hours);
            Console.WriteLine(formattedString);
        }

        public static void InsertBatchRecord<T>(TableInfo tableInfo, List<T> batch)
        {
            if (batch?.Count > 0)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();
                DatabaseManager.InsertRecordsForListofTypes<T>(tableInfo, batch);
                stopwatch.Stop();
                batch.Clear();
            }
        }

        public static void AddRecordToBatch<T>(TableInfo tableInfo, ref List<T> batchRecords, T recordToAdd)
        {
            if (batchRecords != null)
            {
                batchRecords.Add(recordToAdd);
                if (batchRecords.Count >= 100)
                {
                    DatabaseManager.InsertRecordsForListofTypes<T>(tableInfo, batchRecords);
                    batchRecords.Clear();
                }
            }
        }
    }
}
