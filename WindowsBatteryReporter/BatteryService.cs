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
            string fileName = $"battery-report-{now.Month}{now.Day}{now.Year}_{now.Hour}{now.Minute}{now.Second}.html";
            string filePath = Path.Combine(folderPath, fileName);
            string command = $"powercfg /batteryreport /output \"{filePath}\"";

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new Process
            {
                StartInfo = processStartInfo
            };

            process.Start();
            process.WaitForExit();
            //Process.Start(filePath);
        }
    }
}
