using AluraFlix.API.Requests;
using AluraFlix.Modelos;

namespace AluraFlix.API.Requests;
public record VideosRequestEdit(string Titulo, string Url, string? Descricao, int Id, int CategoriasId)
: VideosRequest(Titulo, Url, Descricao, Id, CategoriasId);
