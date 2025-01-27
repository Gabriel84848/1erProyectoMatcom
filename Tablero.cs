
public class Tablero
{
    public int tamano;
    public bool[,] MatrizObstaculos;
    public bool[,] MatrizVictoria;
    public bool[,] MatrizTrampas; 
    public Ficha[,] MatrizFichas; 

    public Tablero()  //contrucctor
    {
        tamano = 30;
        MatrizObstaculos = new bool[tamano, tamano];
        MatrizVictoria = new bool[tamano, tamano];
        MatrizTrampas = new bool[tamano, tamano];
        MatrizFichas = new Ficha[tamano, tamano];
    }
private void InicializarMatrizObstaculos()
{
    string[] patrones = {
        "......0...0........0...0......",
        "000.0.00.0000.00.0000.00.0.000",
        "....0.....0........0.....0....",
        ".00.0000000.000000.0000000.00.",
        "..0.0.0.....0.00.0.....0.0.0..",
        ".00.0.0.00000.00.00000.0.0.00.",
        ".0....0.0.0........0.0.0....0.",
        "00.0000.0.0.0.00.0.0.0.0000.00",
        "...0......0.0....0.0......0...",
        ".0.0.0000.0.000000.0.0000.0.0.",
        ".0.0.0..0.0.0.00.0.0.0..0.0.0.",
        ".0.0.00.000.0.00.0.000.00.0.0.",
        ".0.0..0.0.....00.....0.0..0.0.",
        ".0.00.0.0.0000000000.0.0.00.0.",
        ".0....0................0....0.",
        ".0....0................0....0.",
        ".0.00.0.0.0000000000.0.0.00.0.",
        ".0.0..0.0.....00.....0.0..0.0.",
        ".0.0.00.000.0.00.0.000.00.0.0.",
        ".0.0.0..0.0.0.00.0.0.0..0.0.0.",
        ".0.0.0000.0.000000.0.0000.0.0.",
        "...0......0.0....0.0......0...",
        "00.0000.0.0.0.00.0.0.0.0000.00",
        ".0....0.0.0........0.0.0....0.",
        ".00.0.0.00000.00.00000.0.0.00.",
        "..0.0.0.....0.00.0.....0.0.0..",
        ".00.0000000.000000.0000000.00.",
        "....0.....0........0.....0....",
        "000.0.00.0000.00.0000.00.0.000",
        "......0...0........0...0......"
    };

    for (int i = 0; i < tamano; i++)
    {
        for (int j = 0; j < tamano; j++)
        {
            // Ajusta el valor de la matriz de obstáculos según el patrón
            if (patrones[i][j] == '0')
            {
                MatrizObstaculos[i, j] = true;
            }
            else
            {
                MatrizObstaculos[i, j] = false;
            }
        }
    }
}
    public void GenerarTablero()
    {
        InicializarMatrizObstaculos();
        for (int i = 0; i < tamano; i++)
        {
            for (int j = 0; j < tamano; j++)
            {
                MatrizTrampas[i, j] = false;
                MatrizFichas[i, j] = null;
                if((i==14 && j==14) || (i==15 && j==14) || (i==14 && j==15) || (i==15 && j==15))
                {
                    MatrizVictoria[i,j]=true;
                }
                else
                {
                    MatrizVictoria[i,j]=false;
                }
            }
        }
        desColocarObstaculos();
        ColocarTrampas();
    }

    public void desColocarObstaculos()
    {
        Random random = new Random();
        int numObstaculos = 100;       //ajustar el num de obstaculos

        for (int i = 0; i < numObstaculos; i++)   //una iteracion por cada numObstaculos
        {
            int fila;
            int columna;
            do                    //se ejecuta siempre la primera vez, da igual cualquier cosa
            {
                fila = random.Next(tamano);
                columna = random.Next(tamano);
            } while (!MatrizObstaculos[fila, columna]);  //si es true (hay ya un obsta) se vuelve a repetir el bucle, si no sale del bucle

            MatrizObstaculos[fila, columna] = false;  
        }
    }

    private void ColocarTrampas()   //teienes que hacer que las trampas no sean visibles cuando se genere el tablero, o solo hacerlas visibles para el ladron y el judio
    {
        Random random = new Random(); 
        int numTrampas = 60;

        for (int i = 0; i < numTrampas; i++)
        {
            int fila;
            int columna;
            do
            {
                fila = random.Next(tamano);
                columna = random.Next(tamano);
            } while (MatrizObstaculos[fila, columna] || MatrizTrampas[fila, columna]); // || con el o nos asiguuramos que nama que una de las dos sea true, pa atras
            MatrizTrampas[fila, columna] = true;
        }
    }
    public void DestruirObstaculo(int x, int y)
    {
        if (MatrizObstaculos[x, y] == true)
        {
            MatrizObstaculos[x, y] = false;
        }
    }
    public void MoverFicha(int FilaInicio, int ColInicio, int FilaFinal, int ColFinal)  //modifica la posicion de las fichas en el tablero (se llama desde LeerMovimiento)
    {
        if (!MatrizObstaculos[FilaInicio, ColFinal]&& MatrizFichas[FilaFinal, ColFinal] == null)
        {
            MatrizFichas[FilaFinal, ColFinal] = MatrizFichas[FilaInicio, ColInicio];
            MatrizFichas[FilaInicio, ColInicio] = null;
            //actualiza despues de moverse
            
            if (MatrizTrampas[FilaFinal, ColFinal] == true)
            {
                if (MatrizTrampas[FilaFinal, ColFinal] == true)
                {
                    AplicarTrampa(MatrizFichas[FilaFinal, ColFinal]);
                    MatrizTrampas[FilaFinal, ColFinal] = false; // cuando te comes una trampa se quita
                }
            }
        }
    }
    public void AplicarTrampa(Ficha ficha)
    {
        Random random = new Random();
        int tipoTrampa = random.Next(3);
        switch(tipoTrampa)
        {
            case 0:
                ficha.Velocidad = 0;
                Console.WriteLine($"{ficha.Nombre} ha caido en una trampa para judios no podras moverte durante un turno");
                break;
            case 1:
                ficha.Velocidad -= 1;
                ficha.efectoDuracion +=2;
                Console.WriteLine($"{ficha.Nombre} a pisado moco de slime, pierdes 1 de velocidad por 2 turnos.");
                break;
            case 2:
                ficha.enfriamientoActual += 2;
                Console.WriteLine($"{ficha.Nombre} a tocado un artefacto de dudosa procedencia, pierdes tus poderes durante 2 turnos");
                break;            
        }
    }
}