namespace Worker;

public sealed class Listener
{
    private readonly FileSystemWatcher _watcher;
    private readonly ILogger<Listener> _logger;
    private readonly Queue _queue;

    private static string Name => nameof(Listener);

    public Listener(IConfiguration configuration, ILogger<Listener> logger, Queue queue)
    {
        _logger = logger;
        _queue = queue;
        var options = configuration.GetSection("ListenOptions").Get<ListenOptions>();
        ArgumentNullException.ThrowIfNull(options);
        _watcher = new FileSystemWatcher
        {
            Path = options.Path,
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime,
        };
        if (!string.IsNullOrEmpty(options.Filter)) _watcher.Filter = options.Filter;
        _watcher.Created += Created;
        _logger.LogDebug($"[{DateTime.Now:O}] {Name}: Instance created.");
    }

    public void Start() => _watcher.EnableRaisingEvents = true;

    private void Created(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation($"[{DateTime.Now:O}] {Name}: The file has been created. File : {e.Name}");
        _queue.Produce(e).Wait();
    }
}