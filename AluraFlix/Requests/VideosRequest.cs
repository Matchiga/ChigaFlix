using System.ComponentModel.DataAnnotations;

namespace AluraFlix.API.Requests;
public record VideosRequest([Required] string titulo, [Required] string url, string? descricao, int id);
