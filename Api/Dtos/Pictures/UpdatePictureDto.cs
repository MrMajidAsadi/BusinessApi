namespace Api.Dtos.Pictures;

public class UpdatePictureDto
{
    public int Id { get; set; }
    public string? SeoFileName { get; set; }
    public string? AltAttribute { get; set; }
    public string? TitleAttribute { get; set; }
    public IFormFile? Picture { get; set; }
}