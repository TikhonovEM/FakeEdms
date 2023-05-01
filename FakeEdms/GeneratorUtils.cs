using System;
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

        public static int Number(Faker faker, int maxValue)
        {
            return Number(faker, 0, maxValue);
        }
        
        public static int Number(Faker faker, int minValue, int maxValue)
        {
            return faker.Random.Int(minValue, maxValue);
        }

        public static DateTime Date(Faker faker, DateTime maxDate)
        {
            return Date(faker, DateTime.Today, maxDate);
        }

        public static DateTime Date(Faker faker, DateTime minDate, DateTime maxDate)
        {
            return faker.Date.BetweenOffset(minDate, maxDate).DateTime;
        }

        public static int ConsecutiveNumber(Faker faker)
        {
            return faker.IndexFaker;
        }
    }
}