using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Businesses;

public class UpdateBusinessDto
{
    public int Id { get; set; }

    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}