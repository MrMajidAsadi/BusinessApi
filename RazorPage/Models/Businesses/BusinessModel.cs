using RazorPage.Models.Pictures;

namespace RazorPage.Models.Businesses;

public class BusinessModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<PictureModel> Pictures { get; set; }
}