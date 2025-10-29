using CommandLineLite;

public class Opciones : IValidatable
{
    public string Nombre { get; set; } = "";
    public int Edad { get; set; } = 0;
    public bool Verbose { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            throw new ArgumentException("Debe especificar el nombre con --nombre o -n");
        if (Edad <= 0)
            throw new ArgumentException("La edad debe ser mayor a 0");
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        // Definir el comando principal
        var command = new Command("Ejemplo de uso de CommandLineLite");

        // Registrar las opciones que el programa acepta
        command.AddOption(new Option<string>(
            "--nombre",
            "Nombre del usuario",
            isRequired: true,
            alias: "-n"
        ));
        command.AddOption(new Option<int>(
            "--edad",
            "Edad del usuario",
            defaultValue: 18,
            alias: "-e")
        );
        command.AddOption(new Option<bool>(
            "--verbose",
            "Muestra mensajes detallados",
            alias: "-v"))
        ;

        // Parsear los argumentos en la clase de opciones
        var opciones = Parser.Parse<Opciones>(command, args);

        // Lógica del programa
        Console.WriteLine($"Hola {opciones.Nombre}, tienes {opciones.Edad} años.");
        if (opciones.Verbose)
            Console.WriteLine("Modo detallado activado.");
    }
}
