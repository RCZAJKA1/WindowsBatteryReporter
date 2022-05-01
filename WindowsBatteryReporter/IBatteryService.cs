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
        void CreateBatteryReport(string folderPath);
    }
}
