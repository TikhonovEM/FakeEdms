﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            _faker = new Faker<T>(DefaultLocale).UseSeed(Seed);
        }

        public DataGenerator(int seed)
        {
            Seed = seed;
            _faker = new Faker<T>(DefaultLocale).UseSeed(Seed);
        }

        public DataGenerator(string locale)
        {
            Seed = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds());
            _faker = new Faker<T>(locale).UseSeed(Seed);
        }

        public DataGenerator(int seed, string locale)
        {
            Seed = seed;
            _faker = new Faker<T>(locale).UseSeed(Seed);
        }

        #endregion

        #region PublicApi

        public IEnumerable<T> Generate(int count)
        {
            return Generate(count, Activator.CreateInstance<T>);
        }

        public IEnumerable<T> Generate(int count, Func<T> factory)
        {
            var result = new List<T>();
            _faker.CustomInstantiator(_ => factory?.Invoke());

            var properties = typeof(T).GetProperties();
            foreach (var property in properties.Where(p => !_propertiesWithCustomRule.Contains(p.Name)))
                _faker.RuleFor(property.Name, DefaultGenerationRules.GetDataGenerationFactory<T>(property, Seed));

            for (var i = 0; i < count; i++)
                result.Add(_faker.Generate());

            return result;
        }

        public DataGenerator<T> RuleFor<TProperty>(string propertyName, Func<Faker, T, TProperty> setter)
        {
            _faker.RuleFor(propertyName, setter);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> RuleFor<TProperty>(string propertyName, Func<Faker, TProperty> setter)
        {
            _faker.RuleFor(propertyName, setter);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<Faker, T, TProperty> setter)
        {
            _faker.RuleFor(property, setter);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<Faker, TProperty> setter)
        {
            _faker.RuleFor(property, setter);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }

        public DataGenerator<T> AsRegistrationNumber(Expression<Func<T, string>> property)
        {
            _faker.RuleFor(property, _ => GeneratorUtils.RegistrationNumber(Seed));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsRegistrationNumber(Expression<Func<T, string>> property, params string[] regNumberPatterns)
        {
            _faker.RuleFor(property, _ => GeneratorUtils.RegistrationNumber(Seed, regNumberPatterns));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsSubject(Expression<Func<T, string>> property)
        {
            _faker.RuleFor(property, GeneratorUtils.Subject);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsSubject(Expression<Func<T, string>> property, int maxLength)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.Subject(faker, maxLength));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsSubject(Expression<Func<T, string>> property, int minLength, int maxLength)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.Subject(faker, minLength, maxLength));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsDateBetween(Expression<Func<T, DateTime>> property, DateTime maxDate)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.Date(faker, maxDate));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsDateBetween(Expression<Func<T, DateTime>> property, DateTime minDate, DateTime maxDate)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.Date(faker, minDate, maxDate));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsDateBetween(Expression<Func<T, DateTime?>> property, DateTime maxDate)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.Date(faker, maxDate));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsDateBetween(Expression<Func<T, DateTime?>> property, DateTime minDate, DateTime maxDate)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.Date(faker, minDate, maxDate));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsConsecutiveNumber(Expression<Func<T, int>> property)
        {
            _faker.RuleFor(property, GeneratorUtils.ConsecutiveNumber);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsConsecutiveNumber(Expression<Func<T, int?>> property)
        {
            _faker.RuleFor(property, faker => GeneratorUtils.ConsecutiveNumber(faker));
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsEmail(Expression<Func<T, string>> property)
        {
            _faker.RuleFor(property, GeneratorUtils.Email);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }
        
        public DataGenerator<T> AsAddress(Expression<Func<T, string>> property)
        {
            _faker.RuleFor(property, GeneratorUtils.Address);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }

        public DataGenerator<T> AsConstant<TProperty>(Expression<Func<T, TProperty>> property, TProperty value)
        {
            _faker.RuleFor(property, _ => value);
            var propertyName = PropertyName.For(property);
            if (!_propertiesWithCustomRule.Contains(propertyName))
                _propertiesWithCustomRule.Add(propertyName);
            return this;
        }

        #endregion
    }
}