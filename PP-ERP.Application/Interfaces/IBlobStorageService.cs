using PP_ERP.DTO.BaseDTO;

namespace PP_ERP.Application.Interfaces
{
    public interface IBlobStorageService
    {
        // Upload
        Task<BASE_AZURE_BLOB> UploadPrivateFileAsync(Stream fileStream, string fileName, string? folder = null);
        Task<BASE_AZURE_BLOB> UploadPublicFileAsync(Stream fileStream, string fileName, string? folder = null);

        // Get URL
        string GetPrivateFileUrl(string fileName, int expiryHours = 24);
        string GetPublicFileUrl(string fileName, bool useCdn = true);

        // Delete
        Task<bool> DeletePrivateFileAsync(string fileName);
        Task<bool> DeletePublicFileAsync(string fileName);

        // List
        Task<List<string>> ListFilesAsync(string containerName, string? folder = null);
    }
}
