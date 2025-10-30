namespace Library.Application.Interfaces;

public interface IFirebaseStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<bool> DeleteFileAsync(string fileUrl);
    Task<string> GetFileUrlAsync(string fileName);
}
