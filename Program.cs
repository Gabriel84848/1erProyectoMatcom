using Spectre.Console;
using Figgle;
class Program
{
    static void Main(string[] args)
    {
        
        MostrarIntroduccion();
        MostrarMenuFichas();
        Partida juego = new Partida();
        juego.Iniciar();
    }
    static void MostrarIntroduccion()
    {
        string lore = @"
        El miedo nos mantiene siempre alerta,mientras que el hambre nos obliga a movernos y luchar por 
        nuestras vidas. A través de los sueños, un ente aparentemente divino transporta a varios seres,
        quienes no parecen coincidir en línea temporal, al interior de las mazmorras del Miedo y el Hambre.
        Este ente les deja claro que, en lo profundo de la mazmorra, está aquello que ellos más desean.
        Asi es como comienzan a aventurarse en la mazmorra, cada cual buscando algo diferente pero todos
        con el objetivo de llegar al fondo.";

        Console.Clear();
        AnsiConsole.Markup("[yellow]" + lore + "[/]");
        Console.WriteLine("\nPresiona cualquier tecla para comenzar...");
        Console.ReadKey();
    }
    static void MostrarMenuFichas()
{
    List<string> fichas = new List<string> { "Cyborg", "Ladrón", "Judío", "Mago Brujo", "Zombie Nazi"};
    int selectedIndex = 0;

    while (true)
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[purple]Seleccione una ficha para ver su información o inice la partida pulsando ESC. (Use las flechas y Enter para confirmar):[/]");

        for (int i = 0; i < fichas.Count; i++)
        {
           if(i== selectedIndex)
            {
                AnsiConsole.MarkupLine($"[green]> {fichas[i]}[/]");
            }
            else
            {
                Console.WriteLine($"  {fichas[i]}");
            }
        }

        var key = Console.ReadKey().Key;

        if (key == ConsoleKey.UpArrow)
        {
            if (selectedIndex > 0)
            {
                selectedIndex--;
            }
        }
        else if (key == ConsoleKey.DownArrow)
        {
            if (selectedIndex < fichas.Count - 1)
            {
                selectedIndex++;
            }
        }
        else if (key == ConsoleKey.Enter)
        {
            MostrarInfoFicha(fichas[selectedIndex]);    
        }
        else if (key == ConsoleKey.Escape)
        {
            break;
        }
    }
}
static void MostrarInfoFicha(string ficha)
{
    Console.Clear();
    switch (ficha)
    {
        case "Cyborg":
            AnsiConsole.Markup("[blue]El Cyborg busca en la mazmorra una fuente de poder inagotable, parece que en el futuro la humanidad sigue[/]");
            AnsiConsole.Markup("\n[blue]dependiendo del petróleo. Hace buen uso de su fuerza bruta para quitarse del camino uno que otro obstaculo.[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Habilidad: Destruye un obstaculo en una posicion adyacente (arriba, abajo, izquierda, derecha).[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Enfriamiento: 4 turnos[/]");
            break;
        case "Ladrón":
            AnsiConsole.Markup("[yellow]Siempre ha pasado trabajo para sobrevivir, y el trabajo le ha devuelto el favor en forma de manos rapidas y un[/]");
            AnsiConsole.Markup("\n[yellow]fuerte sentimiento de apropiarse de todo lo que ve. Siempre se justifica con que solo toma prestada las cosas.[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Habilidad: Hace desaparecer una trampa en una posicion adyacente (arriba, abajo, izquierda, derecha).[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Enfriamiento: 4 turnos.[/]");
            break;
        case "Judío":
            AnsiConsole.Markup("[white]Lograron sobreponerse a uno de los episodios mas oscuros de la historia de la humanidad, sin embargo[/]");
            AnsiConsole.Markup("\n[white]no fue solamente escondiendose y hullendo. Muchos desarrollaron y practicaron artes mágicas oscuras.[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Habilidad: Inmoviliza a un jugador durante 2 turnos.[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Enfriamiento: 6 turnos.[/]");
            break;
        case "Mago Brujo":
            AnsiConsole.Markup("[purple]Un ser antiguo que nunca logro ser mago ni logro dominar las oscuras artes de la brujeria, sin embargo[/]");
            AnsiConsole.Markup("\n[purple]se autoproclama Gran Sabio Mago Brujo. Debido a su incapacidad, la efectividad de sus hechizos es incierta[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Habilidad: 50% de probabilidades de reducir en 1 la velocidad de todos los jugadores enemigos en 1[/]");
            AnsiConsole.Markup("\n[red]y 50% de probabilidades de aplicartelo a ti mismo[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Enfriamiento: 4 turnos[/]");
            break;
        case "Zombie Nazi":
            AnsiConsole.Markup("[green]Por alguna razon ha revivido entre los muertos con un solo objetivo en esta mazmorra, revivir al que para él[/]");
            AnsiConsole.Markup("\n[green]es la mayor personalidad alemana, Godofredo Leibniz. Haciendo usa de sustancias del ejército puede mejorar su físico[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Habilidad: Aumenta su velocidad en 2 durante 2 turnos[/]");
            Console.WriteLine();
            AnsiConsole.Markup("\n[red]Enfriamiento: 4 turnos[/]");
            break;
    }

    Console.WriteLine("\nPresiona ESC para regresar al menú...");
    while (Console.ReadKey().Key != ConsoleKey.Escape) { }
}

}

