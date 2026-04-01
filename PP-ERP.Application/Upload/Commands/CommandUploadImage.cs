using MediatR;
using PP_ERP.DTO.BaseDTO;

namespace PP_ERP.Application.Upload.Commands
{
    public class CommandUploadImage : IRequest<BASE_AZURE_BLOB>
    {
        public required Stream FileStream { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public string Folder { get; set; } = "images";
    }
}
