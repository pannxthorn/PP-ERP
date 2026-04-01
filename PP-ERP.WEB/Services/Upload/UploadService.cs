using Microsoft.AspNetCore.Components.Forms;
using PP_ERP.DTO.BaseDTO;
using PP_ERP.WEB.Services.Base;
using System.Net.Http.Headers;

namespace PP_ERP.WEB.Services.Upload
{
    public class UploadService(RestCommon rest, IConfiguration configuration)
        : BaseService(rest, configuration)
    {
        private const long MaxFileSizeBytes = 2 * 1024 * 1024;

        public async Task<RESULT_REST_RESPONSE<BASE_AZURE_BLOB>> UploadImage(IBrowserFile file, string folder = "images")
        {
            using var stream = file.OpenReadStream(maxAllowedSize: MaxFileSizeBytes);
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            var formData = new MultipartFormDataContent();
            formData.Add(fileContent, "file", file.Name);

            return await PostMultipart<BASE_AZURE_BLOB>("api/upload/image", formData);
        }
    }
}
