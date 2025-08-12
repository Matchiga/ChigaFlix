namespace ChigaFlix.API.Responses;
public record VideosResponse(int Id, string Title,string Url, string? Description, int CategoriesId);