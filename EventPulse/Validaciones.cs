using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPulse
{
    public static class Validaciones
    {
        public static string Cadena (string Cadena, string error)
        {
            if (string.IsNullOrEmpty (Cadena))
            {
                throw new ArgumentException ($"La Cadena ingresa es invalida en: {error}");
            } else
            {
                return Cadena;
            }
        }
        public static int Negativo (int cantidad, string error)
        {
            if (cantidad < 0)
            {
                throw new Exception($"La cantidad ingresada no puede ser negativa: {error}");
            } else
            {
                return cantidad;
            }
        }
    }
}