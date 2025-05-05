using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPulse
{
    public class Evento
    {
        private string _evento;
        private string _descripcion;
        private DateTime _fechaI;
        private DateTime _fechaF;
        private string _tipoEvento;
        private string _empresa;
        private int _presupuesto;
        public string Nombre 
        {
            get{return _evento;}
            set
            {
                Validaciones.Cadena(value, "Nombre del Evento.");
                _evento = value;
            }
        }
        public string Descripcion
        {
            get{return _descripcion;}
            set
            {
                _descripcion = value;
            }
        }
        public DateTime FechaInicio
        {
            get{return _fechaI;}
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("La fecha de inicio no puede ser del futuro.");
                _fechaI = value;
            }
        }
        public DateTime FechaFinalizacion
        {
            get{return _fechaF;}
            set
            {
                if (value == DateTime.Now)
                    throw new ArgumentException("La fecha de finalizacion no puede ser igual a la actual.");
                _fechaF = value;
            }
        }
        public string TipoEvento 
        {
            get{ return _tipoEvento;}
            set
            {
                Validaciones.Cadena(value, "Tipo de Evento.");
                _tipoEvento = value;
            }
        }
        public int Presupuesto 
        {
            get{ return _presupuesto;}
            set
            {
                Validaciones.Negativo(value, "Presupuesto del Evento.");
                _presupuesto = value;
            }
        }
        public string NombreEmpresa
        {
            get{return _empresa;}
            set
            {
                Validaciones.Cadena(value, "Nombre de la Empresa");
                _empresa = value;
            }
        }
        public Evento (string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFinalizacion, string tipoEvento, int presupuesto, string nombreEmpresa)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFinalizacion = fechaFinalizacion;
            TipoEvento = tipoEvento;
            Presupuesto = presupuesto;
            NombreEmpresa = nombreEmpresa;
        }
    }
}