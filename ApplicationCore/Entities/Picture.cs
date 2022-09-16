using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities;

public class Picture : BaseEntity, IAggregateRoot
{
    public int BusinessId { get; private set; }
    public string MimeType { get; private set; }
    public string VirtualPath { get; private set; }
    public string? SeoFileName { get; private set; }
    public string? AltAttribute { get; private set; }
    public string? TitleAttribute  { get; private set; }

    public Picture(
        string mimeType,
        string virtualPath,
        string seoFileName,
        string altAttribute,
        string titleAttribute)
    {
        MimeType = mimeType;
        SeoFileName = seoFileName;
        AltAttribute = altAttribute;
        TitleAttribute = titleAttribute;
        VirtualPath = virtualPath;
    }
}