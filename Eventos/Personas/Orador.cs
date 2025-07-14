using Utilidades;
namespace Personas
{
    public abstract class Orador
    {
        public string NombreCompleto { get; private set; }
        public string Especialidad { get; private set; }
        public string Empresa { get; private set; }
        public string Email { get; private set; }
        public bool RequiereAlojamiento { get; protected set; }

        protected Orador(string nombreCompleto, string especialidad, string empresa, string email)
        {
            Validaciones.Cadena(nombreCompleto, "Nombre del orador");
            Validaciones.Cadena(especialidad, "Especialidad");
            Validaciones.Cadena(empresa, "Empresa");
            Validaciones.Email(email);

            NombreCompleto = nombreCompleto;
            Especialidad = especialidad;
            Empresa = empresa;
            Email = email;
        }

        public abstract string Tipo { get; }

        public override string ToString() =>
            $"{Tipo}: {NombreCompleto} | {Especialidad} | {Empresa} | Email: {Email} | Alojamiento: {(RequiereAlojamiento ? "Sí" : "No")}";
    }

}