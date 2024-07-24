namespace AluraFlix.API.Requests;

public record CategoriasRequestEdit(int id, string titulo, string cor) : CategoriasRequest(id, titulo, cor);
