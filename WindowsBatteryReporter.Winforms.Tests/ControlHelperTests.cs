namespace WindowsBatteryReporter.Winforms.Tests
{
    using System;
    using System.Windows.Forms;

    using Moq;

    using Xunit;

    public sealed class ControlHelperTests
    {
        private readonly MockRepository _mockRepository;

        public ControlHelperTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void EnsureControlThreadSynchronization_ControlNull_Throws()
        {
            Control control = null;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => control.EnsureControlThreadSynchronization(null));
            Assert.Equal("Value cannot be null. (Parameter 'control')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void EnsureControlThreadSynchronization_ActionNull_Throws()
        {
            Control control = new Button()
            {
                Enabled = true
            };

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => control.EnsureControlThreadSynchronization(null));
            Assert.Equal("Value cannot be null. (Parameter 'action')", exception.Message);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void EnsureControlThreadSynchronization_InvokeNotRequired_InvokesAction()
        {
            Control control = new Button()
            {
                Enabled = true
            };
            Action action = () => control.Enabled = false;

            control.EnsureControlThreadSynchronization(action);
            Assert.False(control.Enabled);

            this._mockRepository.VerifyAll();
        }
    }
}
