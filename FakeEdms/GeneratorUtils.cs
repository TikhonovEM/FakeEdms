using System;
using System.Text;
using Bogus;
using FakeEdms.Helpers;

namespace FakeEdms
{
    public static class GeneratorUtils
    {
        public static string RegistrationNumber(int seed) => RegNumberGeneratorsPool.Instance.GetGenerator(seed).Generate();

        public static string RegistrationNumber(int seed, params string[] numberPatterns) => RegNumberGeneratorsPool.Instance.GetGenerator(seed, numberPatterns).Generate();

        public static string Subject(Faker faker) => Subject(faker, 30, 250);

        public static string Subject(Faker faker, int maxLength) => Subject(faker, 30, maxLength);

        public static string Subject(Faker faker, int minLength, int maxLength)
        {
            var text = new StringBuilder();
            var currentLength = 0;
            while (currentLength <= maxLength)
            {
                var sentence = faker.Lorem.Sentence(15, 5);
                text.Append(sentence);
                currentLength += sentence.Length;
            }
            return text.ToString().Substring(0, faker.Random.Int(minLength, maxLength));
        }

        public static int Number(Faker faker) => Number(faker, 0, 1_000_000);

        public static int Number(Faker faker, int maxValue) => Number(faker, 0, maxValue);

        public static int Number(Faker faker, int minValue, int maxValue) => faker.Random.Int(minValue, maxValue);

        public static DateTime Date(Faker faker, DateTime maxDate) => Date(faker, DateTime.Today, maxDate);

        public static DateTime Date(Faker faker, DateTime minDate, DateTime maxDate) => faker.Date.BetweenOffset(minDate, maxDate).DateTime;

        public static int ConsecutiveNumber(Faker faker) => faker.IndexFaker;

        public static double Double(Faker faker) => Double(faker, 0, 1_000_000);

        public static double Double(Faker faker, double maxValue) => Double(faker, 0, maxValue);

        public static double Double(Faker faker, double minValue, double maxValue) => faker.Random.Double(minValue, maxValue);

        public static float Float(Faker faker) => Float(faker, 0, 1_000_000);

        public static float Float(Faker faker, float maxValue) => Float(faker, 0, maxValue);
        
        public static float Float(Faker faker, float minValue, float maxValue) => faker.Random.Float(minValue, maxValue);
        public static string Email(Faker faker) => faker.Internet.Email();

        public static string Address(Faker faker) => faker.Address.FullAddress();
    }
}