using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utility.Types
{
    /// <summary>
    /// Provides methods for treating strings.
    /// </summary>
    public static class Strings
    {       
        /// <summary>
        /// Provides Regular Expressions Patterns
        /// </summary>
        public struct RegexPatterns
        {
            /// <summary>
            /// Pattern to validate an hexadecimal color.
            /// </summary>
            public const string ColorHex = "^#([0-9]|[a-fA-F]){6}$";

            /// <summary>
            /// Pattern to validate a decimal number with '.' OR ',' chars separator.
            /// </summary>
            public const string NumRealDecimales = @"^(\d|-)?(\d(|,|.)?)*\.?\d*$";

            /// <summary>
            /// Pattern to validate an alphanumeric.
            /// </summary>
            public const string Alfanumericos = @"^[0-9a-zA-Z']+$";
        }

        /// <summary>
        /// Returns random string with length defined by parameter
        /// </summary>
        /// <param name="size">string size</param>
        /// <param name="toLower">true returns the srtring lowered</param>
        /// <returns></returns>
        public static string GetRandomString(int size, bool toLower)
        {
            //Create a random number
            Random random = new Random(DateTime.Now.Millisecond);

            StringBuilder strBuilder = new StringBuilder();
            String randomString = string.Empty;
            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                strBuilder.Append(ch);
            }

            if (toLower)
                randomString = strBuilder.ToString().ToLower();
            else
                randomString = strBuilder.ToString();

            return randomString;
        }

        /// <summary>
        /// Returns a truncated text with a length set by parameter. If you want, you can concatenate an additional string. 
        /// </summary>
        /// <param name="text">Text to truncate</param>
        /// <param name="length">Length of truncate</param>
        /// <param name="additionalText">Additional text</param>
        /// <returns></returns>
        public static string Truncate(this string text, int length, string additionalText = "")
        {
            string truncateText = "";

            if (!String.IsNullOrEmpty(text))
            {
                truncateText = text;

                if (truncateText.Length > length)
                {
                    string stringCutted = truncateText.Substring(0, length);
                    int lastSpaceIndex = stringCutted.LastIndexOf(" ");

                    if (lastSpaceIndex > 0)
                        truncateText = string.Format("{0} {1}", stringCutted.Substring(0, lastSpaceIndex), additionalText);
                    else
                        truncateText = string.Format("{0} {1}", stringCutted, additionalText);
                }
            }
            return truncateText;
        }

        /// <summary>
        /// Indicates if a string is an hexadecimal color.
        /// </summary>
        /// <param name="color">The string to check</param>
        /// <returns>true if the string is an hexadecimal color; otherwise, false.</returns>
        public static bool isHexColor(string color)
        {
            return Regex.IsMatch(color, RegexPatterns.ColorHex);
        }

        /// <summary>
        /// Indicates if a string is a number. 
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true if the string is a number; otherwise, false.</returns>
        public static bool isNumber(this string value)
        {
            return Regex.IsMatch(value, RegexPatterns.NumRealDecimales);
        }

        /// <summary>
        /// Indicates if a string is an alphanumeric.  
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns></returns>
        public static bool isAlphaNumeric(this string value)
        {
            return Regex.IsMatch(value, RegexPatterns.Alfanumericos);
        }

        /// <summary>
        /// Indicates if a string contains one letter or more. 
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true if the string contains one letter or more; otherwise, false.</returns>
        public static bool ContainsAnyLetter(string value)
        {
            return Regex.IsMatch(value, @"[a-z|ç|ñ]{1,}");
        }

        /// <summary>
        /// Removes diacritical accents from a string.
        /// </summary>
        /// <param name="text">text to remove diacritics</param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Capitalize a word.
        /// </summary>
        /// <param name="word">Word to capitalize</param>
        /// <returns></returns>
        public static string CapitalizeWords(string word)
        {
            if (word == null)
                throw new ArgumentNullException("value");
            if (word.Length == 0)
                return word;

            StringBuilder sb = new StringBuilder(word.Length);
            // Upper the first char.
            sb.Append(char.ToUpper(word[0]));
            for (int i = 1; i < word.Length; i++)
            {
                // Get the current char.
                char c = word[i];

                // Upper if after a space.
                if (char.IsWhiteSpace(word[i - 1]))
                    c = char.ToUpper(c);
                else
                    c = char.ToLower(c);

                sb.Append(c);
            }
            return sb.ToString();
        }        
    }
}
