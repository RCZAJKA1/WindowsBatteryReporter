namespace WindowsBatteryReporter
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles operations for battery reports.
    /// </summary>
    public interface IBatteryService
    {
        /// <summary>
        ///     Creates a new battery report.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed creating the battery report.</returns>
        Task CreateBatteryReportAsync();
    }
}
