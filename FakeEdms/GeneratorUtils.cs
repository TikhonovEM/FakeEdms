using System.Collections.Generic;
using FakeEdms.Generators;
using FakeEdms.Helpers;

namespace FakeEdms
{
    public static class GeneratorUtils
    {
        public static string AsRegistrationNumber()
        {
            return RegNumberGeneratorsPool.Instance.GetGenerator().Generate();
        }
        
        public static string AsRegistrationNumber(params string[] numberPatterns)
        {
            return RegNumberGeneratorsPool.Instance.GetGenerator(numberPatterns).Generate();
        }
    }
}