namespace EventPulse
{
    public class Inscripcion
    {
        public enum MetodoPago { TarjetaCredito, Transferencia, Efectivo }
        public enum EstadoInscripcion { Confirmada, Pendiente, Cancelada }

        public Asistentes Asistente { get; private set; }
        public Evento Evento { get; private set; }
        public DateTime FechaInscripcion { get; private set; }
        public MetodoPago? Pago { get; private set; }
        public EstadoInscripcion Estado { get; private set; }

        public Inscripcion(Asistentes a, Evento e)
        {
            Asistente = a ?? throw new ArgumentException("Asistente inválido.");
            Evento = e ?? throw new ArgumentException("Evento inválido.");
            FechaInscripcion = DateTime.Now;
            Estado = EstadoInscripcion.Confirmada;
        }
    }
}