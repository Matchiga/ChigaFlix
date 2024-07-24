namespace AluraFlix.API.Responses;
public record VideosResponse(int Id, string Titulo,string Url, string? Descricao, int CategoriaId);