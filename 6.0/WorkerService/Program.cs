using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddSingleton<JokeService>();
		services.AddHostedService<WindowsBackgroundService>();
	})
	.Build();

await host.RunAsync();
