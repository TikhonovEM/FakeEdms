using System.Collections.Generic;
using System.Text;
using Bogus;
using FakeEdms.Generators;
using FakeEdms.Helpers;

namespace FakeEdms
{
    public static class GeneratorUtils
    {
        public static string RegistrationNumber()
        {
            return RegNumberGeneratorsPool.Instance.GetGenerator().Generate();
        }
        
        public static string RegistrationNumber(params string[] numberPatterns)
        {
            return RegNumberGeneratorsPool.Instance.GetGenerator(numberPatterns).Generate();
        }

        public static string Subject(Faker faker)
        {
            return Subject(faker, 30, 250);
        }

        public static string Subject(Faker faker, int maxLength)
        {
            return Subject(faker, 30, maxLength);
        }
        
        public static string Subject(Faker faker, int minLenght, int maxLength)
        {
            var text = new StringBuilder();
            var currentLength = 0;
            while (currentLength <= maxLength)
            {
                var sentence = faker.Lorem.Sentence(15, 5);
                text.Append(sentence);
                currentLength += sentence.Length;
            }
            return text.ToString().Substring(0, faker.Random.Int(minLenght, maxLength));
        }
    }
}