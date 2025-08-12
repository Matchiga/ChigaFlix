using System.ComponentModel.DataAnnotations.Schema;

namespace ChigaFlix.Models;
public class Videos
{
    public Videos()
    {
    }

    public Videos(string url, string? description, string title)
    {
        Url = url;
        Description = description;
        Title = title;
    }

    public string Url { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; }
    public int Id { get; set; }
    [ForeignKey("Categorias")]
    public int CategoriesId { get; set; } 
    public Categories? Categories { get; set; }
    public override string ToString()
    {
        return $", Id: {Id}, Url: {Url}, Description: {Description}, Title: {Title}";
    }
}
