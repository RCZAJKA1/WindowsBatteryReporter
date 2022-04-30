﻿namespace WindowsBatteryReporter
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

        /// <summary>
        ///     Handles the button click event for creating a battery report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            this.logger.LogInformation("Create report button clicked.");

            this.SetCreateReportButtonEnabled(false);
            this.batteryService.CreateBatteryReport();
            this.SetCreateReportButtonEnabled(true);
        }

        /// <summary>
        ///     Enables or disables the create report button.
        /// </summary>
        /// <param name="enable">The bool that determines if the button is enabled or disabled.</param>
        public void SetCreateReportButtonEnabled(bool enable)
        {
            Action action = new Action(() => { this.buttonCreateReport.Enabled = enable; });
            this.buttonCreateReport.EnsureControlThreadSynchronization(action);
        }
    }
}
