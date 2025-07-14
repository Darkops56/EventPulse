using Utilidades;
namespace Personas
{
    public abstract class Asistentes
    {
        public string NombreCompleto { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public string Empresa { get; private set; }
        public bool RequiereCertificado { get; private set; }

        protected Asistentes(string nombreCompleto, string email, string telefono, string empresa, bool requiereCertificado)
        {
            Validaciones.Cadena(nombreCompleto, "Nombre del asistente");
            Validaciones.Email(email);
            Validaciones.Cadena(telefono, "Teléfono");
            Validaciones.Cadena(empresa, "Empresa");

            NombreCompleto = nombreCompleto;
            Email = email;
            Telefono = telefono;
            Empresa = empresa;
            RequiereCertificado = requiereCertificado;
        }

        public abstract string Tipo { get; }

        public override string ToString() =>
            $"{Tipo}: {NombreCompleto} | {Empresa} | Email: {Email} | Certificado: {(RequiereCertificado ? "Sí" : "No")}";
    }

}