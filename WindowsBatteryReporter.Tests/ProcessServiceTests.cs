namespace WindowsBatteryReporter.Tests
{
    using System;

    using Microsoft.Extensions.Logging;

    using Moq;

    using WindowsBatteryReporter.Common;

    using Xunit;

    public sealed class ProcessServiceTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<ILogger<ProcessService>> _mockLogger;

        public ProcessServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._mockLogger = this._mockRepository.Create<ILogger<ProcessService>>();
        }

        [Fact]
        public void Ctor_LoggerNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ProcessService(null));
            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidParams_CreatesInstance()
        {
            ProcessService service = this.CreateService();
            Assert.NotNull(service);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateFileUsingCmd_FilePathNull_Throws()
        {
            string filePath = null;

            this._mockLogger.SetupLog(LogLevel.Information, 0, $"Creating file {filePath} via cmd.", null);

            ProcessService service = this.CreateService();

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.CreateFileUsingCmd(filePath));
            Assert.Equal("Value cannot be null. (Parameter 'filePath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateFileUsingCmd_FilePathEmpty_Throws()
        {
            string filePath = String.Empty;

            this._mockLogger.SetupLog(LogLevel.Information, 0, $"Creating file {filePath} via cmd.", null);

            ProcessService service = this.CreateService();

            ArgumentException exception = Assert.Throws<ArgumentException>(() => service.CreateFileUsingCmd(filePath));
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'filePath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateFileUsingCmd_FilePathWhiteSpace_Throws()
        {
            string filePath = " ";

            this._mockLogger.SetupLog(LogLevel.Information, 0, $"Creating file {filePath} via cmd.", null);

            ProcessService service = this.CreateService();

            ArgumentException exception = Assert.Throws<ArgumentException>(() => service.CreateFileUsingCmd(filePath));
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'filePath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        private ProcessService CreateService()
        {
            return new ProcessService(
                this._mockLogger.Object);
        }
    }
}
