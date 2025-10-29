namespace CommandLineLite;

/// <summary>
/// Representa una opción genérica de línea de comandos.
/// </summary>
public class Option<T> : OptionBase
{
    public string Description { get; }
    public T? DefaultValue { get; }

    public Option(string name, string description, T? defaultValue = default, bool isRequired = false, string alias = "")
        : base(name, isRequired, alias)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la opción no puede estar vacío.", nameof(name));

        if (!name.StartsWith("--") && !name.StartsWith("-"))
            throw new ArgumentException("El nombre de la opción debe comenzar con '--' o '-'.", nameof(name));

        Description = description;
        DefaultValue = defaultValue;
    }
}
