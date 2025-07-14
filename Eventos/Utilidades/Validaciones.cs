using System.Text.RegularExpressions;

namespace Utilidades
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
        public static decimal Entero(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("Valor no valido");
            }
            return valor;
        }
        public static void Fechas(DateTime inicio, DateTime fin)
        {
            if (fin < inicio)
                throw new ArgumentException("La fecha de fin debe ser p osterior a la fecha de inicio.");
        }
        public static int CantidadDisponible(int valor)
        {
            if (valor < 1)
            {
                throw new ArgumentException("Cantidad no disponible/invalida");
            }
            return valor;
        }
    }
}
