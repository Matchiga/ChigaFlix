using AluraFlix.API.Requests;

namespace AluraFlix.API.Requests;
public record VideosRequestEdit(string titulo, string descricao, string url, int id)
: VideosRequest(titulo, url, descricao, id);
