namespace CommandLineLite;

/// <summary>
/// Clase abstracta base para opciones de línea de comandos.
/// </summary>
public abstract class OptionBase(string name, bool isRequired = false, string alias = "") : IOption
{
    public string Name { get; } = name;
    public string? Alias { get; } = alias;
    public bool IsRequired { get; } = isRequired;
}
