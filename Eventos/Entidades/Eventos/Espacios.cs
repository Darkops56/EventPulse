using Utilidades;
namespace Entidades
{
    public class Espacio
    {
        public string Nombre { get; private set; }
        public string Direccion { get; private set; }
        public int CapacidadMaxima { get; private set; }
        public int CantidadSalas { get; private set; }
        public bool EquipamientoTecnico { get; private set; }

        public Espacio(string nombre, string direccion, int capacidadMaxima, int cantidadSalas, bool equipamientoTecnico)
        {
            Validaciones.Cadena(nombre, "Nombre del espacio");
            Validaciones.Cadena(direccion, "Dirección del espacio");
            Validaciones.CantidadDisponible(capacidadMaxima);
            Validaciones.CantidadDisponible(cantidadSalas);

            Nombre = nombre;
            Direccion = direccion;
            CapacidadMaxima = capacidadMaxima;
            CantidadSalas = cantidadSalas;
            EquipamientoTecnico = equipamientoTecnico;
        }

        public override string ToString() =>
            $"{Nombre} - {Direccion} | Capacidad: {CapacidadMaxima} | Salas: {CantidadSalas} | Equipamiento: {(EquipamientoTecnico ? "Sí" : "No")}";
    }

}