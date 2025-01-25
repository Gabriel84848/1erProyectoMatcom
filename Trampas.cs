public class Trampa
{
    public string Nombre;
    public string Efecto;
    public int Duracion;
    public bool Visible;

    public Trampa(string nombre, string efecto, int duracion)
    {
        Nombre = nombre;
        Efecto = efecto;
        Duracion = duracion;
        Visible = true; //se inician invisibles //cuando quites la clase trampa tienes que hacer alguna logica para esto
    }

    public void AplicarEfecto(Ficha ficha)
    {
        Console.WriteLine($"Trampa {Nombre} aplicada a {ficha.Nombre}");
        switch (Nombre)
        {
        case "Hechizo Incómodo":
            ficha.Velocidad = 0; // No puede moverse en el siguiente turno
            Console.WriteLine($"{ficha.Nombre} queda atrapado por un turno");
            break;

        case "Trampa para judios":
            ficha.Velocidad -= 1;
            ficha.enfriamientoActual += 2; // Añadir enfriamiento para efectos de 2 turnos
            Console.WriteLine($"{ficha.Nombre} pierde 1 de velocidad por 2 turnos");
            break;

        case "Gas Somnífero":
            ficha.enfriamientoActual += 2; // Añadir enfriamiento para efectos de 2 turnos
            Console.WriteLine($"{ficha.Nombre} no puede usar su habilidad por 2 turnos");
            break;
        }
    }
}
