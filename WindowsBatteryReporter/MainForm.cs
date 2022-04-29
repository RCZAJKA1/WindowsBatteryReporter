namespace WindowsBatteryReporter
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ILogger logger;
        private readonly IBatteryService batteryService;
        private readonly IBatteryView batteryView;

        /// <summary>
        ///     Creates a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm(ILogger<MainForm> logger, IBatteryService batteryService, IBatteryView batteryView)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.batteryService = batteryService ?? throw new ArgumentNullException(nameof(batteryService));
            this.batteryView = batteryView ?? throw new ArgumentNullException(nameof(batteryView));
            
            this.InitializeComponent();
        }

        /// <summary>
        ///     Handles the button click event for creating a battery report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void buttonCreateReport_Click(object sender, EventArgs e)
        {
            this.buttonCreateReport.Enabled = false;
            await this.batteryService.CreateBatteryReportAsync();
            this.buttonCreateReport.Enabled = true;
        }
    }
}
