using System.Text.RegularExpressions;

namespace Eventos
{
    public static class Validaciones
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public static void Cadena(string valor, string err)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException($"La cadena no es válida: {err}");
        }

        public static void Email(string email)
        {
            if (!EmailRegex.IsMatch(email))
                throw new ArgumentException($"Email inválido: {email}");
        }

        public static void Fechas(DateTime inicio, DateTime fin)
        {
            if (fin < inicio)
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");
        }
    }
}
