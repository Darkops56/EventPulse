using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personas
{
    public class OradorLocal : Orador
    {
        public OradorLocal(string nombreCompleto, string especialidad, string empresa, string email)
            : base(nombreCompleto, especialidad, empresa, email)
        {
            RequiereAlojamiento = false;
        }
        public override string Tipo => "Local";
    }
}