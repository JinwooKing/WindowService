using App.WindowsService;
using NLog.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "WorkerService";
    })
    .ConfigureServices(services =>
	{
		services.AddHostedService<WindowsBackgroundService>();
    })
    .ConfigureLogging((context, logging) =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
        logging.AddNLog();
    })
	.Build();

await host.RunAsync();
