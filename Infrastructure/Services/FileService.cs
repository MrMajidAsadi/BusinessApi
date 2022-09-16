using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private string _uploadBase = "Upload";

    public FileService(IConfiguration configuration)
    {
        _uploadBase = configuration.GetSection("UploadDirectoryName").Value;
    }

    public Task Delete(string virtualPath)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Upload(Stream file, string format, string suggestedFileName = "")
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\{_uploadBase}";
        if (!Directory.Exists(uploadDirectory))
            Directory.CreateDirectory(uploadDirectory);

        var fileName = $"{suggestedFileName}{format}";

        var filePath = Path.Combine(uploadDirectory, fileName);

        var num = 1;
        while (File.Exists(filePath))
        {
            fileName = $"{suggestedFileName}-{num}.{format}";
            filePath = Path.Combine(uploadDirectory, fileName);
            num++;
        }

        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }
        
        return $"{_uploadBase}/{fileName}";
    }
}