using Spectre.Console;

public class Partida
{
    private Tablero tablero;
    private List<Jugador> ListaJugadores; 
    private int numJugadores;

    public Partida()
    {
        numJugadores = 4;
        tablero = new Tablero();
        ListaJugadores = new List<Jugador>();  // crea lista con jugadores
    }
    public void Iniciar()
    {
        Console.Clear();
        CantDeJugadores();
        ConfigurarJuego();

        while (true) // Bucle principal del juego
        {
            MostrarEstado();
            RealizarTurno();
            ActualizarFichas();
            if(VerificarVictoria())
            {
                break;   // sale del bucle si alguna fichha llego al destino
            }
        }

        Console.WriteLine("¡Juego terminado!");
    }
    private bool VerificarVictoria()
    {
        foreach(Jugador jugador in ListaJugadores)
        {
            Ficha ficha = jugador.Ficha;
            if(tablero.MatrizVictoria[ficha.PosActualX, ficha.PosActualY])
            {
                Console.WriteLine($"El {jugador.Nombre} ha ganado encarnando un {ficha.Nombre}");
                return true;
            }
        }
        return false;
    }
    public void CantDeJugadores()
    {
        Console.WriteLine("Ingresa la cantidad de jugadores (máximo 4):");
        int cantidad;
        bool VerSiEsValidaLaEntrada;
        do
        {
            string entrada = Console.ReadLine();
            VerSiEsValidaLaEntrada = int.TryParse(entrada, out cantidad) && cantidad > 0 && cantidad <= 4;

            if (VerSiEsValidaLaEntrada == false)
            {
                Console.WriteLine("Entrada inválida. Por favor, ingresa un número entre 1 y 4:");
            }
        }
        while (!VerSiEsValidaLaEntrada);

        numJugadores = cantidad;
    }

