using System;

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
            if (cap < 1) throw new ArgumentException("Capacidad debe >0");
            if (salas < 1) throw new ArgumentException("Debe haber >=1 sala");
            Nombre = nombre; Direccion = dir; Capacidad = cap; SalasDisponibles = salas; EquipoTecnico = equipo;
        }

        public static Espacio CrearInteractivo()
        {
            Console.Write("Nombre del espacio: "); var nom = Console.ReadLine();
            Console.Write("Dirección: "); var dir = Console.ReadLine();
            Console.Write("Capacidad: "); var cap = int.Parse(Console.ReadLine());
            Console.Write("Salas disponibles: "); var sal = int.Parse(Console.ReadLine());
            Console.Write("Equipo técnico (s/n): "); var eq = Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase);
            return new Espacio(nom, dir, cap, sal, eq);
        }
    }
}