namespace WindowsBatteryReporter
{
    /// <summary>
    ///     Handles operations for battery reports.
    /// </summary>
    public interface IBatteryService
    {
        /// <summary>
        ///     Creates a new battery report.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns>The file path of the battery report.</returns>
        string CreateBatteryReport(string folderPath);
    }
}
