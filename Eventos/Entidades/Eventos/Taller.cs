using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades
{
    public class Taller : Evento
    {
        public Dictionary<string, int> Materiales { get; private set; } = new();

        public Taller(string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin, string cliente, decimal presupuesto)
            : base(nombre, descripcion, fechaInicio, fechaFin, cliente, presupuesto)
        { }

        public override string Tipo => "Taller";

        public void DistribuirMaterial(string nombreMaterial, int cantidad)
        {
            if (cantidad < 1)
                throw new Exception("Cantidad de material inválida.");
            Materiales[nombreMaterial] = cantidad;
        }

        public bool PuedeEmitirCertificados => true;
    }

}