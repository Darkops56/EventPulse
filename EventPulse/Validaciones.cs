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
        public static void ValidarArea (string tipoEvento, string Especialidad)
        {
            if (string.IsNullOrEmpty(tipoEvento) || string.IsNullOrEmpty (Especialidad))
            {
                throw new ValidarArea($"No dejar el tipo de evento o la especialidad vacios: {error}");
            }
        }
        public static void ValidarMail (string email)
        {
            if (string.IsNullOrEmpty(email))
            string inicio = email.Substring(0, email.LastIndexOf("@")).Trim();
            string medio = email.Substring(email.LastIndexOf('@') + 1, email.LastIndexOf('.') - email.LastIndexOf('@') - 1).Trim();
            string final = email.Substring(email.LastIndexOf('.'), email.Length - email.LastIndexOf('.')).Trim();

            if (email.Contains("@") && email.EndsWith(".com") && inicio.Trim().Length > 3 && medio.Trim().Length > 3 && final.Trim().Length >= 3)
            {
                return email;
            }       else
            {
                throw new ArgumentException ("La pifiaste con el correo");
            }
        }
        public static void ValidarFecha (DateTime _fechaI, DateTime _fechaF)
        {
            if (_fechaI == default || _fechaF == default)
                throw new ValidarFecha ("Las fechas no pueden estar vacias");

            if (_fechaI > _fechaF)
                throw new ArgumentException ("La fecha de inicio no puede ser menor a la de final");
        }
        public static void ValidarCapacidad ()
}
