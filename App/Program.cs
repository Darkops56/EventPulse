using ConsoleTools;
using Entidades;
using Gestores;

Console.ForegroundColor = ConsoleColor.DarkBlue;
var menu = new ConsoleMenu()
          .Configure(c =>
            {
                c.Title = "Gestores del Evento";
                c.EnableWriteTitle = true;
                c.WriteHeaderAction = () => Console.WriteLine("Elige una opcion:");
            })
            .Add("Gestionar Eventos", SubmenuEventos)
            .Add("Gestionar Oradores", SubmenuOradores)
            .Add("Gestionar Asistentes", SubmenuAsistentes)
            .Add("Gestionar Espacios", SubmenuEspacios)
            .Add("Salir", ConsoleMenu.Close);

menu.Show();
Console.ResetColor();

static void SubmenuEventos()
{
    new ConsoleMenu()
        .Configure(c =>
                {
                    c.Title = "Eventos";
                    c.EnableWriteTitle = true;
                    c.WriteHeaderAction = () => Console.WriteLine("Elige una opcion:");
                })
        .Add("Crear Evento", GestorEventos.CrearDesdeConsola)
        .Add("Listar Eventos", () =>
        {
            GestorEventos.ListarDesdeConsola();
            Console.ReadKey();
        })
        .Add("Eliminar Evento", GestorEventos.EliminarDesdeConsola)
        .Add("Asignar Asistente a Evento", AsignarAsistenteEvento)
        .Add("Asignar Orador a Evento", AsignarOradorEvento)
        .Add("Asignar Espacio a Evento", AsignarEspacioEvento)
        .Add("Registrar Inscripción", GestorEventos.RegistrarInscripcionDesdeConsola)
        .Add("Asignar Espacio Prioritario (Conferencia)", AsignarEspaciosConferencia)
        .Add("Distribuir Materiales (Taller)", DistribuirMaterialesTaller)
        .Add("Generar Plano de Feria", GenerarPlanoFeria)
        .Add("Emitir Certificado", EmitirCertificado)
        .Add("Volver", ConsoleMenu.Close)
        .Show();
}
static void AsignarAsistenteEvento()
{
    if (!GestorEventos.ListarDesdeConsola())
        return;
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("Nombre del evento: ");
    string nombreEvento = Console.ReadLine();
    if (!GestorAsistentes.ListarDesdeConsola())
        return;
    Console.Write("Nombre del asistente: ");
    string nombreAsistente = Console.ReadLine();

    var asistente = GestorAsistentes.ListarTodos().FirstOrDefault(a => a.NombreCompleto.Equals(nombreAsistente, StringComparison.OrdinalIgnoreCase));
    if (asistente == null)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Asistente no encontrado.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }

    GestorEventos.AsignarAsistente(nombreEvento, asistente);
    Console.WriteLine("Asistente asignado correctamente.");
    Console.ResetColor();
    Console.ReadKey();
    return;
}

static void AsignarOradorEvento()
{
    if (!GestorEventos.ListarDesdeConsola())
        return;
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("Nombre del evento: ");
    string nombreEvento = Console.ReadLine();
    if (!GestorOradores.ListarDesdeConsola())
        return;
    Console.Write("Nombre del orador: ");
    string nombreOrador = Console.ReadLine();

    var orador = GestorOradores.ListarOradores().FirstOrDefault(o => o.NombreCompleto.Equals(nombreOrador, StringComparison.OrdinalIgnoreCase));
    if (orador == null)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Orador no encontrado.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }
    var evento = GestorEventos.ObtenerEvento(nombreEvento);
    if (!orador.Especialidad.ToLower().Equals(evento.Tipo.ToLower(), StringComparison.OrdinalIgnoreCase))
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"No se puede asignar el orador: su especialidad es '{orador.Especialidad}' y el evento es de tipo '{evento.Tipo}'.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }

    GestorEventos.AsignarOrador(nombreEvento, orador);
    Console.WriteLine("Orador asignado correctamente.");
    Console.ResetColor();
    Console.ReadKey();
    return;
}

