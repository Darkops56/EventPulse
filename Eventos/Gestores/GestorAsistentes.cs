using Personas;
namespace Gestores
{
    public static class GestorAsistentes
    {
        private static List<Asistentes> asistentes = new();

        public static void CrearDesdeConsola()
        {
            Console.Write("Tipo (General/Preferencial): ");
            var tipo = Console.ReadLine().ToLower();

            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Teléfono: ");
            var telefono = Console.ReadLine();
            Console.Write("Empresa: ");
            var empresa = Console.ReadLine();
            Console.Write("¿Requiere certificado? (s/n): ");
            var certificado = Console.ReadLine().ToLower() == "s";

            if (tipo == "general")
                asistentes.Add(new AsistenteGeneral(nombre, email, telefono, empresa, certificado));
            else
                asistentes.Add(new AsistentePreferencial(nombre, email, telefono, empresa, certificado));

            Console.WriteLine("Asistente creado.");
        }

        public static bool ListarDesdeConsola()
        {
            if (!asistentes.Any())
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay asistentes.");
                Console.ResetColor();
                Console.ReadKey();
                return false;
            }

            foreach (var a in asistentes)
                Console.WriteLine(a);
            return true;
        }

        public static void EliminarDesdeConsola()
        {
            if (asistentes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("No hay eventos para que puedas eliminar.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            Console.Write("Nombre del asistente: ");
            var nombre = Console.ReadLine();
            var asistente = asistentes.FirstOrDefault(a => a.NombreCompleto.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (asistente == null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Asistente no encontrado.");
                Console.ResetColor();
            }
            asistentes.Remove(asistente);
            Console.WriteLine("Asistente eliminado.");
            Console.ReadKey();
            return;
        }

        public static List<Asistentes> ListarTodos() => new(asistentes);
    }
}