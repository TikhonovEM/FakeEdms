﻿using System;
using System.Collections.Generic;
using Bogus;
using FakeEdms.Helpers;

namespace FakeEdms
{
    public class DataGenerator
    {
        private const string DefaultLocale = "ru";
        
        public static IEnumerable<T> Generate<T>(int count)
            where T : class
        {
            return Generate(count, Activator.CreateInstance<T>);
        }

        public static IEnumerable<T> Generate<T>(int count, Func<T> factory)
            where T : class
        {
            var result = new List<T>();
            
            var f = new Faker<T>(DefaultLocale)
                .CustomInstantiator(_ => factory?.Invoke());
            
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
                f.RuleFor(property.Name, DataGenerationRule.GetDataGenerationFactory<T>(property));

            for (var i = 0; i < count; i++)
                result.Add(f.Generate());

            return result;
        }
    }
}