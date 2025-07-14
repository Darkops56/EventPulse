using ConsoleTools;
using Personas;
using Entidades;
using Gestores;
using Inscripciones;


var menu = new ConsoleMenu()
            .Add("Gestionar Eventos", SubmenuEventos)
            .Add("Gestionar Oradores", SubmenuOradores)
            .Add("Gestionar Asistentes", SubmenuAsistentes)
            .Add("Gestionar Espacios", SubmenuEspacios)
            .Add("Salir", ConsoleMenu.Close);

menu.Show();
static void SubmenuEventos()
{
    new ConsoleMenu()
        .Add("Crear Evento", GestorEventos.CrearDesdeConsola)
        .Add("Listar Eventos", () =>
        {
            GestorEventos.ListarDesdeConsola();
            Console.ReadKey();
        })
        .Add("Eliminar Evento", GestorEventos.EliminarDesdeConsola)
        .Add("Registrar Inscripción", GestorEventos.RegistrarInscripcionDesdeConsola)
        .Add("Asignar Espacio Prioritario (Conferencia)", AsignarEspaciosConferencia)
        .Add("Distribuir Materiales (Taller)", DistribuirMaterialesTaller)
        .Add("Generar Plano de Feria", GenerarPlanoFeria)
        .Add("Emitir Certificado", EmitirCertificado)
        .Add("Volver", ConsoleMenu.Close)
        .Show();    
}

static void AsignarEspaciosConferencia()
{
    GestorEventos.ListarDesdeConsola();
    Console.Write("Nombre del evento de tipo conferencia: ");
    var nombre = Console.ReadLine();
    var evento = GestorEventos.ListarEventos().OfType<Conferencia>()
        .FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

    if (evento == null)
    {
        Console.WriteLine("Conferencia no encontrada.");
        Console.ReadKey();
    }
    else
    {
        var disponibles = GestorEspacios.ListarTodos();
        if (disponibles == null)
        {
            Console.WriteLine("No hay espacios disponibles.");
            Console.ReadKey();
            return;
        }
        evento.AsignarEspacioConPrioridad(disponibles);
        Console.WriteLine("Espacios asignados con prioridad a equipamiento técnico.");
        Console.ReadKey();
    }
}

static void DistribuirMaterialesTaller()
{
    GestorEventos.ListarDesdeConsola();

    Console.Write("Nombre del Evento de tipo Taller: ");
    var nombre = Console.ReadLine();

    var evento = GestorEventos.ListarEventos()
        .FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

    if (evento is not Taller taller)
    {
        Console.WriteLine("El evento no es un Taller. Esta función no está disponible.");
        Console.ReadKey();
        return;
    }
    Console.Write("Nombre del material: ");
    var material = Console.ReadLine();
    Console.Write("Cantidad: ");
    var cantidad = int.Parse(Console.ReadLine());

    taller.DistribuirMaterial(material, cantidad);
    Console.WriteLine("Material distribuido.");
    Console.ReadKey();
}

static void GenerarPlanoFeria()
{
    GestorEventos.ListarDesdeConsola();

    Console.Write("Nombre del evento de tipo Feria: ");
    var nombre = Console.ReadLine();

    var evento = GestorEventos.ListarEventos()
        .FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

    if (evento == null)
    {
        Console.WriteLine("Evento no encontrado.");
        Console.ReadKey();
        return;
    }

    if (evento is not Feria feria)
    {
        Console.WriteLine("El evento no es una Feria. Esta función no está disponible.");
        Console.ReadKey();
        return;
    }

    Console.Write("Datos del plano (texto simulado): ");
    var plano = Console.ReadLine();

    feria.GenerarPlano(plano);
    Console.WriteLine("Plano generado correctamente.");
    Console.ReadKey();
}

static void EmitirCertificado()
{
    GestorEventos.ListarDesdeConsola();

    Console.Write("Nombre del evento de tipo Taller o Conferencia: ");
    var nombreEvento = Console.ReadLine();

    var evento = GestorEventos.ListarEventos()
        .FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));

    if (evento == null)
    {
        Console.WriteLine("Evento no encontrado.");
        Console.ReadKey();
        return;
    }

    if (evento is not Taller && evento is not Conferencia)
    {
        Console.WriteLine("Este evento no emite certificados.");
        Console.ReadKey();
        return;
    }
    if (GestorAsistentes.ListarTodos().Count == 0)
    {
        Console.WriteLine("No hay asistentes");
        Console.ReadKey();
        return;
    }
    GestorAsistentes.ListarDesdeConsola();
    Console.Write("Nombre del asistente: ");
    var nombreAsistente = Console.ReadLine();

    var inscripcion = evento.Inscripciones
        .FirstOrDefault(i => i.Asistente.NombreCompleto.Equals(nombreAsistente, StringComparison.OrdinalIgnoreCase));

    if (inscripcion == null)
    {
        Console.WriteLine("El asistente no está inscrito en este evento.");
        Console.ReadKey();
        return;
    }
    Console.WriteLine($"📄 Certificado generado para {nombreAsistente} en '{evento.Nombre}'");
    Console.WriteLine($"📧 Enviado a: {inscripcion.Asistente.Email}");
    Console.ReadKey();
}

static void SubmenuOradores()
{
    new ConsoleMenu()
        .Add("Crear Orador", GestorOradores.CrearDesdeConsola)
        .Add("Listar Oradores", () =>
        {
            GestorOradores.ListarDesdeConsola();
            Console.ReadKey();
        })
        .Add("Eliminar Orador", GestorOradores.EliminarDesdeConsola)
        .Add("Volver", ConsoleMenu.Close)
        .Show();
}

static void SubmenuAsistentes()
{
    new ConsoleMenu()
        .Add("Crear Asistente", GestorAsistentes.CrearDesdeConsola)
        .Add("Listar Asistentes", () =>
        {
            GestorAsistentes.ListarDesdeConsola();
            Console.ReadKey();
        })
        .Add("Eliminar Asistente", GestorAsistentes.EliminarDesdeConsola)
        .Add("Volver", ConsoleMenu.Close)
        .Show();
}

static void SubmenuEspacios()
{
    new ConsoleMenu()
        .Add("Crear Espacio", GestorEspacios.CrearDesdeConsola)
        .Add("Listar Espacios", () =>
        {
            GestorEspacios.ListarDesdeConsola();
            Console.ReadKey();
        })
        .Add("Eliminar Espacio", GestorEspacios.EliminarDesdeConsola)
        .Add("Volver", ConsoleMenu.Close)
        .Show();
}
