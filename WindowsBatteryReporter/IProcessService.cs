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
    }
}
