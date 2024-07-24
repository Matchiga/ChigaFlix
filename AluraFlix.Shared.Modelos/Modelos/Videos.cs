using System.ComponentModel.DataAnnotations.Schema;

namespace AluraFlix.Modelos;
public class Videos
{
    public Videos()
    {
    }

    public Videos(string url, string? descricao, string titulo)
    {
        Url = url;
        Descricao = descricao;
        Titulo = titulo;
    }

    public string Url { get; set; }
    public string? Descricao { get; set; }
    public string Titulo { get; set; }
    public int Id { get; set; }
    [ForeignKey("Categorias")]
    public int CategoriasId { get; set; } 
    public Categorias? Categorias { get; set; }
    public override string ToString()
    {
        return $", Id: {Id}, Url: {Url}, Descrição: {Descricao}, Titulo: {Titulo}";
    }
}
