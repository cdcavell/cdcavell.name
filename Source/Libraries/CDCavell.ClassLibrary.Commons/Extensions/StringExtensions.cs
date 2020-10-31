using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    /// <summary>
    /// Extension methods for existing string types.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/11/2020 | Initial build |~ 
    /// </revision>
    public static class StringExtensions
    {
        /// <summary>
        /// Method to determine if string is a valid email address
        /// </summary>
        /// <param name="value">this string</param>
        /// <returns>bool</returns>
        /// <method>IsValidEmail(this string value)</method>
        public static bool IsValidEmail(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            try
            {
                value = Regex.Replace(value, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[2].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(value,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Method to remove "Carriage Return" and "Line Feed" as well as Html filtering to provide proper neutralization.
        /// </summary>
        /// <param name="value">this string</param>
        /// <returns>string</returns>
        /// <method>Clean(this string value)</method>
        public static string Clean(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            string cleanString = HttpUtility.HtmlEncode(value.Replace("\r", string.Empty).Replace("\n", string.Empty));
            return string.Format("{0}", cleanString);
        }

        /// <summary>
        /// Method to determine if string is a valid Guid
        /// </summary>
        /// <param name="value">this string</param>
        /// <returns>bool</returns>
        /// <method>IsValidGuid(this string value)</method>
        public static bool IsValidGuid(this string value)
        {
            Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            bool isValid = false;

            if (!string.IsNullOrEmpty(value))
                if (isGuid.IsMatch(value))
                    isValid = true;

            return isValid;
        }

        /// <summary>
        /// Strip escape slash and beginning/ending quotes from Json result string
        /// </summary>
        /// <param name="value">this string</param>
        /// <returns>string</returns>
        /// <method>CleanJsonResult(this string result)</method>
        public static string CleanJsonResult(this string value)
        {
            return value.Replace("\\", string.Empty).Trim(new char[1] { '"' });
        }
    }
}
