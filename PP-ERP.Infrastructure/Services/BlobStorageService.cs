using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PP_ERP.Application.Interfaces;
using PP_ERP.DTO.BaseDTO;
using PP_ERP.Infrastructure.Options;

namespace PP_ERP.Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AzureStorageOptions _options;

        public BlobStorageService(IOptions<AzureStorageOptions> options)
        {
            _options = options.Value;
            _blobServiceClient = new BlobServiceClient(_options.ConnectionString);
        }

        public async Task<BASE_AZURE_BLOB> UploadPublicFileAsync(Stream fileStream, string fileName, string? folder = null)
        {
            var blobName = string.IsNullOrEmpty(folder) ? fileName : $"{folder}/{fileName}";
            var containerClient = _blobServiceClient.GetBlobContainerClient(_options.PublicContainer);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(blobName);

            var uploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    CacheControl = "public, max-age=2592000",
                    ContentType = GetContentType(fileName)
                }
            };

            await blobClient.UploadAsync(fileStream, uploadOptions);

            return new BASE_AZURE_BLOB
            {
                FILE_NAME = blobName,
                FILE_URL = GetPublicFileUrl(blobName),
                IS_SUCCESS = true
            };
        }

        public Task<BASE_AZURE_BLOB> UploadPrivateFileAsync(Stream fileStream, string fileName, string? folder = null)
        {
            throw new NotImplementedException();
        }

        public string GetPublicFileUrl(string fileName, bool useCdn = true)
        {
            var shouldUseCdn = useCdn && _options.UseCdn && !string.IsNullOrEmpty(_options.CdnUrl);

            if (shouldUseCdn)
                return $"{_options.CdnUrl}/{fileName}";

            return $"{_options.StorageUrl}/{_options.PublicContainer}/{fileName}";
        }

        public string GetPrivateFileUrl(string fileName, int expiryHours = 24)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePublicFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePrivateFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ListFilesAsync(string containerName, string? folder = null)
        {
            throw new NotImplementedException();
        }

        private static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png"            => "image/png",
                ".gif"            => "image/gif",
                ".svg"            => "image/svg+xml",
                ".webp"           => "image/webp",
                ".pdf"            => "application/pdf",
                ".doc"            => "application/msword",
                ".docx"           => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls"            => "application/vnd.ms-excel",
                ".xlsx"           => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _                 => "application/octet-stream"
            };
        }
    }
}
