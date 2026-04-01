using PP_ERP.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace PP_ERP.Infrastructure.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        private const int VERTICAL_WIDTH = 720;
        private const int VERTICAL_HEIGHT = 1280;
        private const int HORIZONTAL_WIDTH = 1280;
        private const int HORIZONTAL_HEIGHT = 720;
        private const int SQUARE_SIZE = 1000;

        private static readonly Dictionary<string, int> QualityMap = new()
        {
            { "signature", 90 },
            { "profile",   85 },
            { "general",   80 }
        };

        public async Task<byte[]> ResizeImageAsync(Stream inputStream, string type = "general")
        {
            using var image = await Image.LoadAsync(inputStream);

            var (targetWidth, targetHeight) = CalculateTargetSize(image.Width, image.Height);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(targetWidth, targetHeight),
                Mode = ResizeMode.Max,
                Sampler = KnownResamplers.Lanczos3
            }));

            await using var outputStream = new MemoryStream();
            await image.SaveAsJpegAsync(outputStream, new JpegEncoder
            {
                Quality = GetQuality(type)
            });

            return outputStream.ToArray();
        }

        private static (int Width, int Height) CalculateTargetSize(int width, int height)
        {
            if (height > width) return (VERTICAL_WIDTH, VERTICAL_HEIGHT);
            if (height < width) return (HORIZONTAL_WIDTH, HORIZONTAL_HEIGHT);
            return (SQUARE_SIZE, SQUARE_SIZE);
        }

        private static int GetQuality(string type)
        {
            return QualityMap.TryGetValue(type.ToLower(), out var quality) ? quality : QualityMap["general"];
        }
    }
}
