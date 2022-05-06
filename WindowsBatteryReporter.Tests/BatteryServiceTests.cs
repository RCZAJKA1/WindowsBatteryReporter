namespace WindowsBatteryReporter.Tests
{
    using System;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class BatteryServiceTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<ILogger<BatteryService>> _mockLogger;
        private readonly Mock<IProcessService> _mockProcessService;

        public BatteryServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._mockLogger = this._mockRepository.Create<ILogger<BatteryService>>();
            this._mockProcessService = this._mockRepository.Create<IProcessService>();
        }

        [Fact]
        public void Ctor_LoggerNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new BatteryService(null, this._mockProcessService.Object));
            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ProcessServiceNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new BatteryService(this._mockLogger.Object, null));
            Assert.Equal("Value cannot be null. (Parameter 'processService')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidParams_CreatesInstance()
        {
            BatteryService batteryService = this.CreateService();
            Assert.NotNull(batteryService);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatterReport_FolderPathNull_Throws()
        {
            string folderPath = null;

            this._mockLogger.SetupLogger(LogLevel.Information, 0, "Creating battery report.", null);

            BatteryService batteryService = this.CreateService();

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => batteryService.CreateBatteryReport(folderPath));
            Assert.Equal("Value cannot be null. (Parameter 'folderPath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatterReport_FolderPathEmpty_Throws()
        {
            string folderPath = string.Empty;

            this._mockLogger.SetupLogger(LogLevel.Information, 0, "Creating battery report.", null);

            BatteryService batteryService = this.CreateService();

            ArgumentException exception = Assert.Throws<ArgumentException>(() => batteryService.CreateBatteryReport(folderPath));
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'folderPath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatterReport_FolderPathWhiteSpace_Throws()
        {
            string folderPath = " ";

            this._mockLogger.SetupLogger(LogLevel.Information, 0, "Creating battery report.", null);

            BatteryService batteryService = this.CreateService();

            ArgumentException exception = Assert.Throws<ArgumentException>(() => batteryService.CreateBatteryReport(folderPath));
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'folderPath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatterReport_CreatesFile_ReturnsFilePath()
        {
            string folderPath = "testFolder/Path/";

            this._mockLogger.SetupLogger(LogLevel.Information, 0, "Creating battery report.", null);

            this._mockProcessService.Setup(x => x.CreateFileUsingCmd(It.IsAny<string>()));

            BatteryService batteryService = this.CreateService();

            string result = batteryService.CreateBatteryReport(folderPath);
            Assert.NotNull(result);
            Assert.StartsWith($"{folderPath}battery-report-", result);
            Assert.EndsWith(".html", result);

            this._mockRepository.VerifyAll();
        }

        private BatteryService CreateService()
        {
            return new BatteryService(
                this._mockLogger.Object,
                this._mockProcessService.Object);
        }
    }
}
