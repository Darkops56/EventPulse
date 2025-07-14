using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades
{
    public class Conferencia : Evento
    {
        public Conferencia(string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin, string cliente, decimal presupuesto)
            : base(nombre, descripcion, fechaInicio, fechaFin, cliente, presupuesto)
        { }

        public override string Tipo => "Conferencia";

        public void AsignarEspacioConPrioridad(List<Espacio> disponibles)
        {
            var preferidos = disponibles.Where(e => e.EquipamientoTecnico).ToList();
            foreach (var espacio in preferidos)
            {
                AsignarEspacio(espacio);
            }
        }

        public bool PuedeEmitirCertificados => true;
    }

}