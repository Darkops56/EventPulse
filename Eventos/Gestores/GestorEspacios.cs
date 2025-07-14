using Entidades;
namespace Gestores
{
    public static class GestorEspacios
    {
        private static List<Espacio> espacios = new();

        public static void CrearDesdeConsola()
        {
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
        }

        public static void ListarDesdeConsola()
        {
            if (!espacios.Any())
            {
                Console.WriteLine("No hay espacios.");
                Console.ReadKey();
                return;
            }

            foreach (var e in espacios)
                Console.WriteLine(e);
        }

        public static void EliminarDesdeConsola()
        {
            ListarDesdeConsola();
            Console.Write("Nombre del espacio: ");
            var nombre = Console.ReadLine();
            var espacio = espacios.FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (espacio == null)
                throw new Exception("Espacio no encontrado.");
            espacios.Remove(espacio);
            Console.WriteLine("Espacio eliminado.");
        }

        public static List<Espacio> ListarTodos() => new(espacios);
    }
}