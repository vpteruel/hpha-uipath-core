using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace HPHA.UiPath.Core.ImageProcessing
{
    public static class ImageRegionSnipper
    {
        private const int MIN_WIDTH = 50;
        private const int MIN_HEIGHT = 50;

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
            Image<Rgba32> snippedImage;

            if (width < MIN_WIDTH || height < MIN_HEIGHT)
            {
                // Calculate the new width and height for the snipped image
                int newWidth = Math.Max(width, MIN_WIDTH);
                int newHeight = Math.Max(height, MIN_HEIGHT);
                
                // Create a new MIN_WIDTHxMIN_HEIGHT image with a transparent background
                snippedImage = new Image<Rgba32>(newWidth, newHeight);
                snippedImage.Mutate(ctx => ctx.Fill(Color.Transparent));

                // Draw the cropped region of the original image onto the new image
                var croppedRegion = originalImage.Clone(ctx => ctx.Crop(new Rectangle(x, y, width, height)));
                snippedImage.Mutate(ctx => ctx.DrawImage(croppedRegion, new Point(0, 0), 1f));
            }
            else
            {
                // Crop the region directly if width and height are >= MIN_WIDTH and MIN_HEIGHT
                snippedImage = originalImage.Clone(ctx => ctx.Crop(new Rectangle(x, y, width, height)));
            }

            var tempFilePath = $"{inputFolderPath}/{fileName}_snipped.png";
            snippedImage.Save(tempFilePath, new PngEncoder());

            return (snippedImage, tempFilePath);
        }
    }
}
