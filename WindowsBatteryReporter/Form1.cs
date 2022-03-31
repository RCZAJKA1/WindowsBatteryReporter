namespace WindowsBatteryReporter
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     The main form.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly ILogger logger;
        private readonly IBatteryService batteryService;

        /// <summary>
        ///     Creates a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1(ILogger<Form1> logger, IBatteryService batteryService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.batteryService = batteryService ?? throw new ArgumentNullException(nameof(batteryService));
            
            InitializeComponent();
        }

        /// <summary>
        ///     Handles the button click event for creating a battery report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonCreateReport_Click(object sender, EventArgs e)
        {
            this.buttonCreateReport.Enabled = false;
            this.batteryService.CreateBatteryReportAsync();
            this.buttonCreateReport.Enabled = true;
        }
    }
}