static void AsignarEspacioEvento()
{
    if (!GestorEventos.ListarDesdeConsola())
        return;
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("Nombre del evento: ");
    string nombreEvento = Console.ReadLine();
    if (!GestorEspacios.ListarDesdeConsola())
        return;
    Console.Write("Nombre del espacio: ");
    string nombreEspacio = Console.ReadLine();

    var espacio = GestorEspacios.ListarTodos().FirstOrDefault(e => e.Nombre.Equals(nombreEspacio, StringComparison.OrdinalIgnoreCase)); ;
    if (espacio == null)
    {
        Console.ForegroundColor= ConsoleColor.Magenta;
        Console.WriteLine("Espacio no encontrado.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }

    GestorEventos.AsignarEspacio(nombreEvento, espacio);
    Console.WriteLine("Espacio asignado correctamente.");
    Console.ResetColor();
    Console.ReadKey();
    return;
}

static void AsignarEspaciosConferencia()
{

    if (!GestorEventos.ListarDesdeConsola())
        return;

    Console.Write("Nombre del evento de tipo conferencia: ");
    var nombre = Console.ReadLine();
    var evento = GestorEventos.ListarEventos().OfType<Conferencia>()
        .FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

    if (evento == null)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Conferencia no encontrada.");
        Console.ResetColor();
        Console.ReadKey();
    }
    else
    {
        var disponibles = GestorEspacios.ListarTodos();
        if (!GestorEspacios.ListarDesdeConsola())
            return;

        evento.AsignarEspacioConPrioridad(disponibles);
        Console.WriteLine("Espacios asignados con prioridad a equipamiento técnico.");
        Console.ReadKey();
    }
}

static void DistribuirMaterialesTaller()
{
    if (!GestorEventos.ListarDesdeConsola())
        return;

    Console.Write("Nombre del Evento de tipo Taller: ");
    var nombre = Console.ReadLine();

    var evento = GestorEventos.ListarEventos()
        .FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

    if (evento is not Taller taller)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("El evento no es un Taller. Esta función no está disponible.");
        Console.ResetColor();
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
    if (!GestorEventos.ListarDesdeConsola())
        return;

    Console.Write("Nombre del evento de tipo Feria: ");
    var nombre = Console.ReadLine();

    var evento = GestorEventos.ListarEventos()
        .FirstOrDefault(e => e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

    if (evento == null)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Evento no encontrado.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }

    if (evento is not Feria feria)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("El evento no es una Feria. Esta función no está disponible.");
        Console.ResetColor();
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
    if (!GestorEventos.ListarDesdeConsola())
        return;

    Console.Write("Nombre del evento de tipo Taller o Conferencia: ");
    var nombreEvento = Console.ReadLine();

    var evento = GestorEventos.ListarEventos()
        .FirstOrDefault(e => e.Nombre.Equals(nombreEvento, StringComparison.OrdinalIgnoreCase));

    if (evento == null)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Evento no encontrado.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }

    if (evento is not Taller && evento is not Conferencia)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Este evento no emite certificados.");
        Console.ResetColor();
        Console.ReadKey();
        return;
    }
    if (GestorAsistentes.ListarTodos().Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("No hay asistentes");
        Console.ResetColor();
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
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("El asistente no está inscrito en este evento.");
        Console.ResetColor();
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
        .Configure(c =>
                {
                    c.Title = "Oradores";
                    c.EnableWriteTitle = true;
                    c.WriteHeaderAction = () => Console.WriteLine("Elige una opcion:");
                })
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
        .Configure(c =>
                {
                    c.Title = "Asistentes";
                    c.EnableWriteTitle = true;
                    c.WriteHeaderAction = () => Console.WriteLine("Elige una opcion:");
                })
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
        .Configure(c =>
                {
                    c.Title = "Espacios";
                    c.EnableWriteTitle = true;
                    c.WriteHeaderAction = () => Console.WriteLine("Elige una opcion:");
                })
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
