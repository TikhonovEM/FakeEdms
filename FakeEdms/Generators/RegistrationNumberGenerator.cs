using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Fare;

namespace FakeEdms.Generators
{
    internal class RegistrationNumberGenerator
    {
        private static readonly string[] DefaultExpressions = 
        {
            @"^\d{4}$",
            @"^[А-Яа-я]{2,6}-\d{4}-[А-Яа-я]{2,6}$"
        };

        private readonly IEnumerable<string> _numberRegularExpressions = DefaultExpressions;
        
        private readonly ConcurrentDictionary<string, Xeger> _generators = new ConcurrentDictionary<string, Xeger>();
        private readonly Random _random;
        
        public RegistrationNumberGenerator()
        {
            _random = new Random();
        }
        
        public RegistrationNumberGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public RegistrationNumberGenerator(IEnumerable<string> numberRegularExpressions) : this()
        {
            _numberRegularExpressions = numberRegularExpressions;
        }
        
        public RegistrationNumberGenerator(IEnumerable<string> numberRegularExpressions, int seed) : this(seed)
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