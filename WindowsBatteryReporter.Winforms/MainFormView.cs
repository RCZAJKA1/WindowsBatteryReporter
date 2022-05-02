namespace WindowsBatteryReporter
{
    using System.Collections.Generic;

    /// <inheritdoc cref="IMainFormView"/>
    internal sealed class MainFormView : IMainFormView
    {
        /// <inheritdoc/>
        public bool CreateReportButtonEnabled { get; set; }

        /// <inheritdoc/>
        public IList<string> ReportPaths { get; set; }

        /// <inheritdoc/>
        public string StatusLabel { get; set; }
    }
}
