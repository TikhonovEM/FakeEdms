using System.Text.RegularExpressions;

namespace FakeEdms.Helpers
{
    internal static class RegexUtils
    {
        private const string RegistrationNumberPropertyNameRegex = @"\w*(Document|Reg|Register)\w*Number\w*";
        private const string SubjectPropertyNameRegex = @"\w*([Ss]ubject|[Aa]nnotation)\w*";
        private const string EmailPropertyNameRegex = @"\w*[Ee]mail\w*";
        private const string AddressPropertyNameRegex = @"\w*[Aa]ddress\w*";
        
        
        public static bool IsMatch(string input, string regex) => Regex.IsMatch(input, regex);
        public static bool IsRegistrationNumber(string propertyName) => IsMatch(propertyName, RegistrationNumberPropertyNameRegex);
        public static bool IsSubject(string propertyName) => IsMatch(propertyName, SubjectPropertyNameRegex);
        public static bool IsEmail(string propertyName) => IsMatch(propertyName, EmailPropertyNameRegex);
        public static bool IsAddress(string propertyName) => IsMatch(propertyName, AddressPropertyNameRegex);
    }
}