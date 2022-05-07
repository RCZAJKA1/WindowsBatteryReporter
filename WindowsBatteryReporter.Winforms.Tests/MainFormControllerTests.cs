namespace WindowsBatteryReporter.Winforms.Tests
{
    using System;

    using Microsoft.Extensions.Logging;

    using Moq;

    using WindowsBatteryReporter.Common;

    using Xunit;

    public sealed class MainFormControllerTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<ILogger<MainFormController>> _mockLogger;
        private readonly Mock<IMainFormView> _mockMainFormView;
        private readonly Mock<IBatteryService> _mockBatteryService;

        public MainFormControllerTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._mockLogger = this._mockRepository.Create<ILogger<MainFormController>>();
            this._mockMainFormView = this._mockRepository.Create<IMainFormView>();
            this._mockBatteryService = this._mockRepository.Create<IBatteryService>();
        }

        [Fact]
        public void Ctor_LoggerNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new MainFormController(null, this._mockMainFormView.Object, this._mockBatteryService.Object));
            Assert.Equal("Value cannot be null. (Parameter 'logger')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_MainFormViewNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new MainFormController(this._mockLogger.Object, null, this._mockBatteryService.Object));
            Assert.Equal("Value cannot be null. (Parameter 'mainFormView')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_BatteryServiceNull_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new MainFormController(this._mockLogger.Object, this._mockMainFormView.Object, null));
            Assert.Equal("Value cannot be null. (Parameter 'batteryService')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void Ctor_ValidParams_CreatesInstance()
        {
            MainFormController controller = this.CreateMainFormController();

            Assert.NotNull(controller);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatteryReport_FolderPathNull_Throws()
        {
            string folderPath = null;

            this._mockLogger.SetupInformationLogging($"MainFormController.CreateBatteryReport({folderPath}).");

            MainFormController controller = this.CreateMainFormController();

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => controller.CreateBatteryReport(folderPath));

            Assert.Equal("Value cannot be null. (Parameter 'folderPath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatteryReport_FolderPathEmpty_Throws()
        {
            string folderPath = string.Empty;

            this._mockLogger.SetupInformationLogging($"MainFormController.CreateBatteryReport({folderPath}).");

            MainFormController controller = this.CreateMainFormController();

            ArgumentException exception = Assert.Throws<ArgumentException>(() => controller.CreateBatteryReport(folderPath));

            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'folderPath')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatteryReport_CreatesBatteryReport_ReturnsReportPath()
        {
            string folderPath = "test/FolderPath/";
            string reportPath = $"{folderPath}fileName.html";

            this._mockLogger.SetupInformationLogging($"MainFormController.CreateBatteryReport({folderPath}).");
            this._mockMainFormView.SetupSet(x => x.CreateReportButtonEnabled = false);
            this._mockBatteryService.Setup(x => x.CreateBatteryReport(It.Is<string>(y => y == folderPath))).Returns(reportPath);
            this._mockMainFormView.SetupSet(x => x.CreateReportButtonEnabled = true);

            MainFormController controller = this.CreateMainFormController();

            string result = controller.CreateBatteryReport(folderPath);

            Assert.NotNull(result);
            Assert.Equal(reportPath, result);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateBatteryReport_BatteryServiceThrows_ReturnsNull()
        {
            string folderPath = "test/FolderPath/";

            this._mockLogger.SetupInformationLogging($"MainFormController.CreateBatteryReport({folderPath}).");
            this._mockMainFormView.SetupSet(x => x.CreateReportButtonEnabled = false);
            this._mockBatteryService.Setup(x => x.CreateBatteryReport(It.Is<string>(y => y == folderPath))).Throws(() => new InvalidOperationException("testException"));
            this._mockMainFormView.SetupSet(x => x.StatusLabel = "Failed to create battery report.");
            this._mockMainFormView.SetupSet(x => x.CreateReportButtonEnabled = true);

            MainFormController controller = this.CreateMainFormController();

            string result = controller.CreateBatteryReport(folderPath);

            Assert.Null(result);

            this._mockRepository.VerifyAll();
        }

        private MainFormController CreateMainFormController()
        {
            return new MainFormController(
                this._mockLogger.Object,
                this._mockMainFormView.Object,
                this._mockBatteryService.Object);
        }
    }
}