public class Lampada
{
    private bool isLigada;

    public Lampada()
    {
        isLigada = false; 
    }

    public void ligar()
    {
        isLigada = true;
        Console.WriteLine("Está ligada");
    }

    public void desligar()
    {
        isLigada = false;
        Console.WriteLine("Está desligada");
    }

    public bool EstaLigada()
    {
        return isLigada;
    }
}
