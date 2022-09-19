using Api.Dtos.Pictures;

namespace Api.Dtos.Businesses;

#nullable disable
public class BusinessDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PictureDto> Pictures { get; set; }
}