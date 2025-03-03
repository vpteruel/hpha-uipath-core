using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace HPHA.UiPath.Core.ImageProcessing
{
    public static class ImageRegionSnipper
    {
        /// <summary>
        /// Snips a region from an image and saves it to a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="inputFilePath"></param>
        /// <param name="inputFolderPath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static (Image<Rgba32> capturedImage, string filePath) SnipRegion(string fileName, string inputFilePath, string inputFolderPath, int x, int y, int width, int height)
        {
            using Image<Rgba32> originalImage = Image.Load<Rgba32>(inputFilePath);
            Image<Rgba32> snippedImage = originalImage.Clone(ctx => ctx.Crop(new Rectangle(x, y, width, height)));

            var tempFilePath = $"{inputFolderPath}/{fileName}_snipped.png";
            snippedImage.Save(tempFilePath, new PngEncoder());

            return (snippedImage, tempFilePath);
        }
    }
}
