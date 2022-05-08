namespace WindowsBatteryReporter
{
    /// <summary>
    ///     Handles operations against system processes.
    /// </summary>
    public interface IProcessService
    {
        /// <summary>
        ///     Creates the specified file using a command prompt process.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void CreateFileUsingCmd(string filePath);

        /// <summary>
        ///     Opens the specified file using a command prompt process.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void OpenFileUsingCmd(string filePath);

        /// <summary>
        ///     Starts a new process using the specified executable (e.g. 'explorer.exe') and arguments (e.g. 'C://temp').
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="arguments">The process arguments.</param>
        void StartProcess(string executable, string[] arguments);
    }
}
