using System.Collections.Concurrent;
using System.Collections.Generic;
using FakeEdms.Generators;

namespace FakeEdms.Helpers
{
    public class RegNumberGeneratorsPool
    {
        private class RegNumberGeneratorsPoolUniqueValue
        {
            private  int Seed { get; }
            public IEnumerable<string> Patterns { get; }

            public RegNumberGeneratorsPoolUniqueValue(int seed, IEnumerable<string> patterns)
            {
                Seed = seed;
                Patterns = patterns;
            }

            public override bool Equals(object obj)
            {
                if (base.Equals(obj))
                    return true;

                var y = (RegNumberGeneratorsPoolUniqueValue)obj;

                if (y == null)
                    return false;

                return Seed == y.Seed && Patterns.Equals(y.Patterns);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Seed * 397) ^ (Patterns != null ? Patterns.GetHashCode() : 0);
                }
            }
        }
        
        
        
        private readonly ConcurrentDictionary<RegNumberGeneratorsPoolUniqueValue, RegistrationNumberGenerator> _generators;
        private readonly ConcurrentDictionary<int, RegistrationNumberGenerator> _defaultGenerators;

        private RegNumberGeneratorsPool()
        {
            _generators = new ConcurrentDictionary<RegNumberGeneratorsPoolUniqueValue, RegistrationNumberGenerator>();
            _defaultGenerators = new ConcurrentDictionary<int, RegistrationNumberGenerator>();
        }

        public static RegNumberGeneratorsPool Instance { get; } = new RegNumberGeneratorsPool();

        public RegistrationNumberGenerator GetGenerator(int seed, IEnumerable<string> patterns)
        {
            var regNumberGeneratorsPoolUniqueValue = new RegNumberGeneratorsPoolUniqueValue(seed, patterns);
            return _generators.GetOrAdd(regNumberGeneratorsPoolUniqueValue, p => new RegistrationNumberGenerator(seed, p.Patterns));
        }
        
        public RegistrationNumberGenerator GetGenerator(int seed)
        {
            return _defaultGenerators.GetOrAdd(seed, s => new RegistrationNumberGenerator(s));
        }
    }
}