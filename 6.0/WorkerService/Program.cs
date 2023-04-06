using NLog;
using NLog.Extensions.Logging;
using WorkerService;
using WorkerService.Model.Helper;

//var logger = LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
        services.AddSingleton<JokeService>();
		services.AddHostedService<WindowsBackgroundService>();
    })
	.ConfigureLogging(logging =>
	{
        //logging.ClearProviders();
        //logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        logging.AddNLog();
    })
	.Build();

await host.RunAsync();
