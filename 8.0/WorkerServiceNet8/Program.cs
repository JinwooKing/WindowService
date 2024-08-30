using WorkerServiceNet8;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "WorkerService";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();
//var builder = Host.CreateApplicationBuilder(args);
//
//builder.Services.AddHostedService<Worker>();

//var host = builder.Build();

await host.RunAsync();