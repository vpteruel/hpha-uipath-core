using HPHA.UiPath.Core.Generators;
using Shouldly;

namespace HPHA.UiPath.Core.UnitTests.Generators
{
    public class DeterministicIdGeneratorTests
    {
        [Fact]
        public void GenerateShortDeterministicID_ReturnsExpectedLength_DefaultSize()
        {
            // Arrange
            string input = "test input";

            // Act
            string result = DeterministicIdGenerator.GenerateShortDeterministicID(input);

            // Assert
            result.Length.ShouldBe(16);
        }

        [Fact]
        public void GenerateShortDeterministicID_ReturnsExpectedLength_CustomSize()
        {
            // Arrange
            string input = "test input";
            int customSize = 8;

            // Act
            string result = DeterministicIdGenerator.GenerateShortDeterministicID(input, customSize);

            // Assert
            result.Length.ShouldBe(customSize);
        }

        [Theory]
        [InlineData("test input")]
        [InlineData("another test input")]
        [InlineData("yet another test input")]
        [InlineData("one more test input")]
        [InlineData("final test input")]
        public void GenerateShortDeterministicID_ReturnsUrlSafeString(string input)
        {
            // Arrange

            // Act
            string result = DeterministicIdGenerator.GenerateShortDeterministicID(input);

            // Assert
            result.ShouldNotContain("/");
            result.ShouldNotContain("+");
        }

        [Fact]
        public void GenerateShortDeterministicID_ReturnsDeterministicResult()
        {
            // Arrange
            string input = "test input";

            // Act
            string result1 = DeterministicIdGenerator.GenerateShortDeterministicID(input);
            string result2 = DeterministicIdGenerator.GenerateShortDeterministicID(input);

            // Assert
            result1.ShouldBe(result2);
        }

        [Fact]
        public void GenerateShortDeterministicID_ThrowsException_ForInvalidSize()
        {
            // Arrange
            string input = "test input";
            int invalidSize = 100; // Size larger than the base64 hash length

            // Act
            Action act = () => DeterministicIdGenerator.GenerateShortDeterministicID(input, invalidSize);

            // Assert
            act.ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
