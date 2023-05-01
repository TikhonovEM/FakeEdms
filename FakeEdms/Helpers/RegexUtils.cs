using System.Text.RegularExpressions;

namespace FakeEdms.Helpers
{
    internal static class RegexUtils
    {
        private const string RegistrationNumberPropertyNameRegex = @"\w*(Document|Reg|Register)\w*Number\w*";
        private const string SubjectPropertyNameRegex = @"\w*(Subject|Annotation)\w*";
        
        
        public static bool IsMatch(string input, string regex) => Regex.IsMatch(input, regex);
        public static bool IsRegistrationNumber(string propertyName) => IsMatch(propertyName, RegistrationNumberPropertyNameRegex);
        public static bool IsSubject(string propertyName) => IsMatch(propertyName, SubjectPropertyNameRegex);
    }
}