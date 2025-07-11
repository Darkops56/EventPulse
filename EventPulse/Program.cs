using EventPulse;
var eventos = new List<Evento>();
bool salir = false;


Console.Clear();
while (!salir)
{
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("=== Gestión de EventPulse ===");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("1. Crear Evento");
    Console.WriteLine("2. Listar Eventos");
    Console.WriteLine("3. Eliminar Evento");
    Console.WriteLine("4. Añadir Orador a Evento");
    Console.WriteLine("5. Añadir Asistente a Evento");
    Console.WriteLine("6. Listar Asistentes");
    Console.WriteLine("7. Listar Oradores");
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
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Evento creado correctamente.");
                Console.ResetColor();
                break;

            case "2":
                Console.Clear();
                if (eventos.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("No hay eventos disponibles.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                eventos.ForEach(e => Console.WriteLine(e));
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                break;

            case "3":
                Console.Clear();
                if (eventos.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("No hay eventos para eliminar.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Eventos disponibles:");
                eventos.ForEach(e => Console.WriteLine($"- {e.Nombre}"));
                Console.Write("Nombre del evento a eliminar: ");
                var nombre = Console.ReadLine();
                var eliminados = eventos.RemoveAll(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                if (eliminados > 0)
                    Console.WriteLine("Evento(s) eliminado(s).");
                else
                    Console.WriteLine("No se encontró ningún evento con ese nombre.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                break;

            case "4":
                Console.Clear();
                eventos.ForEach(e => Console.WriteLine(e));
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Nombre del evento: ");
                var nombreOr = Console.ReadLine();

                var ev = eventos.Find(e => e.Nombre.Equals(nombreOr, StringComparison.OrdinalIgnoreCase));
                if (ev != null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    var orador = Orador.CrearInteractivo();

                    ev.AgregarOrador(orador);
                    Console.WriteLine("Orador añadido.");
                    Console.ResetColor();
                }
                else Console.WriteLine("Evento no encontrado.");
                Console.ResetColor();
                Console.Clear();
                break;

            case "5":
                Console.Clear();
                eventos.ForEach(e => Console.WriteLine(e));
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Nombre del evento: ");
                var nombreAs = Console.ReadLine();

                ev = eventos.Find(e => e.Nombre.Equals(nombreAs, StringComparison.OrdinalIgnoreCase));
                if (ev != null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    var asistente = Asistentes.CrearInteractivo();
                    try
                    {
                        ev.RegistrarAsistente(asistente);
                        
                    }
                    catch (System.Exception)
                    {
                        
                        throw new Exception("No se pudo crear el asistente.");
                    }
                    Console.WriteLine("Asistente registrado.");
                    Console.ResetColor();
                    Console.ReadKey();

                }
                else Console.WriteLine("Evento no encontrado.");
                Console.ResetColor();
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
                        var tieneOradores = typeof(Evento)
                            .GetField("_asistentes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                            .GetValue(item) as List<Asistentes>;
                        if (tieneOradores != null && tieneOradores.Count > 0)
                        {
                            item.ListarAsistentes();
                            hayAsistentes = true;
                            Console.WriteLine();
                        }
                    }

                    if (!hayAsistentes)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("No hay asistentes registrados en ningún evento.");
                        Console.ResetColor();
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                break;
            case "7":
                Console.Clear();

                if (eventos.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("No hay eventos registrados.");
                    Console.ResetColor();
                }
                else
                {
                    bool hayOradores = false;
                    foreach (var item in eventos)
                    {
                        var tieneOradores = typeof(Evento)
                            .GetField("_oradores", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                            .GetValue(item) as List<Orador>;

                        if (tieneOradores != null && tieneOradores.Count > 0)
                        {
                            item.ListarOradores();
                            hayOradores = true;
                            Console.WriteLine();
                        }
                    }

                    if (!hayOradores)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("No hay oradores registrados en ningún evento.");
                        Console.ResetColor();
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                break;
            case "8":
                Console.Clear();
                salir = true;
                break;

            default:
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Opción inválida.");
                Console.ResetColor();
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
