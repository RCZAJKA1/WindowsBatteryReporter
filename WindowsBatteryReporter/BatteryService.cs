namespace WindowsBatteryReporter
{
    using System;
    using System.IO;

    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="IBatteryService"/>
    public sealed class BatteryService : IBatteryService
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<BatteryService> _logger;

        /// <summary>
        ///     The process service.
        /// </summary>
        private readonly IProcessService _processService;

        /// <summary>
        ///     Creates a new instance of the <see cref="BatteryService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Throws if any injected dependencies are null.</exception>
        public BatteryService(ILogger<BatteryService> logger, IProcessService processService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._processService = processService ?? throw new ArgumentNullException(nameof(processService));
        }

        /// <inheritdoc/>
        public string CreateBatteryReport(string folderPath)
        {
            this._logger.LogInformation("Creating battery report.");

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

            this._processService.CreateFileUsingCmd(filePath);

            return filePath;
        }
    }
}
