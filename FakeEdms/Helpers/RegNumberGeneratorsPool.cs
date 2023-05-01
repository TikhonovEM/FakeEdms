using System.Collections.Concurrent;
using System.Collections.Generic;
using FakeEdms.Generators;

namespace FakeEdms.Helpers
{
    internal class RegNumberGeneratorsPool
    {
        private readonly ConcurrentDictionary<IEnumerable<string>, RegistrationNumberGenerator> _generators;

        private readonly RegistrationNumberGenerator _defaultRegistrationNumberGenerator;

        private RegNumberGeneratorsPool()
        {
            _generators = new ConcurrentDictionary<IEnumerable<string>, RegistrationNumberGenerator>();
            _defaultRegistrationNumberGenerator = new RegistrationNumberGenerator();
        }

        public static RegNumberGeneratorsPool Instance { get; } = new RegNumberGeneratorsPool();

        public RegistrationNumberGenerator GetGenerator(IEnumerable<string> patterns)
        {
            return _generators.GetOrAdd(patterns, p => new RegistrationNumberGenerator(p));
        }
        
        public RegistrationNumberGenerator GetGenerator()
        {
            return _defaultRegistrationNumberGenerator;
        }
    }
}