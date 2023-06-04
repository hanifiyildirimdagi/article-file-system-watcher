namespace Worker;

public sealed class ListenOptions
{
    public ListenOptions(string path)
    {
        Path = path;
    }

    public string Path { get; set; }
    public string? Filter { get; set; }
}