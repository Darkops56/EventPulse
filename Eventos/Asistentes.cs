namespace Eventos
{
    public class Asistentes
    {
        public string NombreCompleto { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public string Empresa { get; private set; }
        public bool NecesitaCertificado { get; private set; }

        public Asistentes(string nom, string mail, string tel, string emp, bool cert)
        {
            Validaciones.Cadena(nom, "Asistente.NombreCompleto");
            Validaciones.Email(mail);
            NombreCompleto = nom; Email = mail; Telefono = tel; Empresa = emp; NecesitaCertificado = cert;
        }

        public static Asistentes CrearInteractivo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Nombre completo: "); var nom = Console.ReadLine();
            Console.Write("Email: "); var mail = Console.ReadLine();
            Console.Write("Teléfono: "); var tel = Console.ReadLine();
            Console.Write("Empresa: "); var emp = Console.ReadLine();
            Console.Write("Necesita certificado (s/n): "); var cert = Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase);
            Console.ResetColor();
            Console.Clear();
            return new Asistentes(nom, mail, tel, emp, cert);
        }
       
    }
}