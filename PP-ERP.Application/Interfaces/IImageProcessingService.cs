namespace PP_ERP.Application.Interfaces
{
    public interface IImageProcessingService
    {
        Task<byte[]> ResizeImageAsync(Stream inputStream, string type = "general");
    }
}
