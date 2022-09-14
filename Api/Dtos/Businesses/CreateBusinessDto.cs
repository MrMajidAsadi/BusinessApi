using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Businesses;

#nullable disable
public class CreateBusinessDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
}