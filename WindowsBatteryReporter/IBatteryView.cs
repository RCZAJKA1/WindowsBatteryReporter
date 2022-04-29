namespace WindowsBatteryReporter
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBatteryView
    {
        void SetCreateReportButtonEnabled(bool enable);
    }
}
