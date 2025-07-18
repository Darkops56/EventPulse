using Entidades;
namespace Gestores
{
    public static class GestorEspacios
    {
        private static List<Espacio> espacios = new();

        public static void CrearDesdeConsola()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();
            Console.Write("Dirección: ");
            var direccion = Console.ReadLine();
            Console.Write("Capacidad máxima: ");
            var capacidad = int.Parse(Console.ReadLine());
            Console.Write("Cantidad de salas: ");
            var salas = int.Parse(Console.ReadLine());
            Console.Write("¿Tiene equipamiento técnico? (s/n): ");
            bool tecnico = Console.ReadLine().ToLower() == "s";

            espacios.Add(new Espacio(nombre, direccion, capacidad, salas, tecnico));
            Console.WriteLine("Espacio creado.");
            Console.ResetColor();
        }

        public static bool ListarDesdeConsola()
        {
            if (!espacios.Any())
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay espacios.");
                Console.ResetColor();
                Console.ReadKey();
                return false;
            }

            foreach (var e in espacios)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(e);
                Console.ResetColor();
            }
            return true;
        }

        public static void EliminarDesdeConsola()
        {
            if (espacios.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay espacios para que puedas eliminar.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Nombre del espacio: ");
            var nombre = Console.ReadLine();
            var espacio = espacios.FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (espacio == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Espacio no encontrado.");
                Console.ResetColor();
            }
            espacios.Remove(espacio);
            Console.WriteLine("Espacio eliminado.");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }

        public static List<Espacio> ListarTodos() => new(espacios);
    }
}