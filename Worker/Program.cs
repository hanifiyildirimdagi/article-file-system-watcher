using Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
    {
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<Queue>();
        services.AddSingleton<Listener>();
        services.AddHostedService<Worker.Worker>();
    })
    .Build();

host.Run();