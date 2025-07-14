using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personas
{
    public class AsistenteGeneral : Asistentes
    {
        public AsistenteGeneral(string nombreCompleto, string email, string telefono, string empresa, bool requiereCertificado)
            : base(nombreCompleto, email, telefono, empresa, requiereCertificado) { }

        public override string Tipo => "General";
    }

}