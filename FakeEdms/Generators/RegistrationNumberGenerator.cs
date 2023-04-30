using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Fare;

namespace FakeEdms.Generators
{
    internal class RegistrationNumberGenerator
    {
        private readonly IEnumerable<string> _defaultExpressions = new []
        {
            @"^\d{4}$",
            @"^[А-Яа-я]{2,6}-\d{4}-[А-Яа-я]{2,6}$"
        };
        
        private readonly IEnumerable<string> _numberRegularExpressions;
        
        private readonly ConcurrentDictionary<string, Xeger> _generators = new ConcurrentDictionary<string, Xeger>();
        private readonly Random _random = new Random();
        
        public RegistrationNumberGenerator()
        {
            _numberRegularExpressions = _defaultExpressions;
        }

        public RegistrationNumberGenerator(IEnumerable<string> numberRegularExpressions)
        {
            _numberRegularExpressions = numberRegularExpressions;
        }

        public string Generate()
        {
            var regex = _numberRegularExpressions.ElementAt(_random.Next(0, _numberRegularExpressions.Count()));
            var generator = _generators.GetOrAdd(regex, s => new Xeger(s));

            return generator.Generate();
        }
    }
}