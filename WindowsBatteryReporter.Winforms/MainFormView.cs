namespace WindowsBatteryReporter
{
    using static System.Windows.Forms.ListBox;

    /// <inheritdoc cref="IMainFormView"/>
    internal sealed class MainFormView : IMainFormView
    {
        /// <inheritdoc/>
        public bool CreateReportButtonEnabled { get; set; }

        /// <inheritdoc/>
        public ObjectCollection ReportPaths { get; set; }

        /// <inheritdoc/>
        public string StatusLabel { get; set; }
    }
}
