using HPHA.UiPath.Core.ImageProcessing;
using Shouldly;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace HPHA.UiPath.Core.UnitTests.ImageProcessing
{
    public class ImageRegionSnipperTest
    {
        [Fact]
        public void SnipRegion_ShouldReturnCorrectImageAndFilePath()
        {
            // Arrange
            string inputFolderPath = Ulid.NewUlid().ToString();
            string inputFileName = Ulid.NewUlid().ToString();

            Directory.CreateDirectory(inputFolderPath);

            string inputFilePath = Path.Combine(inputFolderPath, $"{inputFileName}.png");

            using Image<Rgba32> image = new(100, 100);
            image.Mutate(ctx => ctx.Fill(Color.Red).Fill(Color.Green, new Rectangle(25, 25, 50, 50)));
            image.Save(inputFilePath);

            int x = 25, y = 25, width = 50, height = 50;

            // Act
            var (capturedImage, filePath) = ImageRegionSnipper
                .SnipRegion(inputFileName, inputFilePath, inputFolderPath, x, y, width, height);

            // Assert
            filePath.ShouldNotBeNullOrEmpty();
            File.Exists(filePath).ShouldBeTrue();
            capturedImage.Width.ShouldBe(width);
            capturedImage.Height.ShouldBe(height);

            if (Directory.Exists(inputFolderPath))
                Directory.Delete(inputFolderPath, true);
        }

        [Fact]
        public void SnipRegion_ShouldReturnCorrectImageAndFilePath_WhenWidthAndHeightAreLessThan50()
        {
            // Arrange
            string inputFolderPath = Ulid.NewUlid().ToString();
            string inputFileName = Ulid.NewUlid().ToString();

            Directory.CreateDirectory(inputFolderPath);

            string inputFilePath = Path.Combine(inputFolderPath, $"{inputFileName}.png");

            using Image<Rgba32> image = new(100, 100);
            image.Mutate(ctx => ctx.Fill(Color.Red).Fill(Color.Green, new Rectangle(25, 25, 50, 50)));
            image.Save(inputFilePath);

            int x = 25, y = 25, width = 10, height = 10;

            // Act
            var (capturedImage, filePath) = ImageRegionSnipper
                .SnipRegion(inputFileName, inputFilePath, inputFolderPath, x, y, width, height);

            // Assert
            filePath.ShouldNotBeNullOrEmpty();
            File.Exists(filePath).ShouldBeTrue();
            capturedImage.Width.ShouldBe(50);
            capturedImage.Height.ShouldBe(50);

            if (Directory.Exists(inputFolderPath))
                Directory.Delete(inputFolderPath, true);
        }
    }
}