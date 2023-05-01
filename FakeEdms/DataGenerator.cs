using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus;
using FakeEdms.Generators;
using FakeEdms.Helpers;

namespace FakeEdms
{
    public class DataGenerator<T> where T : class
    {
        private const string DefaultLocale = "ru";

        private readonly Faker<T> _faker;
        private readonly List<string> _propertiesWithCustomRule = new List<string>();

        public int Seed { get; }

        #region Constructors

        public DataGenerator()
        {
            Seed = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds());
            _faker = new Faker<T>(DefaultLocale);
        }

        public DataGenerator(int seed)
        {
            Seed = seed;
            _faker = new Faker<T>(DefaultLocale);
        }

        public DataGenerator(string locale)
        {
            _faker = new Faker<T>(locale);
        }

        public DataGenerator(int seed, string locale)
        {
            Seed = seed;
            _faker = new Faker<T>(locale);
        }

        #endregion

        #region PublicApi

        public IEnumerable<T> Generate(int count)
        {
            return Generate(count, Activator.CreateInstance<T>);
        }

        public IEnumerable<T> Generate(int count, Func<T> factory)
        {
            Randomizer.Seed = new Random(Seed);

            var result = new List<T>();
            _faker.CustomInstantiator(_ => factory?.Invoke());

            var properties = typeof(T).GetProperties();
            foreach (var property in properties.Where(p => !_propertiesWithCustomRule.Contains(p.Name)))
                _faker.RuleFor(property.Name, DefaultGenerationRules.GetDataGenerationFactory<T>(property));

            for (var i = 0; i < count; i++)
                result.Add(_faker.Generate());

            return result;
        }

        public DataGenerator<T> RuleFor(string propertyName, Func<Faker, T, object> setter)
        {
            _faker.RuleFor(propertyName, setter);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }

        #endregion
    }
}