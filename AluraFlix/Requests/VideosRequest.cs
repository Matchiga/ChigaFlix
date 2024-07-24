using System.ComponentModel.DataAnnotations;

namespace AluraFlix.API.Requests;
public record VideosRequest(string Titulo, string Url, string? Descricao, int Id, int CategoriasId);
