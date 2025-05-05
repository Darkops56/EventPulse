using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPulse
{
    public class Oradores
    {
        public string NombreCompleto;
        public string Especialidad;
        public string Empresa;
        public string email;
        public bool requiereAlojamiento;

     public Oradores(string NombreCompleto,string Especialidad,string Empresa,string email,bool requiereAlojamiento){ 
            this.NombreCompleto = NombreCompleto;
            this.Especialidad = Especialidad;
            this.Empresa = Empresa;
            this.Empresa = Empresa;
            NecesitaAlojamiento();
            NoNecesitaAlojamiento();
    } 
    public void NecesitaAlojamiento ()
        {
            if (requiereAlojamiento == true)
        {
               Console.WriteLine("Requiere de un alojamiento");

               requiereAlojamiento = false;
        }
        }
        public void NoNecesitaAlojamiento ()
        {
            {
            if (requiereAlojamiento == false)
               Console.WriteLine("No necesita un alojamiento ");
               
               requiereAlojamiento = true;
            }
        }
    }
}