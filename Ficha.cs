using Spectre.Console;

public class Ficha
{ 
    public string Nombre;
    public int Velocidad;  //se modifica en dependencia del efecto
    public string Habilidad;
    public int Enfriamiento;
    public int enfriamientoActual;
    public int efectoDuracion; // Duración del efecto en turnos
    private int velocidadOriginal; // guarda el valor fijo de la velocidad original
    public int PosActualX;
    public int PosActualY;
    

    public Ficha(string nombre, int velocidad, string habilidad, int enfriamiento, int posX, int posY)
    {
        Nombre = nombre;
        Velocidad = velocidad;  //guarda la velocidad original pero sera modificada en dependencia e lo que le pase a la ficha
        velocidadOriginal = velocidad; // Guardar la velocidad original
        Habilidad = habilidad;
        Enfriamiento = enfriamiento;
        enfriamientoActual = 0;
        efectoDuracion = 0;
        PosActualX = posX;
        PosActualY = posY;
    }

    public void UsarHabilidadDelPlayer(Tablero tablero, int objetivoX, int objetivoY)
    {
        if (enfriamientoActual <= 0)
        {
            // Verifica si la casilla objetivo sea adyacente, sin embargo ya no es 100%  necesario
            if (Math.Abs(objetivoX - PosActualX) <= 1 && Math.Abs(objetivoY - PosActualY) <= 1)
            {
                switch (Habilidad)
                {
                    case "Destrucción":
                        if (tablero.MatrizObstaculos[objetivoX, objetivoY])  //objetivoxx y objetivoy creare un metodo mas general para seleccionar donde usar la hab(clase juego)
                        {   
                            Console.Clear();
                            tablero.DestruirObstaculo(objetivoX, objetivoY);
                            AnsiConsole.MarkupLine($"[green]Cyborg destruye el obstáculo seleccionado. Presiona cualquier tecla para continuar)[/]");
                            Console.ReadKey();
                            enfriamientoActual=Enfriamiento;
                        }
                        else
                        {
                            Console.Clear();
                            AnsiConsole.MarkupLine($"[red]No hay obstáculo en esa posicion. Presiona cualquier tecla para continuar[/]");
                            Console.ReadKey();
                        }
                        break;

                    case "Magia Negra":
                        Ficha FichaAfectadaPorMagia = SeleccionarFicha(tablero); //elegir la ficha
                        if (FichaAfectadaPorMagia != null)
                        {
                            FichaAfectadaPorMagia.Velocidad =0;
                            FichaAfectadaPorMagia.efectoDuracion = 2;
                            Console.Clear();
                            AnsiConsole.MarkupLine($"[green]El Judio somete a {FichaAfectadaPorMagia.Nombre} con su Magia Negra y ahora no se puede mover en 2 turnos[/]");
                            Console.WriteLine($"Presiona cualquier tecla para continuar");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"No hay nadie a quien realentizar. Presiona cualquier tecla para continuar"); // esto no pasa desde que actualizaste 2 a 4
                            Console.ReadKey();
                        }
                        enfriamientoActual=Enfriamiento;
                        break;

                    case "Tomar Prestado":
                        if (tablero.MatrizTrampas[objetivoX, objetivoY] == true)
                        {
                            tablero.MatrizTrampas[objetivoX, objetivoY] = false;
                            Console.Clear();
                            AnsiConsole.MarkupLine($"[green]El ladron toma prestada una trampa. No creo que la devuelva)[/]");
                            Console.WriteLine($"Presiona cualquier tecla para continuar");
                            Console.ReadKey();
                            enfriamientoActual = Enfriamiento;
                        }
                        else
                        {
                            Console.Clear();
                            AnsiConsole.MarkupLine($"[red]No hay trampa en esa posicion para mover. Presiona cualquier tecla para continuar[/]");
                            Console.ReadKey();
                            enfriamientoActual = 0;
                        }
                        break;

                    case "Hechizo de Novato":
                        Random randomHechizo = new Random();
                        int resultado = randomHechizo.Next(2); // 0 o 1

                        if (resultado == 0)
                        {
                            foreach (Ficha ficha in tablero.MatrizFichas)
                            {
                                if (ficha != null && ficha != this) // No comerse su propio efecto
                                {
                                    if (ficha.Velocidad > 0) // Verificar velocidad
                                    {
                                        ficha.Velocidad -= 1; // Reducir velocidad
                                        ficha.efectoDuracion = 2; // Aplicar durante 2 turnos
                                        Console.Clear();
                                        AnsiConsole.MarkupLine($"[green]{ficha.Nombre} es ralentizado por 2 turnos. Presiona cualquier tecla para continuar.[/]");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        AnsiConsole.MarkupLine($"[red]{ficha.Nombre} ya tiene velocidad 0, no se puede reducir más. Presiona cualquier tecla para continuar[/]");
                                        Console.ReadKey();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Velocidad > 0) // Verificar velocidad
                            {
                                Velocidad -= 1; // Reducir velocidad
                                efectoDuracion = 2; // Aplicar durante 2 turnos
                                Console.Clear();
                                AnsiConsole.MarkupLine($"[red]{Nombre} se ralentiza a sí mismo por 2 turnos. Presiona cualquier tecla para continuar.[/]");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"{Nombre} ya tiene velocidad 0, no se puede reducir más. Presiona cualquier tecla para continuar");
                                Console.ReadKey();
                            }
                            enfriamientoActual=Enfriamiento;
                        }
                        break;

                    case "Gas Alemán":
                        Velocidad += 2; // Aumenta la velocidad en 2
                        efectoDuracion = 2; // El efecto dura 2 turnos
                        Console.Clear();
                        AnsiConsole.MarkupLine($"[green]{Nombre} aumenta su velocidad a {Velocidad} por 2 turnos. Presiona cualquier tecla para continuar[/]");
                        Console.ReadKey();
                        enfriamientoActual = Enfriamiento;
                        break;
                }
                
            }
            else
            {
                Console.WriteLine($"La casilla ({objetivoX}, {objetivoY}) no está adyacente a la ficha");
            }
        }
        else
        {
            Console.WriteLine($"La habilidad de {Nombre} está en enfriamiento por {enfriamientoActual} turnos más");
        }
    }
    public void Actualizar()
    {
        if (enfriamientoActual > 0)  //ningun enfriamiento ni duracion de ningun efecto son eternos, por tanto --
        {
            enfriamientoActual--;
        }

        if (efectoDuracion > 0)
        {
            efectoDuracion--;
            if (efectoDuracion == 0)
            {
                Velocidad = velocidadOriginal; // Restaurar la velocidad original después de que el efecto expire
            }
        }
    }
 private Ficha SeleccionarFicha(Tablero tablero)//metodito para buscar las fichas en el tablero
{
    List<Ficha> fichasEnTablero = new List<Ficha>();
    for (int i = 0; i < tablero.tamano; i++)
    {
        for (int j = 0; j < tablero.tamano; j++)
        {
            if (tablero.MatrizFichas[i, j] != null && tablero.MatrizFichas[i, j] != this)
            {
                fichasEnTablero.Add(tablero.MatrizFichas[i, j]);
            }
        }
    }
    AnsiConsole.MarkupLine("[green]Selecciona la ficha para usar Magia Negra escribiendo su numero:[/]");
    for (int k = 0; k < fichasEnTablero.Count; k++)
    {
        Console.WriteLine($"{k}. {fichasEnTablero[k].Nombre}");
    }

    int numDeLista;
    bool VerSiEsValidaLaEntrada;
    do
    {
        string entrada = Console.ReadLine();
        VerSiEsValidaLaEntrada = int.TryParse(entrada, out numDeLista) 
        && numDeLista >= 0 
        && numDeLista < fichasEnTablero.Count;

        if (!VerSiEsValidaLaEntrada)
        {
            AnsiConsole.MarkupLine("[red]Pon un numero valido.[/]");
        }
    } while (!VerSiEsValidaLaEntrada);

    return fichasEnTablero[numDeLista];
}
 
}
