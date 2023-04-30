using System;
using System.Reflection;
using Bogus;

namespace FakeEdms
{
    internal static class DataGenerationRule
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

        internal static int GenerateInt(PropertyInfo property, Faker faker)
        {
            return faker.Random.Number(1, 100);
        }
        
        internal static DateTime GenerateDate(PropertyInfo property, Faker faker)
        {
            var now = DateTime.Now;
            return faker.Date.BetweenOffset(now.AddYears(-1), now.AddYears(1)).DateTime;
        }
        
        internal static string GenerateString(PropertyInfo property, Faker faker)
        {
            if (property.Name.Contains("RegistrationNumber"))
                return $"RegistrationNumber {faker.UniqueIndex}";

            return faker.Lorem.Sentences(2, string.Empty);
        }
    }
}