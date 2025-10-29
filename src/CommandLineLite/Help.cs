namespace CommandLineLite;

/// <summary>
/// Clase para imprimir la ayuda de la aplicación.
/// </summary>
public static class Help
{
    public static void Print(Command command, string appName = "aplicacion")
    {
        Console.WriteLine($"Uso: {appName} [opciones]");
        Console.WriteLine(command.Description);
        Console.WriteLine("\nOpciones:");

        foreach (var option in command.GetOptions())
        {
            dynamic opt = option;
            if (opt.Description is string description)
                Console.WriteLine($"  {opt.Name.PadRight(20)}{description}");
            else
                Console.WriteLine($"  {option.Name.PadRight(20)}(sin descripción)");
        }

        Console.WriteLine($"  {"--help, -h".PadRight(20)}Muestra esta ayuda");
        Console.WriteLine($"  {"--version, -v".PadRight(20)}Muestra la versión de la aplicación");
    }
}
