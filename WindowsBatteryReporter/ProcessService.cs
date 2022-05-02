namespace WindowsBatteryReporter
{
    using System;
    using System.Diagnostics;

    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="IProcessService"/>
    public sealed class ProcessService : IProcessService
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<ProcessService> _logger;

        /// <summary>
        ///     Creates a new instance of the <see cref="ProcessService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Throws if any injected dependencies are null.</exception>
        public ProcessService(ILogger<ProcessService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void CreateFileUsingCmd(string filePath)
        {
            this._logger.LogInformation($" file {filePath}.");

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(filePath));
            }

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

            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Command failed to create file {filePath}.", ex.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        public void OpenFileUsingCmd(string filePath)
        {
            this._logger.LogInformation($"Opening file {filePath}.");

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(filePath));
            }

            string command = $"start \"{filePath}\"";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            Process process = new Process
            {
                StartInfo = processStartInfo
            };

            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Command failed to open {filePath}.", ex.Message);
                throw;
            }
        }
    }
}
