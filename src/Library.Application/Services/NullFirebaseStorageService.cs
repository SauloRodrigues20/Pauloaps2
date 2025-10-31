using Library.Application.Interfaces;

namespace Library.Application.Services;

public class NullFirebaseStorageService : IFirebaseStorageService
{
    public Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        throw new InvalidOperationException("Firebase Storage não está configurado. Configure as credenciais do Firebase para fazer upload de imagens.");
    }

    public Task<bool> DeleteFileAsync(string fileUrl)
    {
        // Silently ignore deletion when Firebase is not configured
        return Task.FromResult(true);
    }

    public Task<string> GetFileUrlAsync(string fileName)
    {
        return Task.FromResult(string.Empty);
    }
}
