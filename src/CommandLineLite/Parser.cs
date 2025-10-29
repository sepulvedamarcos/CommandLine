using System.ComponentModel;
using System.Reflection;

namespace CommandLineLite;

/// <summary>
/// Clase para parsear argumentos de línea de comandos.
/// </summary>
public static class Parser
{
    public static T Parse<T>(Command command, string[] args) where T : new()
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Uso: [aplicación] [opciones]");
            Console.WriteLine("Use --help para ver las opciones disponibles.");
            Environment.Exit(1);
        }

        if (args.Contains("--help") || args.Contains("-h"))
        {
            Help.Print(command);
            Environment.Exit(0);
        }

        if (args.Contains("--version") || args.Contains("-v"))
        {
            VersionInfo.Print();
            Environment.Exit(0);
        }

        var options = new T();
        var optionMapping = new Dictionary<string, PropertyInfo>();
        var commandOptions = command.GetOptions().ToList();

        foreach (var option in commandOptions)
        {
            var propInfo = typeof(T).GetProperty(option.Name.TrimStart('-'), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo != null)
            {
                optionMapping[option.Name.ToLower()] = propInfo;
                if (!string.IsNullOrEmpty(option.Alias))
                    optionMapping[option.Alias.ToLower()] = propInfo;
            }
        }

        var mandatoryOptions = commandOptions.Where(o => o.IsRequired).ToDictionary(o => o.Name, o => false);

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i].ToLower();
            if (optionMapping.TryGetValue(arg, out var prop))
            {
                if (i + 1 >= args.Length)
                {
                    Console.WriteLine($"Error: Falta el valor para la opción '{arg}'.");
                    Environment.Exit(1);
                }

                var valueString = args[++i];
                try
                {
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    var value = converter.ConvertFromString(valueString);
                    prop.SetValue(options, value);
                    mandatoryOptions[optionMapping.First(kv => kv.Value == prop).Key] = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo asignar el valor '{valueString}' a la opción '{arg}': {ex.Message}");
                    Environment.Exit(1);
                }
            }
        }

        foreach (var entry in mandatoryOptions)
        {
            if (!entry.Value)
            {
                Console.WriteLine($"Error: La opción obligatoria '{entry.Key}' no ha sido especificada.");
                Environment.Exit(1);
            }
        }

        // Si el tipo implementa IValidatable, ejecutar su validación
        if (options is IValidatable validatable)
            validatable.Validate();

        return options;
    }
}
