using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraFlix.Modelos;
public class Videos
{
    public string Url { get; set; }
    public string? Descricao { get; set; }
    public string Titulo { get; set; }
    public int Id { get; set; }
    public Videos(string url, string? descricao, string titulo)
    {
        Url = url;
        Descricao = descricao;
        Titulo = titulo;
    }

    public override string ToString()
    {
        return $", Id: {Id}, Url: {Url}, Descrição: {Descricao}, Titulo: {Titulo}";
    }
}
