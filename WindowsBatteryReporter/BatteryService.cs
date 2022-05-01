namespace WindowsBatteryReporter
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <inheritdoc cref="IBatteryService"/>
    public sealed class BatteryService : IBatteryService
    {
        /// <inheritdoc/>
        public void CreateBatteryReport(string folderPath)
        {
            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(folderPath));
            }

            DateTime now = DateTime.Now;

            string padZeroFormat = "{0:00}";

            string month = string.Format(padZeroFormat, now.Month);
            string day = string.Format(padZeroFormat, now.Day);
            string year = string.Format(padZeroFormat, now.Year);
            string hour = string.Format(padZeroFormat, now.Hour);
            string min = string.Format(padZeroFormat, now.Minute);
            string sec = string.Format(padZeroFormat, now.Second);

            string fileName = $"battery-report-{month}{day}{year}_{hour}{min}{sec}.html";
            string filePath = Path.Combine(folderPath, fileName);
            string command = $"powercfg /batteryreport /output \"{filePath}\"";

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = processStartInfo
            };

            process.Start();
            //Process.Start(filePath);
        }
    }
}
