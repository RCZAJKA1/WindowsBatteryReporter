namespace WindowsBatteryReporter
{
    using System;
    using System.Windows.Forms;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using WindowsBatteryReporter.Winforms;

    /// <summary>
    ///     The program main entry point.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        ///     Gets or sets the service provider.
        /// </summary>
        private static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IHost host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            MainForm mainForm = ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }

        /// <summary>
        ///     Creates the host builder.
        /// </summary>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services
                    .AddTransient<MainForm>()
                    .AddTransient<IBatteryService, BatteryService>()
                    .AddTransient<IMainFormView, MainFormView>()
                    .AddTransient<IMainFormController, MainFormController>()
                    .AddTransient<IProcessService, ProcessService>();
                });
        }
    }
}
