using ChigaFlix.API.Requests;
using ChigaFlix.Models;

namespace ChigaFlix.API.Requests;
public record VideosRequestEdit(string Title, string Url, string? Description, int Id, int CategoriesId)
: VideosRequest(Title, Url, Description, Id, CategoriesId);
