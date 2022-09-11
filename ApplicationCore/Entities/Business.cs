using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities;

public class Business : BaseEntity, IAggregateRoot
{
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<Picture> Pictures { get; private set; } = new();

    public Business() { }

    public Business(
        string userId,
        string name,
        string description,
        List<Picture>? pictures = null)
    {
        UserId = userId;
        Name = name;
        Description = description;
        
        if (pictures is not null)
            Pictures = pictures;
    }
}