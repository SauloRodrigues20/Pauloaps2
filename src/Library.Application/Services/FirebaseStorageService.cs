using Google.Cloud.Storage.V1;
using Library.Application.Interfaces;

namespace Library.Application.Services;

public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly string _bucketName;
    private readonly StorageClient _storageClient;

    public FirebaseStorageService(string bucketName)
    {
        _bucketName = bucketName;
        _storageClient = StorageClient.Create();
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        try
        {
            var objectName = $"books/{Guid.NewGuid()}_{fileName}";
            
            await _storageClient.UploadObjectAsync(
                _bucketName,
                objectName,
                contentType,
                fileStream
            );

            // Retorna a URL pública do arquivo
            return $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{Uri.EscapeDataString(objectName)}?alt=media";
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao fazer upload do arquivo: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteFileAsync(string fileUrl)
    {
        try
        {
            // Extrai o nome do objeto da URL
            var uri = new Uri(fileUrl);
            var pathParts = uri.AbsolutePath.Split('/');
            var objectName = Uri.UnescapeDataString(pathParts[^1].Split('?')[0]);

            await _storageClient.DeleteObjectAsync(_bucketName, objectName);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao deletar arquivo: {ex.Message}", ex);
        }
    }

    public Task<string> GetFileUrlAsync(string fileName)
    {
        try
        {
            var objectName = $"books/{fileName}";
            var url = $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{Uri.EscapeDataString(objectName)}?alt=media";
            return Task.FromResult(url);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter URL do arquivo: {ex.Message}", ex);
        }
    }
}
