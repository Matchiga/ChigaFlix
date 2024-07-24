using System.ComponentModel.DataAnnotations;

namespace AluraFlix.API.Responses;

public record CategoriasResponse(int id, [Required]string titulo, [Required]string cor);
