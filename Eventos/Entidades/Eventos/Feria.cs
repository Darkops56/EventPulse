using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entidades
{
    public class Feria : Evento
    {
        public string PlanoOrganizacion { get; private set; }

        public Feria(string nombre, string descripcion, DateTime fechaInicio, DateTime fechaFin, string cliente, decimal presupuesto)
            : base(nombre, descripcion, fechaInicio, fechaFin, cliente, presupuesto)
        { }

        public override string Tipo => "Feria";

        public void GenerarPlano(string datosPlano)
        {
            if (string.IsNullOrWhiteSpace(datosPlano))
                throw new Exception("Datos del plano inválidos.");
            PlanoOrganizacion = datosPlano;
        }
    }


}