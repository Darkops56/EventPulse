namespace Eventos
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
        private List<Asistentes> _asistentes = new List<Asistentes>();

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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Nombre: "); var nombre = Console.ReadLine();
            Console.Write("Descripción: "); var desc = Console.ReadLine();
            Console.Write("Fecha inicio (yyyy-MM-dd): "); var fi = DateTime.Parse(Console.ReadLine());
            Console.Write("Fecha fin (yyyy-MM-dd): "); var ff = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Especialidad/tipo del evento.");
            var tipo = Console.ReadLine();
            Console.Write("Presupuesto: "); var pres = decimal.Parse(Console.ReadLine());
            Console.Write("Empresa cliente: "); var cliente = Console.ReadLine();
            Console.ResetColor();


            var evento = new Evento(nombre, desc, fi, ff, tipo, pres, cliente);

            var conteo = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("¿Agregar espacios? (s/n)");
            while (Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                var esp = Espacio.CrearInteractivo();
                evento.AgregarEspacio(esp);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("¿Agregar otro espacio? (s/n)");
                Console.ResetColor();
                conteo += 1;
            }
            while(conteo == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Debe tener un espacio el evento.");
                Console.WriteLine("¿Agregar espacios? (s/n)");
                Console.ResetColor();
                while (Console.ReadLine().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    var esp = Espacio.CrearInteractivo();
                    evento.AgregarEspacio(esp);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("¿Agregar otro espacio? (s/n)");
                    Console.ResetColor();
                    conteo += 1;
                }
            }
            Console.ResetColor();
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Evento creado correctamente");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            return evento;
        }
        public void AgregarEspacio(Espacio espacio)
        {
            if (espacio == null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Espacio invalido.");
                Console.ResetColor();
                Console.ReadKey();
                return;

            }
            if (!_espacios.Contains(espacio)) _espacios.Add(espacio);

        }

        public void AgregarOrador(Orador orador)
        {
            if (orador == null) throw new ArgumentException("Orador inválido.");
            if (orador.Experiencia != Tipo)
            {
                Console.WriteLine("Especialidad no coincide");
                Console.ReadKey();
                return;
            }
            _oradores.Add(orador);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Orador creado correctamente...");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Presione una tecla para continuar");
            Console.ResetColor();
            Console.ReadKey();
        }
        public void ListarOradores()
        {
            if (_oradores == null || _oradores.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"El evento \"{Nombre}\" no tiene oradores registrados.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"=== Oradores del evento \"{Nombre}\" ===");
            Console.ResetColor();
            foreach (var o in _oradores)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"> {o.NombreCompleto} | {o.Email} | {o.Empresa} | Especialidad: {o.Experiencia} | Alojamiento: {(o.NecesitaAlojamiento ? "Sí" : "No")}");
                Console.ResetColor();
            }
        }

        public void RegistrarAsistente(Asistentes asistente)
        {
            if (asistente == null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Asistente inválido");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            int capacidad = 0; foreach (var e in _espacios) capacidad += e.Capacidad;
            if (_inscripciones.Count >= capacidad)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Capacidad Alcanzada");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            _inscripciones.Add(new Inscripcion(asistente, this));
            _asistentes.Add(asistente);
        }
        public void ListarAsistentes()
        {
            if (_asistentes == null || _asistentes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"El evento \"{Nombre}\" no tiene asistentes registrados.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"=== Asistentes del evento \"{Nombre}\" ===");
            Console.ResetColor();
            foreach (var a in _asistentes)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"> {a.NombreCompleto} | {a.Email} | {a.Empresa} | Tel: {a.Telefono} | Certificado: {(a.NecesitaCertificado ? "Sí" : "No")}");
                Console.ResetColor();
            }
        }
        public override string ToString() => $"{Nombre} ({Tipo}) [{FechaInicio:dd/MM/yyyy}-{FechaFin:dd/MM/yyyy}] Presupuesto:{Presupuesto:C} Empresa:{EmpresaCliente}";
        
    }
}