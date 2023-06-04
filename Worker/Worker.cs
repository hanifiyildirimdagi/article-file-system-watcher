namespace Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Queue _queue;
    private readonly Listener _listener;

    public Worker(ILogger<Worker> logger, Queue queue, Listener listener)
    {
        _logger = logger;
        _queue = queue;
        _listener = listener;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _listener.Start();
        while (!stoppingToken.IsCancellationRequested)
        {
            var @event = await _queue.Consume(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            _logger.LogInformation("[{date}] {fileName} arrived to worker.",$"{DateTime.Now:O}",@event.Name);
        }
    }
}