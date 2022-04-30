namespace WindowsBatteryReporter.Winforms
{
    using System;

    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="IMainFormController">
    internal sealed class MainFormController : IMainFormController
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
        /// <param name="batteryService">The battery service.</param>
        public MainFormController(ILogger<MainFormController> logger, IMainFormView mainFormView, IBatteryService batteryService)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mainFormView = mainFormView ?? throw new ArgumentNullException(nameof(mainFormView));
            this._batteryService = batteryService ?? throw new ArgumentNullException(nameof(batteryService));
        }

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

            this._logger.LogInformation("MainFormController.CreateBatteryReport().");

            this._mainFormView.CreateReportButtonEnabled = false;
            this._batteryService.CreateBatteryReport(folderPath);
            this._mainFormView.CreateReportButtonEnabled = true;
        }

        //public void SetCreateReportButtonEnabled(bool enable)
        //{
        //    Action action = new Action(() => { this._mainFormView.CreateReportButtonEnabled = enable; });
        //    this.buttonCreateReport.EnsureControlThreadSynchronization(action);
        //}
    }
}
