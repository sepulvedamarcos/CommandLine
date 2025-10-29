namespace CommandLineLite;

/// <summary>
/// Representa un comando de línea de comandos.
/// </summary>
public class Command(string description)
{
    public string Description { get; } = description;
    private readonly List<IOption> _options = [];

    public void AddOption(IOption option)
    {
        if (_options.Any(o => o.Name == option.Name || (!string.IsNullOrEmpty(o.Alias) && o.Alias == option.Alias)))
            throw new ArgumentException($"La opción '{option.Name}' ya ha sido agregada o su alias ya existe.", nameof(option));

        _options.Add(option);
    }

    public IEnumerable<IOption> GetOptions() => _options;
}
