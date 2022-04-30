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
        void CreateBatteryReport();

        ///// <summary>
        /////     Enables or disables the create report button.
        ///// </summary>
        ///// <param name="enable">The bool that determines if the button is enabled or disabled.</param>
        //public void SetCreateReportButtonEnabled(bool enable);
    }
}
