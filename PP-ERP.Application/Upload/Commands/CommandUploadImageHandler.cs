using MediatR;
using PP_ERP.Application.Interfaces;
using PP_ERP.DTO.BaseDTO;

namespace PP_ERP.Application.Upload.Commands
{
    public class CommandUploadImageHandler(IImageProcessingService imageProcessing, IBlobStorageService blob)
        : IRequestHandler<CommandUploadImage, BASE_AZURE_BLOB>
    {
        public async Task<BASE_AZURE_BLOB> Handle(CommandUploadImage request, CancellationToken cancellationToken)
        {
            var resized = await imageProcessing.ResizeImageAsync(request.FileStream, "general");
            using var ms = new MemoryStream(resized);
            return await blob.UploadPublicFileAsync(ms, request.FileName, request.Folder);
        }
    }
}
