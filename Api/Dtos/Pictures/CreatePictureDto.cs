using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Pictures;

#nullable disable
public class CreatePictureDto
{
    [Required]
    public int BusinessId { get; set; }
    public string SeoFileName { get; set; }
    public string AltAttribute { get; set; }
    public string TitleAttribute { get; set; }
    [Required]
    public IFormFile Picture { get; set; }
}