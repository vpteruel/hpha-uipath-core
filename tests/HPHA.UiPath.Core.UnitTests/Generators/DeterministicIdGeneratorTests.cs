using HPHA.UiPath.Core.Generators;

namespace HPHA.UiPath.Core.UnitTests.Generators
{
    public class DeterministicIdGeneratorTests
    {
        [Fact]
        public void GenerateShortDeterministicID_ReturnsExpectedLength_DefaultSize()
        {
            string input = "test input";
            string result = DeterministicIdGenerator.GenerateShortDeterministicID(input);
            Assert.Equal(16, result.Length);
        }

        [Fact]
        public void GenerateShortDeterministicID_ReturnsExpectedLength_CustomSize()
        {
            string input = "test input";
            int customSize = 8;
            string result = DeterministicIdGenerator.GenerateShortDeterministicID(input, customSize);
            Assert.Equal(customSize, result.Length);
        }

        [Fact]
        public void GenerateShortDeterministicID_ReturnsUrlSafeString()
        {
            string input = "test input";
            string result = DeterministicIdGenerator.GenerateShortDeterministicID(input);
            Assert.DoesNotContain("/", result);
            Assert.DoesNotContain("+", result);
        }

        [Fact]
        public void GenerateShortDeterministicID_ReturnsDeterministicResult()
        {
            string input = "test input";
            string result1 = DeterministicIdGenerator.GenerateShortDeterministicID(input);
            string result2 = DeterministicIdGenerator.GenerateShortDeterministicID(input);
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void GenerateShortDeterministicID_ThrowsException_ForInvalidSize()
        {
            string input = "test input";
            int invalidSize = 100; // Size larger than the base64 hash length
            Assert.Throws<ArgumentOutOfRangeException>(() => DeterministicIdGenerator.GenerateShortDeterministicID(input, invalidSize));
        }
    }
}
