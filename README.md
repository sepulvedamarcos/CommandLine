# CommandLineLite

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
![Made with C#](https://img.shields.io/badge/Made%20with-C%23-239120.svg?logo=csharp)
[![Platform](https://img.shields.io/badge/Platform-Linux%20|%20Windows-orange.svg)](https://dotnet.microsoft.com/)

---

**CommandLineLite** es una **librería ligera, intuitiva y sin dependencias innecesarias** para manejar argumentos de línea de comandos en aplicaciones .NET.  
Ideal para herramientas de consola donde quieres simplicidad, claridad y cero magia detrás del código.

> ✨ Diseñada para desarrolladores que prefieren escribir código limpio y tener control total del flujo.

---

## 🚀 Características

- ✅ Sintaxis clara y minimalista  
- 🧩 Definición tipada de opciones (`Option<T>`)  
- 🔍 Soporte para alias (`-h`, `--help`)  
- 📘 Generación automática de ayuda (`Help.Print()`)  
- 🧱 Integración opcional con `appsettings.json` para versión  
- ⚙️ Compatible con `.NET 8` y `.NET 9`

---

## 📦 Instalación

Desde **NuGet** (próximamente):

```bash
dotnet add package CommandLineLite
```

O simplemente copia los archivos fuente si prefieres mantenerlo interno.

## 🧪 Ejemplo completo de uso

El siguiente ejemplo está disponible en el directorio [`tests`](./tests), e ilustra cómo usar **CommandLineLite** para crear una aplicación de consola con parámetros, validación y ayuda automática.

### 1️⃣ Definir la clase de opciones

```csharp
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
```

lo puedes ejecutar por ejemplo con:

```csharp
dotnet run -- --nombre Marcos --edad 35 --verbose
```

```csharp
dotnet run -- --help

Uso: aplicacion [opciones]
Ejemplo de uso de CommandLineLite

Opciones:
  --nombre, -n          Nombre del usuario
  --edad, -e            Edad del usuario
  --verbose, -v         Muestra mensajes detallados
  --help, -h            Muestra esta ayuda
  --version, -v         Muestra la versión de la aplicación
```


---

<div align="center">

**¿Te resultó útil CommandLineLite?**  
⭐ Dale una estrella al repositorio

**¿Quieres apoyar el desarrollo?**  
☕ [Invítame un café](https://ko-fi.com/sepulvedamarcos)

*Proyecto creado con fines educativos y de aprendizaje*

</div>