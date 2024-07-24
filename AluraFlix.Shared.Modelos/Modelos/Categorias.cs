namespace AluraFlix.Modelos;

public class Categorias
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Cor { get; set; }
    public List<Videos> Videos { get; set; } = new List<Videos>();
    public Categorias(int id, string titulo, string cor)
    {
        Id = id;
        Titulo = titulo;
        Cor = cor;
    }

    public override string ToString()
    {
        return $", Id: {Id}, Titulo: {Titulo}, Cor: {Cor}";
    }
}
