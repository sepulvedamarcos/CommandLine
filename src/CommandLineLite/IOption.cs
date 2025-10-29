namespace CommandLineLite;

/// <summary>
/// Interfaz para representar una opción de línea de comandos.
/// </summary>
public interface IOption
{
    string Name { get; }
    string? Alias { get; }
    bool IsRequired { get; }
}
