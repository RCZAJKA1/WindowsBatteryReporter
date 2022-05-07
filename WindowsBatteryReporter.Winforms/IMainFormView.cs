namespace WindowsBatteryReporter
{
    using System.Collections.Generic;
    using static System.Windows.Forms.ListBox;

    /// <summary>
    ///     Represents UI controls on the <see cref="MainForm"/>.
    /// </summary>
    public interface IMainFormView
    {
        /// <summary>
        ///     Gets and sets enabling the create report button.
        /// </summary>
        bool CreateReportButtonEnabled { get; set; }

        /// <summary>
        ///     Gets and sets the report paths.
        /// </summary>
        ObjectCollection ReportPaths { get; set; }

        /// <summary>
        ///     Gets and sets the status label.
        /// </summary>
        string StatusLabel { get; set; }
    }
}
