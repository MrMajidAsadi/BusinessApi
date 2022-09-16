namespace ApplicationCore.Interfaces;

public interface IFileService
{
    Task<string> Upload(Stream file, string format, string suggestedFileName = "");
    Task Delete(string virtualPath);
}