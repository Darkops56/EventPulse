using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenPulse
{
    public class Espacios
    {
        private string _espacios;
        private string _direccion;
        private int _capacidad;
        private int _salasDis;
        private string _equipamiento;
        public Evento evento;
        public string NombreSala
        {
            get { return _espacios;}
            set
            {
                Validaciones.Cadena(value, "El Nombre del Espacio");
                _espacios = value;
            }
        }
        public string Direccion 
        {
            get { return _direccion;}
            set 
            {
                Validaciones.Cadena(value, "La direccion de la Sala.");
                _direccion = value;
            }
        }
        public int Capacidad
        {
            get { return _capacidad;}
            set
            {
                Validaciones.Negativo(value, "Capacidad del Espacio.");
                _capacidad = value;
            }
        }
        public int SalasDisponibles
        {
            get { return _salasDis;}
            set 
            {
                Validaciones.Negativo(value, "Salas Disponibles");
                _salasDis  = value;
            }
        }
        public string EquipamientoTenico
        {
            get { return _equipamiento;}
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _equipamiento = "no hay equipamiento";
                }
                else
                {
                    _equipamiento = value;
                }
            }
        }
    }
}