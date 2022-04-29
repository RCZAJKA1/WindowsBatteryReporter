namespace WindowsBatteryReporter
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <inheritdoc cref="IBatteryService"/>
    internal sealed class BatteryService : IBatteryService
    {
        private const string C_PATH = "C:\\BatteryReports";

        /// <inheritdoc/>
        public async Task CreateBatteryReportAsync()
        {
            DateTime now = DateTime.Now;
            string filePath = $"{C_PATH}\\battery-report-{now.Month}{now.Day}{now.Year}_{now.Hour}{now.Minute}{now.Second}.html";
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
