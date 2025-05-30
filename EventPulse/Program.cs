using EventPulse;
var eventos = new List<Evento>();
bool salir = false;


Console.Clear();
while (!salir)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("=== Gestión de EventPulse ===");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("1. Crear Evento");
    Console.WriteLine("2. Listar Eventos");
    Console.WriteLine("3. Eliminar Evento");
    Console.WriteLine("4. Añadir Orador a Evento");
    Console.WriteLine("5. Añadir Asistente a Evento");
    Console.WriteLine("6. Listar Oradores");
    Console.WriteLine("7. Listar Asistentes");
    Console.WriteLine("8. salir");
    Console.Write("Seleccione una opción: ");
    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Console.Clear();
                var nuevoEvento = Evento.CrearInteractivo();
                eventos.Add(nuevoEvento);
                Console.WriteLine("Evento creado correctamente.");
                break;

            case "2":
                Console.Clear();
                if (eventos.Count == 0)
                {
                    Console.WriteLine("No hay eventos disponibles.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                eventos.ForEach(e => Console.WriteLine(e));
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                break;

            case "3":
                Console.Clear();
                if (eventos.Count == 0)
                {
                    Console.WriteLine("No hay eventos para eliminar.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
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
                Console.Clear();
                break;

            case "4":
                Console.Clear();
                eventos.ForEach(e => Console.WriteLine(e));
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
                Console.Clear();
                break;

            case "5":
                Console.Clear();
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
                Console.Clear();
                break;
            case "6":
                Console.Clear();
                if (eventos.Count == 0)
                {
                    Console.WriteLine("No hay eventos registrados.");
                }
                else
                {
                    bool hayAsistentes = false;
                    foreach (var item in eventos)
                    {
                        if (item.Asistentes != null && item.Asistentes.Count > 0)
                        {
                            item.ListarAsistentes();
                            hayAsistentes = true;
                            Console.WriteLine(); // Espacio entre eventos
                        }
                    }

                    if (!hayAsistentes)
                    {
                        Console.WriteLine("No hay asistentes registrados en ningún evento.");
                    }
                }
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                break;
            case "7":
                Console.Clear();

                if (eventos.Count == 0)
                {
                    Console.WriteLine("No hay eventos registrados.");
                }
                else
                {
                    bool hayOradores = false;
                    foreach (var item in eventos)
                    {
                        var tieneOradores = typeof(EventPulse.Evento)
                            .GetField("_oradores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                            .GetValue(item) as List<Orador>;

                        if (tieneOradores != null && tieneOradores.Count > 0)
                        {
                            item.ListarOradores();
                            hayOradores = true;
                            Console.WriteLine(); // Espacio entre eventos
                        }
                    }

                    if (!hayOradores)
                    {
                        Console.WriteLine("No hay oradores registrados en ningún evento.");
                    }
                }
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                break;
            case "8":
                Console.Clear();
                salir = true;
                break;

            default:
                Console.WriteLine("Opción inválida.");
                Console.Clear();
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
