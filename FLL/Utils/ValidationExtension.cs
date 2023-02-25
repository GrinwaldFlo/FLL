using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace FLL.Utils
{
    public static class ValidationExtension
    {
        public static bool LengthValidation(this string value, int min, int max)
        {
            return value.Length >= min && value.Length <= max;   
        }
        public static string? LengthValidationStr(this string value, int min, int max)
        {
            if (value.LengthValidation(min, max))
                return null;
            return $"Length must be between {min} and {max} characters";
        }


        public const string RegexEmailValidation = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public static bool IsValidEmail(string email)
        {
            string pattern = RegexEmailValidation;

            return Regex.IsMatch(email, pattern);
        }
    }
}
