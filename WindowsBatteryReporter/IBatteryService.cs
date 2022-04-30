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
        void CreateBatteryReport();
    }
}
