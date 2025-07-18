using Personas;
using Entidades;
using Utilidades;
namespace Inscripciones
{
    public class Inscripcion
    {
        public Asistentes Asistente { get; private set; }
        public Evento Evento { get; private set; }
        public DateTime FechaInscripcion { get; private set; }
        public EMetododePago MetodoPago { get; private set; }
        public EEstado Estado { get; private set; }

        public Inscripcion(Asistentes asistente, Evento evento, EMetododePago metodoPago, EEstado estado)
        {
            Asistente = asistente;
            Evento = evento;
            FechaInscripcion = DateTime.Now;
            MetodoPago = metodoPago;
            Estado = estado;
        }

        public override string ToString() =>
            $"Inscripción: {Asistente.NombreCompleto} a {Evento.Nombre} | Fecha: {FechaInscripcion:dd/MM/yyyy} | Estado: {Estado}";
    }
}