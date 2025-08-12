namespace ChigaFlix.API.Requests;

public record CategoriasRequestEdit(int id, string titulo, string cor) : CategoriesRequest(id, titulo, cor);
