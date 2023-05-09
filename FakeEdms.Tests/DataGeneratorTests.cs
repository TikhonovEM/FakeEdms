using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using System = Bogus.DataSets.System;

namespace FakeEdms.Tests
{
    public class DataGeneratorTests
    {
        [Fact]
        public void ConstructorWithSeed_ReturnsSameSeed()
        {
            const int seed = 420;

            var dataGenerator = new DataGenerator<TestDataClass>(seed);
            
            Assert.Equal(seed, dataGenerator.Seed);
        }
        
        [Fact]
        public void ConstructorWithoutLocale_UseRuLocale()
        {
            const string cyrillicSymbolsPattern = @"[А-Яа-я,\.\s]+";
            
            var dataGenerator = new DataGenerator<TestDataClass>();
            var data = dataGenerator.Generate(1).First();
            var text = data.Text;
            
            Assert.Matches(cyrillicSymbolsPattern, text);
        }
        
        [Fact]
        public void ConstructorWithEnLocale_UseEnLocale()
        {
            const string latinSymbolsPattern = @"[A-Za-z,\.\s]+";
            const string enLocale = "en_US";
            
            var dataGenerator = new DataGenerator<TestDataClass>(enLocale);
            var data = dataGenerator.Generate(1).First();
            var text = data.Text;
            
            Assert.Matches(latinSymbolsPattern, text);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void Generate_WithCount_ReturnsSameCountOfObjects(int count)
        {

            var dataGenerator = new DataGenerator<TestDataClass>();
            var data = dataGenerator.Generate(count);
            var dataCount = data.Count();

            Assert.Equal(count, dataCount);
        }
        
        [Fact]
        public void Generate_WithTType_ReturnsObjectsWithTType()
        {
            var dataGenerator = new DataGenerator<TestDataClass>();
            var data = dataGenerator.Generate(100);

            Assert.All(data, obj => Assert.IsType<TestDataClass>(obj));
        }

        [Theory]
        [InlineData(47)]
        [InlineData(555)]
        [InlineData(5432)]
        public void Generate_2GeneratorsWithSameSeedWithoutCustomRules_ReturnsSameData_ExceptRegexData(int seed)
        {
            var dataGenerator1 = new DataGenerator<TestDataClassWithoutRegNumber>(seed);
            var dataGenerator2 = new DataGenerator<TestDataClassWithoutRegNumber>(seed);
            var count = 100;

            var data1 = dataGenerator1.Generate(count).ToArray();
            var data2 = dataGenerator2.Generate(count).ToArray();

            for (var i = 0; i < count; i++)
            {
                Assert.Equal(data1[i], data2[i], new TestDataClassWithoutRegNumberEqualityComparer());
            }
        }
        
        [Theory]
        [InlineData(47)]
        [InlineData(555)]
        [InlineData(5432)]
        public void Generate_2GeneratorsWithSameSeedWithCustomRules_ReturnsSameData_ExceptRegexData(int seed)
        {
            var dataGenerator1 = new DataGenerator<TestDataClass>(seed)
                .AsSubject(x => x.Text)
                .AsAddress(x => x.DocumentAnnotation)
                .AsEmail(x => x.RegistrationNumberAdd)
                .AsConsecutiveNumber(x => x.Age);
            var dataGenerator2 = new DataGenerator<TestDataClass>(seed)
                .AsSubject(x => x.Text)
                .AsAddress(x => x.DocumentAnnotation)
                .AsEmail(x => x.RegistrationNumberAdd)
                .AsConsecutiveNumber(x => x.Age);
            var count = 100;

            var data1 = dataGenerator1.Generate(count).ToArray();
            var data2 = dataGenerator2.Generate(count).ToArray();

            for (var i = 0; i < count; i++)
            {
                Assert.Equal(data1[i], data2[i], new TestDataClassEqualityComparer());
            }
        }

        [Fact]
        public void RuleFor_StringFakerTTproperty_AddedRuleForStringPropertyWithoutError()
        {
            const string propertyName = "Text";

            var dataGenerator = new DataGenerator<TestDataClass>();
            var exception = Record.Exception(() => dataGenerator.RuleFor(propertyName, (faker, obj) => faker.Lorem.Text()));
            
            Assert.Null(exception);
        }
        
        [Fact]
        public void RuleFor_StringFakerTproperty_AddedRuleForStringPropertyWithoutError()
        {
            const string propertyName = "Text";

            var dataGenerator = new DataGenerator<TestDataClass>();
            var exception = Record.Exception(() => dataGenerator.RuleFor(propertyName, faker => faker.Lorem.Text()));
            
            Assert.Null(exception);
        }
        
        [Fact]
        public void RuleFor_TTporpertyFakerTTproperty_AddedRuleForStringPropertyWithoutError()
        {
            var dataGenerator = new DataGenerator<TestDataClass>();
            var exception = Record.Exception(() => dataGenerator.RuleFor(obj => obj.Text, (faker, obj) => faker.Lorem.Text()));
            
            Assert.Null(exception);
        }
        
        [Fact]
        public void RuleFor_TTporpertyFakerTproperty_AddedRuleForStringPropertyWithoutError()
        {
            var dataGenerator = new DataGenerator<TestDataClass>();
            var exception = Record.Exception(() => dataGenerator.RuleFor(obj => obj.Text, faker => faker.Lorem.Text()));
            
            Assert.Null(exception);
        }
    }
}