using Utilidades;
using Inscripciones;
namespace Entidades
{
    public abstract class Evento
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public string Cliente { get; private set; }
        public decimal Presupuesto { get; private set; }
        public List<Inscripcion> Inscripciones { get; private set; } = new();
        public List<Espacio> EspaciosAsignados { get; private set; } = new();

        public Evento(string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin, string cliente, decimal presupuesto)
        {
            Validaciones.Cadena(nombre, "Nombre del evento");
            Validaciones.Cadena(cliente, "Cliente");
            Validaciones.Fechas(fechaInicio, fechaFin);
            Validaciones.Entero(presupuesto);

            Nombre = nombre;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Cliente = cliente;
            Presupuesto = presupuesto;
        }

        public abstract string Tipo { get; }

        public void AsignarEspacio(Espacio espacio)
        {
            if (!EspaciosAsignados.Contains(espacio))
            {
                EspaciosAsignados.Add(espacio);
            }
        }

        public void AgregarInscripcion(Inscripcion inscripcion)
        {
            int capacidadTotal = EspaciosAsignados.Sum(e => e.CapacidadMaxima);
            if (Inscripciones.Count >= capacidadTotal)
                throw new Exception("Capacidad máxima alcanzada para este evento.");

            Inscripciones.Add(inscripcion);
        }

        public override string ToString() =>
            $"{Tipo}: {Nombre} | {Descripcion} | {FechaInicio:dd/MM/yyyy} al {FechaFin:dd/MM/yyyy} | Cliente: {Cliente}";
    }

}