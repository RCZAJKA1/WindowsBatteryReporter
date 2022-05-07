namespace WindowsBatteryReporter.Winforms
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="IMainFormController">
    public sealed class MainFormController : IMainFormController
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     The main form view.
        /// </summary>
        private readonly IMainFormView _mainFormView;

        /// <summary>
        ///     The battery service.
        /// </summary>
        private readonly IBatteryService _batteryService;

        /// <summary>
        ///     Creates a new instance of the <see cref="MainFormController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mainFormView">The main form view.</param>
        /// <param name="batteryService">The battery service.</param>
        /// <exception cref="ArgumentNullException">Throws if any injected dependencies are null.</exception>
        public MainFormController(ILogger<MainFormController> logger, IMainFormView mainFormView, IBatteryService batteryService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mainFormView = mainFormView ?? throw new ArgumentNullException(nameof(mainFormView));
            this._batteryService = batteryService ?? throw new ArgumentNullException(nameof(batteryService));
        }

        /// <inheritdoc/>
        public string CreateBatteryReport(string folderPath)
        {
            this._logger.LogInformation($"MainFormController.CreateBatteryReport({folderPath}).");

            if (folderPath == null)
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(folderPath));
            }

            string reportPath = null;
            try
            {
                this._mainFormView.CreateReportButtonEnabled = false;
                reportPath = this._batteryService.CreateBatteryReport(folderPath);
            }
            catch
            {
                this._mainFormView.StatusLabel = "Failed to create battery report.";
            }
            finally
            {
                this._mainFormView.CreateReportButtonEnabled = true;
            }

            return reportPath;
        }

        /// <inheritdoc/>
        public void OpenBatteryReport(string filePath)
        {
            this._logger.LogInformation("Opening battery report.");

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(filePath));
            }

            // TODO: move to service
            if (File.Exists(filePath))
            {
                this._logger.LogInformation("File path exists. Running command.");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = filePath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            else
            {
                this._logger.LogInformation($"The file path does not exist. '{filePath}'");
            }
        }
    }
}
