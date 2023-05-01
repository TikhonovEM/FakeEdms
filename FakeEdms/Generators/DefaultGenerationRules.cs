using System;
using System.Reflection;
using Bogus;
using FakeEdms.Helpers;

namespace FakeEdms.Generators
{
    internal static class DefaultGenerationRules
    {
        public static Func<Faker, T, object> GetDataGenerationFactory<T>(PropertyInfo property)
        {
            switch (property.PropertyType)
            {
                case Type intType when intType == typeof(int) || intType == typeof(int?):
                    return (f, t) => GenerateInt(property, f);
                case Type dateType when dateType == typeof(DateTime) || dateType == typeof(DateTime?):
                    return (f, t) => GenerateDate(property, f);
                case Type stringType when stringType == typeof(string):
                    return (f, t) => GenerateString(property, f);
                default: return (f, t) => null;
            }
        }

        private static int GenerateInt(PropertyInfo property, Faker faker)
        {
            return faker.Random.Number(1, 1000000);
        }

        private static DateTime GenerateDate(PropertyInfo property, Faker faker)
        {
            var now = DateTime.Now;
            return faker.Date.BetweenOffset(now.AddYears(-1), now.AddYears(1)).DateTime;
        }

        private static string GenerateString(PropertyInfo property, Faker faker)
        {
            var propertyName = property.Name;
            
            if (RegexUtils.IsRegistrationNumber(propertyName))
                return GeneratorUtils.RegistrationNumber();

            if (RegexUtils.IsSubject(propertyName))
                return GeneratorUtils.Subject(faker);

            if (RegexUtils.IsEmail(propertyName))
                return faker.Internet.Email();
            
            if (RegexUtils.IsAddress(propertyName))
                return faker.Address.FullAddress();

            return faker.Lorem.Sentences(2, string.Empty);
        }
    }
}