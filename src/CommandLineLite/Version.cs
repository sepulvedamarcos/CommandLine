using Microsoft.Extensions.Configuration;

namespace CommandLineLite;

/// <summary>
/// Clase para imprimir la versión de la aplicación.
/// </summary>
public static class VersionInfo
{
    public static void Print()
    {
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string? version = configuration.GetValue<string>("Version");

            Console.WriteLine(!string.IsNullOrEmpty(version)
                ? $"Versión: {version}"
                : "No se encontró información de la versión.");
        }
        catch
        {
            Console.WriteLine("No se pudo leer la versión desde la configuración.");
        }
    }
}
