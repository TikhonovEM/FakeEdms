using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Fare;

namespace FakeEdms.Generators
{
    public class RegistrationNumberGenerator
    {
        private static readonly string[] DefaultExpressions = 
        {
            @"^\d{4}$",
            @"^[А-Яа-я]{2,6}-\d{4}-[А-Яа-я]{2,6}$",
            @"^\d{4}-[А-Яа-я]{2,6}$",
            @"^[А-Яа-я]{2,6}-\d{4}$"
        };

        private readonly Random _random;

        private readonly IEnumerable<string> _numberRegularExpressions = DefaultExpressions;
        
        private readonly ConcurrentDictionary<string, Xeger> _generators = new ConcurrentDictionary<string, Xeger>();

        public RegistrationNumberGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public RegistrationNumberGenerator(int seed, IEnumerable<string> numberRegularExpressions) : this(seed)
        {
            _numberRegularExpressions = numberRegularExpressions;
        }

        public string Generate()
        {
            var regex = _numberRegularExpressions.ElementAt(_random.Next(0, _numberRegularExpressions.Count()));
            var generator = _generators.GetOrAdd(regex, s => new Xeger(s, _random));

            return generator.Generate();
        }
    }
}