using System;
using System.Collections.Generic;
using System.Reflection;
using Bogus;
using FakeEdms.Generators;
using FakeEdms.Helpers;

namespace FakeEdms
{
    public class DataGenerator
    {
        private const string DefaultLocale = "ru";

        private readonly string _locale = DefaultLocale;

        public int Seed { get; }

        #region Constructors

        public DataGenerator()
        {
            Seed = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds());
        }

        public DataGenerator(int seed)
        {
            Seed = seed;
        }

        public DataGenerator(string locale) : this()
        {
            _locale = locale;
        }

        public DataGenerator(int seed, string locale) : this(seed)
        {
            _locale = locale;
        }

        #endregion

        #region PublicApi

        public virtual IEnumerable<T> Generate<T>(int count)
            where T : class
        {
            return Generate(count, Activator.CreateInstance<T>);
        }

        public virtual IEnumerable<T> Generate<T>(int count, Func<T> factory)
            where T : class
        {
            Randomizer.Seed = new Random(Seed);
            
            var result = new List<T>();
            
            var f = new Faker<T>(_locale)
                .CustomInstantiator(_ => factory?.Invoke());

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
                f.RuleFor(property.Name, GetDataGenerationFactory<T>(property));

            for (var i = 0; i < count; i++)
                result.Add(f.Generate());

            return result;
        }

        #endregion

        private Func<Faker, T, object> GetDataGenerationFactory<T>(PropertyInfo property)
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

        private int GenerateInt(PropertyInfo property, Faker faker)
        {
            return faker.Random.Number(1, 100);
        }
        
        private DateTime GenerateDate(PropertyInfo property, Faker faker)
        {
            var now = DateTime.Now;
            return faker.Date.BetweenOffset(now.AddYears(-1), now.AddYears(1)).DateTime;
        }
        
        private string GenerateString(PropertyInfo property, Faker faker)
        {
            if (RegexUtils.IsRegistrationNumber(property.Name))
                return new RegistrationNumberGenerator(Seed).Generate();

            return faker.Lorem.Sentences(2, string.Empty);
        }
    }
}