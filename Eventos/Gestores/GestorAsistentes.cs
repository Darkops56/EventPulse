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

        public static void ListarDesdeConsola()
        {
            if (!asistentes.Any())
            {
                Console.WriteLine("No hay asistentes.");
                Console.ReadKey();
                return;
            }

            foreach (var a in asistentes)
                Console.WriteLine(a);
        }

        public static void EliminarDesdeConsola()
        {
            ListarDesdeConsola();
            Console.Write("Nombre del asistente: ");
            var nombre = Console.ReadLine();
            var asistente = asistentes.FirstOrDefault(a => a.NombreCompleto.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (asistente == null)
                throw new Exception("Asistente no encontrado.");
            asistentes.Remove(asistente);
            Console.WriteLine("Asistente eliminado.");
        }

        public static List<Asistentes> ListarTodos() => new(asistentes);
    }
}