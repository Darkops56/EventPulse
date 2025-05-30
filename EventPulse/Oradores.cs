using System;
using System.Text.RegularExpressions;

namespace EventPulse
{
    public class Orador
    {
        public string NombreCompleto { get; private set; }
        public Evento.TipoEvento Experiencia { get; private set; }
        public string Empresa { get; private set; }
        public string Email { get; private set; }
        public bool NecesitaAlojamiento { get; private set; }

        public Orador(string nombre, Evento.TipoEvento exp, string empresa, string email, bool aloj)
        {
            Validaciones.Cadena(nombre, "Orador.NombreCompleto");
            Validaciones.Cadena(empresa, "Orador.Empresa");
            Validaciones.Email(email);
            NombreCompleto = nombre;
            Experiencia = exp;
            Empresa = empresa;
            Email = email;
            NecesitaAlojamiento = aloj;
        }

        public static Orador CrearInteractivo()
        {
            Console.Write("Nombre completo: "); var nom = Console.ReadLine();
            Console.WriteLine("Especialidad (1-Conferencia,2-Taller,3-Seminario,4-Feria): ");
            var exp = (Evento.TipoEvento)(int.Parse(Console.ReadLine()) - 1);
            Console.Write("Empresa: "); var emp = Console.ReadLine();
            Console.Write("Email: "); var mail = Console.ReadLine();
            Console.Write("Necesita alojamiento (s/n): "); var aloj = Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase);
            Console.Clear();
            return new Orador(nom, exp, emp, mail, aloj);
            
        }
    }
}