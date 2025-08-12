namespace ChigaFlix.Models;

public class Categories
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public List<Videos> Videos { get; set; } = new List<Videos>();
    public Categories(int id, string title, string color)
    {
        Id = id;
        Title = title;
        Color = color;
    }

    public override string ToString()
    {
        return $", Id: {Id}, Title: {Title}, Color: {Color}";
    }
}
