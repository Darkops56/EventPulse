using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personas
{
    public class AsistentePreferencial : Asistentes
    {
        public bool AccesoExclusivo { get; private set; } = true;
        public bool MaterialExtra { get; private set; } = true;
        public bool AgendaPersonalizada { get; private set; } = true;

        public AsistentePreferencial(string nombreCompleto, string email, string telefono, string empresa, bool requiereCertificado)
            : base(nombreCompleto, email, telefono, empresa, requiereCertificado) { }

        public override string Tipo => "Preferencial";
    }

}