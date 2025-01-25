
public class Tablero
{
    public int tamano;
    public bool[,] MatrizObstaculos;
    public bool[,] MatrizVictoria;
    public Trampa[,] MatrizTrampas; 
    public Ficha[,] MatrizFichas; 

    public Tablero()  //contrucctor
    {
        tamano = 30;
        MatrizObstaculos = new bool[tamano, tamano];
        MatrizVictoria = new bool[tamano, tamano];
        MatrizTrampas = new Trampa[tamano, tamano];  //QUITAR CLASE TRAMPA MAS TARDE
        MatrizFichas = new Ficha[tamano, tamano];
    }
    private void InicializarMatrizObstaculos()
{
    string[] patrones = {
        "1.....0...0........0...0.....2",
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
        ".0....0...............0....0.",
        ".0....0...............0....0.",
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
        "3.....0...0........0...0.....4"
    };

    for (int i = 0; i < tamano; i++)
    {
        for (int j = 0; j < tamano; j++)
        {
            if (patrones[i][j] == '0')
            {
                MatrizObstaculos[i, j] = true; //transforma donde halla un 0 a obstaculos
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
                MatrizTrampas[i, j] = null;
                MatrizFichas[i, j] = null;
                if((i==15 && j==15) || (i==16 && j==15) || (i==15 && j==16) || (i==16 && j==16))
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
        VisionDeTrampas();
    }

    public void desColocarObstaculos()
    {
        Random random = new Random();
        int numObstaculos = 20;       //ajustar el num de obstaculos

        for (int i = 0; i < numObstaculos; i++)   //una iteracion por cada numObstaculos
        {
            int fila;
            int columna;
            do                    //se ejecuta siempre la primera vez, da igual cualquier cosa
            {
                fila = random.Next(tamano);
                columna = random.Next(tamano);
            } while (MatrizObstaculos[fila, columna]);  //si es true (hay ya un obsta) se vuelve a repetir el bucle, si no sale del bucle

            MatrizObstaculos[fila, columna] = false;  
        }
    }

    private void ColocarTrampas()   //teienes que hacer que las trampas no sean visibles cuando se genere el tablero, o solo hacerlas visibles para el ladron y el judio
    {
        Random random = new Random(); 
        int numTrampas = 25;

        for (int i = 0; i < numTrampas; i++)
        {
            int fila;
            int columna;
            do
            {
                fila = random.Next(tamano);
                columna = random.Next(tamano);
            } while (MatrizObstaculos[fila, columna] || MatrizTrampas[fila, columna] != null); // || con el o nos asiguuramos que nama que una de las dos sea true, pa atras

            int tipoTrampa = random.Next(3);
            switch (tipoTrampa)
            {
                case 0:
                    MatrizTrampas[fila, columna] = new Trampa("Hechizo Incómodo", "Queda atrapado por un turno", 1);    //aqui se crean(inicializan) las 3 trampas (objetos), no es necesario Trampa trampas, debido a que trampas ya fue declarado en el constructor. En las coordenadas trampas[fila, columna] se creara un objeto de tipo Trampa
                    break;
                case 1:
                    MatrizTrampas[fila, columna] = new Trampa("Trampa para judios", "Tu velocidad se reduce en 1 por 2 turnos", 2);
                    break;
                case 2:
                    MatrizTrampas[fila, columna] = new Trampa("Gas Somnífero", "No puede usar su habilidad por 2 turnos", 2);
                    break;
            }
        }
    }
    private void VisionDeTrampas()
{
    for (int i = 0; i < tamano; i++)
    {
        for (int j = 0; j < tamano; j++)
        {
            if (MatrizTrampas[i, j] != null) 
            {
                MatrizTrampas[i, j].Visible = true; //hace que las trampas inicialmente esten invisibles
                        //arriba                                        //abajo
                if ((i > 0 && MatrizFichas[i - 1, j] != null) || (i < tamano - 1 && MatrizFichas[i + 1, j] != null) ||
                    (j > 0 && MatrizFichas[i, j - 1] != null) || (j < tamano - 1 && MatrizFichas[i, j + 1] != null))
                {       //izquierda                                    //derecha
                    MatrizTrampas[i, j].Visible = true; // si hay alguna adyacente se vuelve visible
                }
            }
        }
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
            VisionDeTrampas();

            if (MatrizTrampas[FilaFinal, ColFinal] != null)
            {
                MatrizTrampas[FilaFinal, ColFinal].AplicarEfecto(MatrizFichas[FilaFinal, ColFinal]);
                MatrizTrampas[FilaFinal, ColFinal] = null; // cuando te comes una trampa se quita
            }
        }
    }
}
