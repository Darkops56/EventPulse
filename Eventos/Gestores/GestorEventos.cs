using Entidades;
using Inscripciones;
using Utilidades;
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

        public static void ListarDesdeConsola()
        {
            if (eventos.Count == 0)
            {
                Console.WriteLine("No hay eventos.");
                Console.ReadKey();
                return;
            }

            foreach (var e in eventos)
                Console.WriteLine(e);
        }

        public static void EliminarDesdeConsola()
        {
            if (eventos.Count == 0)
            {
                Console.WriteLine("No hay eventos para que puedas eliminar.");
                Console.ReadKey();
                return;
            }
            Console.Write("Nombre del evento: ");
            var nombre = Console.ReadLine();
            var evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (evento == null)
                throw new Exception("Evento no encontrado.");
            eventos.Remove(evento);
            Console.WriteLine("Evento eliminado.");
        }
        public static void RegistrarInscripcionDesdeConsola()
        {
            if (eventos.Count == 0)
            {
                Console.WriteLine("No hay eventos para que puedas eliminar.");
                Console.ReadKey();
                return;
            }
            foreach (var e in eventos)
            {
                Console.WriteLine($"{e.Tipo}: {e.Nombre}, {e.Descripcion}, {e.FechaInicio}, {e.FechaFin}, {e.Cliente}, {e.Presupuesto}");
            }
            Console.Write("Nombre del evento: ");
            var nombreEvento = Console.ReadLine();
            var evento = eventos.FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));

            if (evento == null)
            {
                Console.WriteLine("Evento no encontrado.");
                Console.ReadKey();
                return;
            }

            if (!evento.EspaciosAsignados.Any())
            {
                Console.WriteLine(" Este evento no tiene espacios asignados. No se puede registrar asistencia.");
                Console.ReadKey();
                return;
            }

            GestorAsistentes.ListarDesdeConsola();
            
            Console.Write("Nombre del asistente: ");
            var nombreAsistente = Console.ReadLine();
            var asistente = GestorAsistentes.ListarTodos()
                .FirstOrDefault(a => a.NombreCompleto.Equals(nombreAsistente, StringComparison.OrdinalIgnoreCase));

            if (asistente == null)
            {
                Console.WriteLine("Asistente no encontrado.");
                Console.ReadKey();
                return;
            }

            if (evento.Inscripciones.Any(i => i.Asistente == asistente))
            {
                Console.WriteLine("Este asistente ya está registrado en este evento.");
                Console.ReadKey();
                return;
            }

            int capacidad = evento.EspaciosAsignados.Sum(e => e.CapacidadMaxima);
            if (evento.Inscripciones.Count >= capacidad)
            {
                Console.WriteLine(" Capacidad máxima alcanzada. No se puede registrar más asistentes.");
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
                Console.WriteLine("Método de pago no válido.");
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
                Console.WriteLine("Estado no válido.");
                Console.ReadKey();
                return;
            }

            var inscripcion = new Inscripcion(asistente, evento, metodoPagoSeleccionado, estadoSeleccionado);
            evento.AgregarInscripcion(inscripcion);

            Console.WriteLine(" Inscripción registrada correctamente.");
            Console.ReadKey();
        }
        public static List<Evento> ListarEventos() => new(eventos);
    }
}