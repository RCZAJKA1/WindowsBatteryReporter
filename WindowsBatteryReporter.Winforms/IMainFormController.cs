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
        /// <returns>The file path of the battery report.</returns>
        string CreateBatteryReport(string folderPath);

        /// <summary>
        ///     Opens the specified battery report file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void OpenBatteryReport(string filePath);
    }
}
