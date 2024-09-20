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
    }
}
