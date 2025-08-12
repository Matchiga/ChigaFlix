using System.ComponentModel.DataAnnotations;

namespace ChigaFlix.API.Responses;

public record CategoriesResponse(int id, [Required]string title, [Required]string color);
