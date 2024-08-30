using App.WindowsService;
using NLog.Extensions.Logging;

//var logger = LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "WorkerService";
    })
    .ConfigureServices(services =>
	{
        //LoggerProviderOptions.RegisterProviderOptions<
        //    EventLogSettings, EventLogLoggerProvider>(services);

        //services.AddSingleton<JokeService>();
		services.AddHostedService<WindowsBackgroundService>();
    })
    .ConfigureLogging((context, logging) =>
    {
        // See: https://github.com/dotnet/runtime/issues/47303
        logging.AddConfiguration(
            context.Configuration.GetSection("Logging"));

        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.None);
        logging.AddNLog();
    })
	.Build();

await host.RunAsync();
