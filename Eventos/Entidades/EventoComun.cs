namespace Entidades
{
    public class EventoComun : Evento
    {
        private readonly string _tipo;

        public EventoComun(string tipo, string nombre, string descripcion, DateTime inicio, DateTime fin, string cliente, decimal presupuesto)
            : base(nombre, descripcion, inicio, fin, cliente, presupuesto)
        {
            _tipo = tipo;
        }

        public override string Tipo => _tipo;
    }
}