public class Jugador
{
    public string Nombre;
    public Ficha Ficha;   

    public Jugador(string nombre)
    {
        Nombre = nombre;
    }
    public void AsignarFicha(Ficha ficha)
    {
        Ficha = ficha;
        Console.WriteLine($"{ficha.Nombre} asignado a {Nombre}");
    }
}
