using Entidades;
using Inscripciones;
using Utilidades;
using Personas;
using System.Data;
namespace Gestores
{
    public static class GestorEventos
    {
        private static List<Evento> eventos = new();

        public static void CrearDesdeConsola()
        {
            Console.Write("Tipo de (Conferencia/Taller/Feria) o cualquier otro: ");
            var tipo = Console.ReadLine().Trim().ToLower();

            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            var descripcion = Console.ReadLine();

            Console.Write("Fecha inicio (yyyy-MM-dd): ");
            var inicio = DateTime.Parse(Console.ReadLine());
            Console.Write("Fecha fin (yyyy-MM-dd): ");
            var fin = DateTime.Parse(Console.ReadLine());

            Console.Write("Cliente: ");
            var cliente = Console.ReadLine();
            Console.Write("Presupuesto: ");
            var presupuesto = decimal.Parse(Console.ReadLine());

            Evento nuevoEvento = tipo switch
            {
                "conferencia" => new Conferencia(nombre, descripcion, inicio, fin, cliente, presupuesto),
                "taller" => new Taller(nombre, descripcion, inicio, fin, cliente, presupuesto),
                "feria" => new Feria(nombre, descripcion, inicio, fin, cliente, presupuesto),
                _ => new EventoComun(tipo, nombre, descripcion, inicio, fin, cliente, presupuesto)
            };

            eventos.Add(nuevoEvento);
            Console.WriteLine("Evento creado.");
            Console.ReadKey();
        }

        public static bool ListarDesdeConsola()
        {
            if (eventos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay eventos.");
                Console.ResetColor();
                Console.ReadKey();
                return false;
            }

            foreach (var e in eventos)
                Console.WriteLine(e);


            return true;
        }

        public static void EliminarDesdeConsola()
        {
            if (eventos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay eventos para que puedas eliminar.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            Console.Write("Nombre del evento: ");
            var nombre = Console.ReadLine();
            var evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (evento == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Evento no encontrado.");
                Console.ResetColor();
            }
            eventos.Remove(evento);
            Console.WriteLine("Evento eliminado.");
        }
        public static void RegistrarInscripcionDesdeConsola()
        {

            if (eventos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay eventos para que puedas eliminar.");
                Console.ReadKey();
                return;
            }
            foreach (var e in eventos)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"{e.Tipo}: {e.Nombre}, {e.Descripcion}, {e.FechaInicio}, {e.FechaFin}, {e.Cliente}, {e.Presupuesto}");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Nombre del evento: ");
            var nombreEvento = Console.ReadLine();
            var evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));

            if (evento == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Evento no encontrado.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            if (!evento.EspaciosAsignados.Any())
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" Este evento no tiene espacios asignados. No se puede registrar asistencia.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            if (!GestorAsistentes.ListarDesdeConsola())
                return;

            Console.Write("Nombre del asistente: ");
            var nombreAsistente = Console.ReadLine();
            var asistente = GestorAsistentes.ListarTodos()
                .FirstOrDefault(a => a.NombreCompleto.Equals(nombreAsistente, StringComparison.OrdinalIgnoreCase));

            if (asistente == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Asistente no encontrado.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            if (evento.Inscripciones.Any(i => i.Asistente == asistente))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Este asistente ya está registrado en este evento.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            int capacidad = evento.EspaciosAsignados.Sum(e => e.CapacidadMaxima);
            if (evento.Inscripciones.Count >= capacidad)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" Capacidad máxima alcanzada. No se puede registrar más asistentes.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Método de pago:");
            foreach (var metodo in Enum.GetValues(typeof(EMetododePago)))
                Console.WriteLine($" - {metodo}");

            Console.Write("Ingrese el método: ");
            var metodoInput = Console.ReadLine();

            if (!Enum.TryParse<EMetododePago>(metodoInput, true, out var metodoPagoSeleccionado))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Método de pago no válido.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Estado de la inscripción:");
            foreach (var estado in Enum.GetValues(typeof(EEstado)))
                Console.WriteLine($" - {estado}");

            Console.Write("Ingrese el estado: ");
            var estadoInput = Console.ReadLine();

            if (!Enum.TryParse<EEstado>(estadoInput, true, out var estadoSeleccionado))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Estado no válido.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            var inscripcion = new Inscripcion(asistente, evento, metodoPagoSeleccionado, estadoSeleccionado);
            evento.AgregarInscripcion(inscripcion);

            Console.WriteLine(" Inscripción registrada correctamente.");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }
        public static void AsignarOrador(string nombreEvento, Orador orador)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Evento? evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));
            if (evento == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No se encontró el evento con ese nombre.");
                Console.ResetColor();
            }
            Console.WriteLine("se añadio correctamente el orador");
            evento.AsignarOrador(orador);
            Console.ResetColor();
        }

        public static void AsignarAsistente(string nombreEvento, Asistentes asistente)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Evento? evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));
            if (evento == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No se encontró el evento con ese nombre.");
                Console.ResetColor();
            }

            Console.WriteLine("se añadio correctamente el Asistente");
            evento.AsignarAsistente(asistente);

        }

        public static void AsignarEspacio(string nombreEvento, Espacio espacio)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Evento? evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));
            if (evento == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No se encontró el evento con ese nombre.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            Console.WriteLine("se añadio correctamente el Espacio");
            evento.AsignarEspacio(espacio);
            Console.ResetColor();
            Console.ReadKey();
            return;
        }
        public static Evento? ObtenerEvento(string nombre)
        {
            var evento = ListarEventos().FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            return evento;
        }
        public static List<Evento> ListarEventos() => new(eventos);
    }
}