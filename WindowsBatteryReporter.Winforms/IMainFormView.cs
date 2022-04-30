namespace WindowsBatteryReporter
{
    /// <summary>
    ///     Represents UI controls on the <see cref="MainForm"/>.
    /// </summary>
    public interface IMainFormView
    {
        /// <summary>
        ///     Gets and sets enabling the create report button.
        /// </summary>
        bool CreateReportButtonEnabled { get; set; }
    }
}
