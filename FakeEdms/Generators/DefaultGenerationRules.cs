using System;
using System.Reflection;
using Bogus;
using FakeEdms.Helpers;

namespace FakeEdms.Generators
{
    internal static class DefaultGenerationRules
    {
        #region Default types
        private static readonly Type IntType = typeof(int);
        private static readonly Type NullIntType = typeof(int?);
        private static readonly Type DateType = typeof(DateTime);
        private static readonly Type NullDateType = typeof(DateTime?);
        private static readonly Type DoubleType = typeof(double);
        private static readonly Type NullDoubleType = typeof(double?);
        private static readonly Type FloatType = typeof(float);
        private static readonly Type NullFloatType = typeof(float?);
        private static readonly Type StringType = typeof(string);
        #endregion



        public static Func<Faker, T, object> GetDataGenerationFactory<T>(PropertyInfo property)
        {
            switch (property.PropertyType)
            {
                case Type intType when intType == IntType || intType == NullIntType:
                    return (f, t) => GenerateInt(property, f);
                case Type dateType when dateType == DateType || dateType == NullDateType:
                    return (f, t) => GenerateDate(property, f);
                case Type stringType when stringType == StringType:
                    return (f, t) => GenerateString(property, f);
                case Type doubleType when doubleType == DoubleType || doubleType == NullDoubleType:
                    return (f, t) => GenerateDouble(property, f);
                case Type floatType when floatType == FloatType || floatType == NullFloatType:
                    return (f, t) => GenerateFloat(property, f);
                default: return (f, t) => null;
            }
        }

        private static int GenerateInt(PropertyInfo property, Faker faker) => GeneratorUtils.Number(faker);

        private static double GenerateDouble(PropertyInfo property, Faker faker) => GeneratorUtils.Double(faker);

        private static float GenerateFloat(PropertyInfo property, Faker faker) => GeneratorUtils.Float(faker);

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
                return GeneratorUtils.Email(faker);
            
            if (RegexUtils.IsAddress(propertyName))
                return GeneratorUtils.Address(faker);

            return faker.Lorem.Sentences(2, string.Empty);
        }
    }
}