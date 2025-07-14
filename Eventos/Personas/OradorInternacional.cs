using Utilidades;

namespace Personas
{
    public class OradorInternacional : Orador
    {
        public string PaisOrigen { get; private set; }
        public string Idioma { get; private set; }
        public bool NecesitaInterprete { get; private set; }

        public OradorInternacional(string nombreCompleto, string especialidad, string empresa, string email, string paisOrigen, string idioma, bool necesitaInterprete)
            : base(nombreCompleto, especialidad, empresa, email)
        {
            Validaciones.Cadena(paisOrigen, "País de origen");
            Validaciones.Cadena(idioma, "Idioma");

            RequiereAlojamiento = true;
            PaisOrigen = paisOrigen;
            Idioma = idioma;
            NecesitaInterprete = necesitaInterprete;
        }

        public override string Tipo => "Internacional";

        public override string ToString() =>
            base.ToString() + $" | País: {PaisOrigen} | Idioma: {Idioma} | Intérprete: {(NecesitaInterprete ? "Sí" : "No")}";
    }

}