namespace WindowsBatteryReporter
{
    using System;
    using System.Windows.Forms;

    using Microsoft.Extensions.Logging;

    using WindowsBatteryReporter.Winforms;

    using static System.Windows.Forms.ListBox;

    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class MainForm : Form, IMainFormView
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     The main form controller.
        /// </summary>
        private readonly IMainFormController _mainFormController;

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

        /// <inheritdoc/>
        public bool CreateReportButtonEnabled
        {
            get => this.buttonCreateReport.Enabled;
            set => this.buttonCreateReport.Enabled = value;
        }

        /// <inheritdoc/>
        public ObjectCollection ReportPaths
        {
            get => this.listBoxMain.Items;
            set => this.listBoxMain.Items.Add(value);
        }

        /// <inheritdoc/>
        public string StatusLabel
        {
            get => this.statusLabelProgress.Text;
            set => this.statusLabelProgress.Text = value;
        }

        /// <summary>
        ///     Handles the button click event for creating a battery report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            this._logger.LogInformation("Create report button clicked.");

            using FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.StatusLabel = "Creating battery report...";

                string reportPath = this._mainFormController.CreateBatteryReport(folderBrowserDialog.SelectedPath);

                if (reportPath != null)
                {
                    this.StatusLabel = $"Report created: {reportPath}";
                    // TODO: fix
                    this.ReportPaths.Add(reportPath);
                }
                else
                {
                    this._logger.LogError("Failed to create battery report.");
                    this.StatusLabel = "Failed to create battery report.";
                }
            }
        }

        /// <summary>
        ///     Exits the application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this._logger.LogInformation("Exit button clicked.");

            // Subsequently prompts user to confirm exit upon the Form.OnFormClosing event
            this.Close();
        }

        /// <inheritdoc/>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (confirm == DialogResult.OK)
                {
                    this._logger.LogInformation("Exit application OK button clicked.");

                    base.OnClosing(e);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        ///     Opens the selected battery report file.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void listBoxMain_DoubleClick(object sender, EventArgs e)
        {
            string filePath = this.listBoxMain.SelectedItem.ToString();

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                this._mainFormController.OpenBatteryReport(filePath);
            }
        }
    }
}
