namespace EventPulse
{
    public class Evento
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public string Tipo { get; private set; }
        public decimal Presupuesto { get; private set; }
        public string EmpresaCliente { get; private set; }

        private List<Orador> _oradores = new List<Orador>();

        private List<Espacio> _espacios = new List<Espacio>();

        private List<Inscripcion> _inscripciones = new List<Inscripcion>();
        public List<Asistentes> Asistentes { get; set; } = new();

        private Evento(string nombre, string descripcion, DateTime inicio, DateTime fin, string tipo, decimal presupuesto, string cliente)
        {
            Validaciones.Cadena(nombre, "Evento.Nombre");
            Validaciones.Fechas(inicio, fin);
            Validaciones.Cadena(cliente, "Evento.EmpresaCliente");

            Nombre = nombre;
            Descripcion = descripcion;
            FechaInicio = inicio;
            FechaFin = fin;
            Tipo = tipo;
            Presupuesto = presupuesto;
            EmpresaCliente = cliente;
        }

        public static Evento CrearInteractivo()
        {
            Console.Write("Nombre: "); var nombre = Console.ReadLine();
            Console.Write("Descripción: "); var desc = Console.ReadLine();
            Console.Write("Fecha inicio (yyyy-MM-dd): "); var fi = DateTime.Parse(Console.ReadLine());
            Console.Write("Fecha fin (yyyy-MM-dd): "); var ff = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Especialidad/tipo del evento.");
            var tipo = Console.ReadLine();
            Console.Write("Presupuesto: "); var pres = decimal.Parse(Console.ReadLine());
            Console.Write("Empresa cliente: "); var cliente = Console.ReadLine();

            var evento = new Evento(nombre, desc, fi, ff, tipo, pres, cliente);

            Console.WriteLine("¿Agregar espacios? (s/n)");
            while (Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                var esp = Espacio.CrearInteractivo();
                evento.AgregarEspacio(esp);
                Console.WriteLine("¿Agregar otro espacio? (s/n)");
            }
            Console.Clear();
            return evento;
        }

        public void AgregarEspacio(Espacio espacio)
        {
            if (espacio == null) throw new ArgumentException("Espacio inválido.");
            if (!_espacios.Contains(espacio)) _espacios.Add(espacio);
        }

        public void AgregarOrador(Orador orador)
        {
            if (orador == null) throw new ArgumentException("Orador inválido.");
            if (orador.Experiencia != Tipo) throw new ArgumentException("Especialidad no coincide.");
            _oradores.Add(orador);
        }
        public void ListarOradores()
        {
            if (_oradores == null || _oradores.Count == 0)
            {
                Console.WriteLine($"El evento \"{Nombre}\" no tiene oradores registrados.");
                return;
            }

            Console.WriteLine($"=== Oradores del evento \"{Nombre}\" ===");
            foreach (var o in _oradores)
            {
                Console.WriteLine($"> {o.NombreCompleto} | {o.Email} | {o.Empresa} | Especialidad: {o.Experiencia} | Alojamiento: {(o.NecesitaAlojamiento ? "Sí" : "No")}");
            }
        }

        public void RegistrarAsistente(Asistentes asistente)
        {
            if (asistente == null) throw new ArgumentException("Asistente inválido.");
            int capacidad = 0; foreach (var e in _espacios) capacidad += e.Capacidad;
            if (_inscripciones.Count >= capacidad) throw new ArgumentException("Capacidad alcanzada.");
            _inscripciones.Add(new Inscripcion(asistente, this));
        }
        public void ListarAsistentes()
        {
            if (Asistentes == null || Asistentes.Count == 0)
            {
                Console.WriteLine($"El evento \"{Nombre}\" no tiene asistentes registrados.");
                return;
            }

            Console.WriteLine($"=== Asistentes del evento \"{Nombre}\" ===");
            foreach (var a in Asistentes)
            {
                Console.WriteLine($"> {a.NombreCompleto} | {a.Email} | {a.Empresa} | Tel: {a.Telefono} | Certificado: {(a.NecesitaCertificado ? "Sí" : "No")}");
            }
        }

        public override string ToString() => $"{Nombre} ({Tipo}) [{FechaInicio:dd/MM/yyyy}-{FechaFin:dd/MM/yyyy}] Presupuesto:{Presupuesto:C} Cliente:{EmpresaCliente}";
    }
}