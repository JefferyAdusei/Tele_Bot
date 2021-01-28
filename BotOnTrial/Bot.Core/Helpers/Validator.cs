using System.Text.RegularExpressions;
using Bot.Core.Constants;

namespace Bot.Core.Helpers
{
    public static class Validator
    {
        /// <summary>
        /// Validates whether the text is a valid phone number
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, Helper.PhoneRegex);
        }

        public static bool IsDate(this string date)
        {
            return Regex.IsMatch(date, Helper.DateRegex);
        }
    }
}