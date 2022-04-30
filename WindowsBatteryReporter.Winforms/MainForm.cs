namespace WindowsBatteryReporter
{
    using System;
    using System.Windows.Forms;

    using Microsoft.Extensions.Logging;

    using WindowsBatteryReporter.Winforms;

    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class MainForm : Form, IMainFormView
    {
        private readonly ILogger _logger;
        private readonly IMainFormController _mainFormController;

        /// <inheritdoc/>
        public bool CreateReportButtonEnabled { get; set; }

        /// <summary>
        ///     Creates a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mainFormController">The main form controller.</param>
        /// <exception cref="ArgumentNullException">Throws if any injected dependencies are null.</exception>
        public MainForm(ILogger<MainForm> logger, IMainFormController mainFormController)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mainFormController = mainFormController ?? throw new ArgumentNullException(nameof(mainFormController));

            this.InitializeComponent();
        }

        /// <summary>
        ///     Handles the button click event for creating a battery report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            this._logger.LogInformation("Create report button clicked.");

            this._mainFormController.CreateBatteryReport();
        }
    }
}
