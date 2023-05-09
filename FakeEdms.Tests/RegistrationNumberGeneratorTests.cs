using FakeEdms.Generators;
using Xunit;

namespace FakeEdms.Tests
{
    public class RegistrationNumberGeneratorTests
    {
        [Theory]
        [InlineData(444)]
        [InlineData(2023)]
        [InlineData(987654321)]
        public void Generate_2GeneratorsWithSameSeedWithoutPatterns_ReturnsSameString(int seed)
        {
            var generator1 = new RegistrationNumberGenerator(seed);
            var generator2 = new RegistrationNumberGenerator(seed);

            var string1 = generator1.Generate();
            var string2 = generator2.Generate();
            
            Assert.Equal(string1, string2);
        }
        
        [Theory]
        [InlineData(444)]
        [InlineData(2023)]
        [InlineData(987654321)]
        public void Generate_2GeneratorsWithSameSeed_ReturnsSameString(int seed)
        {
            var patterns = new []
            {
                @"^\d{6}$",
                @"^\w{4}-\d{6}$",
                @"^\d{6}-\w{4}$",
            };
            
            var generator1 = new RegistrationNumberGenerator(seed, patterns);
            var generator2 = new RegistrationNumberGenerator(seed, patterns);

            var string1 = generator1.Generate();
            var string2 = generator2.Generate();
            
            Assert.Equal(string1, string2);
        }

        [Theory]
        [InlineData(@"^\d{6}$")]
        [InlineData(@"^\w{4}-\d{6}$")]
        [InlineData(@"^\d{6}-\w{4}$")]
        public void Generate_GeneratedStringMatchesRegex(string regex)
        {
            var generator = new RegistrationNumberGenerator(420, new []{ regex });

            var generatedString = generator.Generate();
            
            Assert.Matches(regex, generatedString);
        }
    }
}