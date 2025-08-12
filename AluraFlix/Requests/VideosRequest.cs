using System.ComponentModel.DataAnnotations;

namespace ChigaFlix.API.Requests;
public record VideosRequest(string Title, string Url, string? Description, int Id, int CategoriesId);