    public void InicializarJugadores()
    {
        for  (int i = 0; i < numJugadores; i++)  // va metiendo a los jugadores segun el numJugadores (CREA LAS INSTANCIAS U OBJETOS JUGADORES)
        {
            Jugador jugador = new Jugador($"Jugador {i+1}");
            ListaJugadores.Add(jugador);
        }
    }
    private void ConfigurarJuego()
    {
        InicializarJugadores();
        // la lista de las fichas del juegp (lista de objetos tipo Ficha)
        List<Ficha> ListaDeFichas = new List<Ficha>
        {
            new Ficha("Cyborg", 3, "Destrucción", 4, -1, -1),
            new Ficha("Ladrón", 3, "Tomar Prestado", 2, -1, -1),
            new Ficha("Judio", 3, "Robar", 2, -1, -1),
            new Ficha("Mago Brujo", 3, "Hechizo de Novato", 4, -1, -1),
            new Ficha("Zombie Nazi", 2, "Gas Alemán", 4, -1, -1)
        };
        tablero.GenerarTablero();
        
        // for principal para aasignar una ficha a cada jugador (1 vez por cada jugador)
        for (int i = 0; i < numJugadores; i++)  
        {
            Console.WriteLine($"Jugador {i + 1}, selecciona una ficha:");
            for (int j = 0; j < ListaDeFichas.Count; j++)
            {
                Console.WriteLine($"{j}. {ListaDeFichas[j].Nombre}"); //muestra las fichas para seleeccionar
            }

            int NumSeleccionado; // Inicializar seleccion con un valor por defecto 0
            bool VerSiEsValidaLaEntrada;  // se inicializa en false

            do
            {
                Console.WriteLine("Selecciona una ficha por su numero en la lista:");
                string entrada = Console.ReadLine();
                //el TryParse trata de convertir la entreda a entero, si lo logra VerSiEsValidaLaEntrada pasa atrue y out le pasa el entero a NumSeleccionado
                VerSiEsValidaLaEntrada = int.TryParse(entrada, out  NumSeleccionado) && NumSeleccionado >= 0 && NumSeleccionado < ListaDeFichas.Count;
                
                if (VerSiEsValidaLaEntrada == false) //se ejecuta si lo que introducen es invalido
                {
                    Console.WriteLine("entrada invalida. Usa los numeros de la lista para seleccionar tu ficha");
                }
            }
            while (!VerSiEsValidaLaEntrada); // si VerSiEsValidaLaEntrada es false ent el while toma true y se repite el bucle
            
            Ficha ficha = ListaDeFichas[NumSeleccionado];   // asigna a ficha, la que selecciono el jugador 
            ListaDeFichas.RemoveAt(NumSeleccionado); // eliminar la ficha seleccionada para que no se repita usando RemoveaAt (metodo de List)

            int coordX=0;
            int coordY=0;
            switch (i)
            {
                case 0:
                coordX=0;
                coordY=0;
                break;
                case 1:
                coordX=0;
                coordY=29;
                break;
                case 2:
                coordX=0;
                coordY=29;
                break;
                case 3:
                coordX=0;
                coordY=29;
                break;
            }
            ficha.PosActualX = coordX;
            ficha.PosActualY = coordY;
            tablero.MatrizFichas[ficha.PosActualX, ficha.PosActualY] = ficha;   // colocar la ficha en la matriz de fichas segun la caracteristica de la ficha
            ListaJugadores[i].AsignarFicha(ficha);  // accede al jugador actual (segun el for) de la lista de jugadores y le agrega la ficha seleccionada
        }
        
    }
    private void MostrarEstado()
    {
        int PosX = 24;
        int PosY = 12;
        Console.WriteLine();
        for (int i = 0; i < tablero.tamano; i++)
        {
            for (int j = 0; j < tablero.tamano; j++)
            {
                if(tablero.MatrizVictoria[i,j])
                {
                    AnsiConsole.Markup("[green]$[/] ");
                }
                else if (tablero.MatrizFichas[i, j] != null)
                {   //ablero.MatrizFichas[i, j] accede a la ficha en esa posicion. tablero.MatrizFichas[i, j].Nombre  muestra el nombre de la ficha
                    AnsiConsole.Markup($"[blue]{tablero.MatrizFichas[i, j].Nombre.Substring(0, 1)}[/] ");     //substring sirve para sacar un substring de un string. en este caso 0 indica que sera el primer caracter y 1 que la longitud del substing es de 1 
                }                                                                         // + " " hace que la primera letra del Nombre de la ficha se muestre
                else if (tablero.MatrizObstaculos[i, j])
                {
                    AnsiConsole.Markup("[yellow]O[/] ");
                }
                else if (tablero.MatrizTrampas[i, j])
                {
                    AnsiConsole.Markup("[red]T[/] ");
                }
                else
                {
                    AnsiConsole.Markup("[grey].[/] ");
                }
            }
            Console.WriteLine();
        }
    }
   private void RealizarTurno()
    {
        for (int i = 0; i < numJugadores; i++)
        {
            Console.WriteLine($"Turno del {ListaJugadores[i].Nombre}:");
            Ficha ficha = ListaJugadores[i].Ficha; // acceder a la ficha ya asigndada al jugador en cuestion  // Ficha ficha se usa para crear una variable local que guarde la referencia solop para este metodo (se usa ficha en vez de ListaJugadores[i].Ficha)
        
            string respuesta;
            do
            {
                Console.WriteLine("Quieres usar tu habilidad antes de moverte? (SI/NO)");
                respuesta = Console.ReadLine().ToUpper();

                if(respuesta != "SI"  && respuesta != "NO")
                {
                    Console.WriteLine("Respuesta invalida. Que te cuesta decir SI o NO?");
                }
            }
            while(respuesta  != "SI" && respuesta != "NO");

            if (respuesta == "SI")
            {
                UsarHabilidad(ficha, tablero);
            }

            // mecanica de que el movimiento se ejecute uno a uno en deppendeencia de la velocidad de la ficha
            for (int m = ficha.Velocidad; m > 0; m--)
            {
                Console.WriteLine($"Te quedan {m} movimientos.");
                MostrarEstado();
                // Llama al metodo que mueve la ficha
                if(!LeerMovimiento(ficha, tablero))
                {
                    m++; // si fallo algo devuelve una m para que no se coma el turno
                }
                
            }
        }
    }
    private void UsarHabilidad(Ficha ficha, Tablero tablero)
{
    switch(ficha.Habilidad)
    {
        case "Destrucción":
        case "Tomar Prestado":
        case "Robar":
            // Las habilidades que requieren una posicion especifica
            (int objetivoX, int objetivoY) = LeerCoordenadasHabilidad(ficha);
            ficha.UsarHabilidadDelPlayer(tablero, objetivoX, objetivoY);
            break;

        case "Hechizo de Novato":
        case "Gas Alemán":
            // Las habilidades que no requieren una posicion especifica
            ficha.UsarHabilidadDelPlayer(tablero, ficha.PosActualX, ficha.PosActualY); // Pasar las coordenadas actuales de la ficha
            break;
    }
} 
private bool LeerMovimiento(Ficha ficha, Tablero tablero)
{
    var direccion = Console.ReadKey(true).Key;
    int DirX;  //inicializar, si no se jode
    int DirY;

    switch (direccion)
    {
        case ConsoleKey.UpArrow:
            DirX = -1;
            DirY = 0;
            break;
        case ConsoleKey.DownArrow:
            DirX = 1;
            DirY = 0;
            break;
        case ConsoleKey.LeftArrow:
            DirX = 0;
            DirY = -1;
            break;
        case ConsoleKey.RightArrow:
            DirX = 0;
            DirY = 1;
            break;
        default:  //si es invalida muestra el mensaje de error y llaama otra vez el metodo
            Console.WriteLine("Dirección inválida. Por favor, inténtalo de nuevo.");
            return LeerMovimiento(ficha, tablero); // Llamada recursiva para intentarlo de nuevo
    }

    // Calcular la nueva posicion
    int nuevoX = ficha.PosActualX + DirX;
    int nuevoY = ficha.PosActualY + DirY;

    // Verificar si la nueva posicion esta dentro del tablero y no es un obstaculo
    if (nuevoX >= 0 && nuevoX < tablero.tamano && nuevoY >= 0 && nuevoY < tablero.tamano && !tablero.MatrizObstaculos[nuevoX, nuevoY])
    {
        // Mover la ficha a la nueva posicion con el metodo de tablero
        tablero.MoverFicha(ficha.PosActualX, ficha.PosActualY, nuevoX, nuevoY);

        // Actualiza la posición de la ficha
        ficha.PosActualX = nuevoX;
        ficha.PosActualY = nuevoY;

    
        return true; // Movimiento valido
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Estas intentado salir del tablero o comerte un obstaculo. Intentalo otra vez");
        return false; // Movimiento invalido
    }
}
private (int objetivoX, int objetivoY) LeerCoordenadasHabilidad(Ficha ficha)
{
    Console.WriteLine("Selecciona la ficha usando");
    var direccion = Console.ReadKey(true).Key;
    int DirX;  //inicializar, si no se jode
    int DirY;

    switch (direccion)
    {
           case ConsoleKey.UpArrow:
            DirX = -1;
            DirY = 0;
            break;
        case ConsoleKey.DownArrow:
            DirX = 1;
            DirY = 0;
            break;
        case ConsoleKey.LeftArrow:
            DirX = 0;
            DirY = -1;
            break;
        case ConsoleKey.RightArrow:
            DirX = 0;
            DirY = 1;
            break;
        default:
            Console.WriteLine("LAS FLECHAS DIRECCIONALES DEL TECLADO!!!. Intentalo otra vez");
            return LeerCoordenadasHabilidad(ficha); //intentarlo de nuevo
    }
    // Calcular las coordenadas objetivo
    int objetivoX = ficha.PosActualX + DirX;
    int objetivoY = ficha.PosActualY + DirY;

    // Verificar que las coordenadas objetivo estén dentro del tablero
    if (objetivoX >= 0 && objetivoX < tablero.tamano && objetivoY >= 0 && objetivoY < tablero.tamano)
    {
        return (objetivoX, objetivoY);
    }
    else
    {
        Console.WriteLine("Quieres usar la habilidad fuera del tablero?. Mira a ver lo que estas haciendo e intentalo otra vez");
        return LeerCoordenadasHabilidad(ficha);
    }
}
    private void ActualizarFichas()    //revisar
    {
        foreach (Jugador jugador in ListaJugadores)
        {
            jugador.Ficha.Actualizar();
        }
    }
}
