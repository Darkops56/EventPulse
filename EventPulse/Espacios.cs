namespace EventPulse
{
    public class Espacio
    {
        public string Nombre { get; private set; }
        public string Direccion { get; private set; }
        public int Capacidad { get; private set; }
        public int SalasDisponibles { get; private set; }
        public bool EquipoTecnico { get; private set; }

        public Espacio(string nombre, string dir, int cap, int salas, bool equipo)
        {
            Validaciones.Cadena(nombre, "Espacio.Nombre");
            if (cap < 1) throw new ArgumentException("Capacidad debe ser mayor a 0");
            if (salas < 1) throw new ArgumentException("Debe haber al menos 1 sala");
            Nombre = nombre; Direccion = dir; Capacidad = cap; SalasDisponibles = salas; EquipoTecnico = equipo;
        }

        public static Espacio CrearInteractivo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Nombre del espacio: "); var nom = Console.ReadLine();
            Console.Write("Dirección: "); var dir = Console.ReadLine();
            Console.Write("Capacidad: "); var cap = int.Parse(Console.ReadLine());
            Console.Write("Salas disponibles: "); var sal = int.Parse(Console.ReadLine());
            Console.Write("Equipo técnico (s/n): "); var eq = Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase);
            Console.ResetColor();
            Console.Clear();
            return new Espacio(nom, dir, cap, sal, eq);
        }
    }
}