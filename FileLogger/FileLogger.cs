using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger
{
    public class FileLogger
    {
        public static string DateSuffixFormat => "yyyyMMdd";

        public void Log(string message, DateTime logDate)
        {
            string filename;

            if (!IsWeekend(logDate))
            {
                filename = $"log{logDate.ToString(DateSuffixFormat)}.txt";
                using StreamWriter fileWeekDay = new StreamWriter(filename, true);

                fileWeekDay.WriteLine(message);

                return;
            }
                
            
            filename = "weekend.txt";

            if (File.Exists(filename))
            {
                var suffixWeekendFile = IsSaturday(logDate) ? $"weekend-{logDate.ToString(DateSuffixFormat)}.txt" : $"weekend-{logDate.AddDays(-1).ToString(DateSuffixFormat)}.txt";
                File.Move(filename, suffixWeekendFile,true);
            }

            using StreamWriter file = new StreamWriter(filename, true);

            file.WriteLine(message);
        }

        private static bool IsWeekend(DateTime logDate) => logDate.DayOfWeek == DayOfWeek.Saturday || logDate.DayOfWeek == DayOfWeek.Sunday;
        private static bool IsSaturday(DateTime logDate) => logDate.DayOfWeek == DayOfWeek.Saturday;
    }
}
