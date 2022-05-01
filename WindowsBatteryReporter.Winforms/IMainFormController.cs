namespace WindowsBatteryReporter.Winforms
{
    /// <summary>
    ///     Handles business logic upon invoking UI controls from the <see cref="MainForm"/>.
    /// </summary>
    public interface IMainFormController
    {
        /// <summary>
        ///     Creates a new battery report.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        void CreateBatteryReport(string folderPath);

        ///// <summary>
        /////     Enables or disables the create report button.
        ///// </summary>
        ///// <param name="enable">The bool that determines if the button is enabled or disabled.</param>
        //public void SetCreateReportButtonEnabled(bool enable);
    }
}
