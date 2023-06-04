using System.Threading.Channels;

namespace Worker;

public sealed class Queue
{
    private readonly Channel<FileSystemEventArgs> _channel;

    public Queue()
    {
        _channel = Channel.CreateBounded<FileSystemEventArgs>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true
        });
    }
    
    public async Task Produce(FileSystemEventArgs @event)
    {
        await _channel.Writer.WriteAsync(@event);
    }

    public async ValueTask<FileSystemEventArgs> Consume(CancellationToken cancellationToken)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}