namespace WindowsBatteryReporter
{
    using System;
    using System.Windows.Forms;

    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class MainForm : Form, IMainFormView
    {
        private readonly ILogger logger;
        private readonly IBatteryService batteryService;

        /// <summary>
        ///     Creates a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm(ILogger<MainForm> logger, IBatteryService batteryService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.batteryService = batteryService ?? throw new ArgumentNullException(nameof(batteryService));

            this.InitializeComponent();
        }

        public void SetCreateReportButtonEnabled(bool enable)
        {
            this.buttonCreateReport.Enabled = enable;
        }

        /// <summary>
        ///     Handles the button click event for creating a battery report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void buttonCreateReport_Click(object sender, EventArgs e)
        {
            this.logger.LogInformation("Create report button clicked.");

            this.SetCreateReportButtonEnabled(false);
            await this.batteryService.CreateBatteryReportAsync();
            this.SetCreateReportButtonEnabled(true);
        }
    }
}
