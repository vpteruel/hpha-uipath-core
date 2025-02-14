using HPHA.UiPath.Core.ImageProcessing;
using Shouldly;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace HPHA.UiPath.Core.UnitTests.ImageProcessing
{
    public class ImageRegionSnipperTest : IDisposable
    {
        private readonly string _inputFileName;
        private readonly string _inputFilePath;
        private readonly string _inputFolderPath;

        public ImageRegionSnipperTest()
        {
            _inputFileName = Guid.NewGuid().ToString();
            _inputFolderPath = Guid.NewGuid().ToString();
            
            Directory.CreateDirectory(_inputFolderPath);

            _inputFilePath = Path.Combine(_inputFolderPath, $"{_inputFileName}.png");

            // Create a test image
            using Image<Rgba32> image = new(100, 100);
            image.Mutate(ctx => ctx.Fill(Color.Red).Fill(Color.Green, new Rectangle(25, 25, 50, 50)));
            image.Save(_inputFilePath);
        }

        [Fact]
        public void SnipRegion_ShouldReturnCorrectImageAndFilePath()
        {
            // Arrange
            int x = 25, y = 25, width = 50, height = 50;

            // Act
            var (capturedImage, filePath) = ImageRegionSnipper
                .SnipRegion(_inputFileName, _inputFilePath, _inputFolderPath, x, y, width, height);

            // Assert
            filePath.ShouldNotBeNullOrEmpty();
            File.Exists(filePath).ShouldBeTrue();

            using Image<Rgba32> expectedImage = new(50, 50);
            expectedImage.Mutate(ctx => ctx.Fill(Color.Green));

            capturedImage.Frames.Count.ShouldBe(expectedImage.Frames.Count);
            capturedImage.Size.ShouldBe(expectedImage.Size);
            capturedImage.Width.ShouldBe(expectedImage.Width);
            capturedImage.Height.ShouldBe(expectedImage.Height);
        }

        public void Dispose()
        {
            if (Directory.Exists(_inputFolderPath))
            {
                Directory.Delete(_inputFolderPath, true);
            }
        }
    }
}