using Personas;
using Entidades;
namespace Gestores
{
    public static class GestorOradores
    {
        private static List<Orador> oradores = new();

        public static void CrearDesdeConsola()
        {
            Console.Write("Tipo (Local/Internacional): ");
            var tipo = Console.ReadLine().ToLower();

            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();
            Console.Write("Especialidad: ");
            var especialidad = Console.ReadLine();
            Console.Write("Empresa: ");
            var empresa = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();

            if (tipo == "local")
            {
                oradores.Add(new OradorLocal(nombre, especialidad, empresa, email));
            }
            else
            {
                Console.Write("País: ");
                var pais = Console.ReadLine();
                Console.Write("Idioma: ");
                var idioma = Console.ReadLine();
                Console.Write("¿Necesita intérprete? (s/n): ");
                bool interprete = Console.ReadLine().ToLower() == "s";
                oradores.Add(new OradorInternacional(nombre, especialidad, empresa, email, pais, idioma, interprete));
            }

            Console.WriteLine("Orador creado.");
        }

        public static void ListarDesdeConsola()
        {
            if (!oradores.Any())
            {
                Console.WriteLine("No hay oradores.");
                Console.ReadKey();
                return;
            }

            foreach (var o in oradores)
                Console.WriteLine(o);
        }

        public static void EliminarDesdeConsola()
        {
            ListarDesdeConsola();
            Console.Write("Nombre del orador: ");
            var nombre = Console.ReadLine();
            var orador = oradores.FirstOrDefault(o => o.NombreCompleto.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (orador == null)
                throw new Exception("Orador no encontrado.");
            oradores.Remove(orador);
            Console.WriteLine("Orador eliminado.");
        }

        public static List<Orador> ListarOradores() => new(oradores);
    }
}