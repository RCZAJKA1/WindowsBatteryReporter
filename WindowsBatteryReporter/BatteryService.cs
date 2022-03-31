namespace WindowsBatteryReporter
{
    using System;
    using System.Diagnostics;

    /// <inheritdoc cref="IBatteryService"/>
    internal sealed class BatteryService : IBatteryService
    {
        private readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <inheritdoc/>
        public void CreateBatteryReportAsync()
        {
            DateTime now = DateTime.Now;
            string filePath = $"{this.DesktopPath}\\battery-report-{now.Month}{now.Day}{now.Year}_{now.Hour}{now.Minute}.html";
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
