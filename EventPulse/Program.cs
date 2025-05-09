using EventPulse;
var eventos = new List<Evento>();
bool salir = false;

while (!salir)
{
    Console.WriteLine("=== Gestión de EventPulse ===");
    Console.WriteLine("1. Crear Evento");
    Console.WriteLine("2. Listar Eventos");
    Console.WriteLine("3. Eliminar Evento");
    Console.WriteLine("4. Añadir Orador a Evento");
    Console.WriteLine("5. Añadir Asistente a Evento");
    Console.WriteLine("6. Salir");
    Console.Write("Seleccione una opción: ");
    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                var nuevoEvento = Evento.CrearInteractivo();
                eventos.Add(nuevoEvento);
                Console.WriteLine("Evento creado correctamente.");
                break;

            case "2":
                if (eventos.Count == 0)
                {
                    Console.WriteLine("No hay eventos disponibles.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                }
                eventos.ForEach(e => Console.WriteLine(e));
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                break;

            case "3":
                if (eventos.Count == 0)
                {
                    Console.WriteLine("No hay eventos para eliminar.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                }
                Console.WriteLine("Eventos disponibles:");
                eventos.ForEach(e => Console.WriteLine($"- {e.Nombre}"));
                Console.Write("Nombre del evento a eliminar: ");
                var nombre = Console.ReadLine();
                var eliminados = eventos.RemoveAll(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                if (eliminados > 0)
                    Console.WriteLine("Evento(s) eliminado(s).");
                else
                    Console.WriteLine("No se encontró ningún evento con ese nombre.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                break;

            case "4":
                Console.Write("Nombre del evento: ");
                var nombreOr = Console.ReadLine();
                var ev = eventos.Find(e => e.Nombre.Equals(nombreOr, StringComparison.OrdinalIgnoreCase));
                if (ev != null)
                {
                    var orador = Orador.CrearInteractivo();
                    ev.AgregarOrador(orador);
                    Console.WriteLine("Orador añadido.");
                }
                else Console.WriteLine("Evento no encontrado.");
                break;

            case "5":
                Console.Write("Nombre del evento: ");
                var nombreAs = Console.ReadLine();
                ev = eventos.Find(e => e.Nombre.Equals(nombreAs, StringComparison.OrdinalIgnoreCase));
                if (ev != null)
                {
                    var asistente = Asistentes.CrearInteractivo();
                    ev.RegistrarAsistente(asistente);
                    Console.WriteLine("Asistente registrado.");
                }
                else Console.WriteLine("Evento no encontrado.");
                break;

            case "6":
                salir = true;
                break;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error de validación: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error inesperado: {ex.Message}");
    }
}
