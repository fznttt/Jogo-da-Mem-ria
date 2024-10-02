public class Carta
{
    public string Valor { get; set; }
    public bool Revelada { get; set; }

    public Carta(string valor)
    {
        Valor = valor;
        Revelada = false;
    }
}
