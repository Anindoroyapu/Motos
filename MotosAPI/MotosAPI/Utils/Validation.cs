using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MotosAPI.Utils
{
    public class Validation
    {
        public static StatusObj IsValidDateFormat(string? date, string title = "Date")
        {
            if (string.IsNullOrEmpty(date) || date.Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            // Define a regular expression pattern to match the YYYY-MM-DD format
            string pattern = @"^\d{4}-\d{2}-\d{2}$";
            var st = Regex.IsMatch(date, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = "Please enter valid " + title };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidEmailFormat(string? email, string title, string postText = "")
        {
            if (email == null || email.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = $"Please enter {title}. {postText}" };
            }

            if (email.Length < 6)
            {
                return new StatusObj { Status = 1, Title = $"{title} should be minimum 3 characters long. {postText}" };
            }

            if (email.Length > 200)
            {
                return new StatusObj { Status = 1, Title = $" {title} should be maximum 200 characters long. {postText}" };
            }

            // Define a regular expression pattern to match the email format
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            var st = Regex.IsMatch(email, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = $"Please enter valid {title}. {postText}" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidEmailAll(List<string> email, string title)
        {
            var validateAll = email.Select(x => IsValidEmailFormat(x, title, $"({x})")).ToList();

            return ValidateAll(validateAll);
        }

        public static StatusObj IsValidPhoneNumberFormat(string? phoneNumber, string title = "PrimaryPhone Number")
        {
            if (phoneNumber == null || phoneNumber.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (phoneNumber.Length < 9)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum 9 characters long" };
            }

            if (phoneNumber.Length > 14)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum 14 characters long" };
            }

            //--Define a regular expression pattern to match the phone number format allow + sign
            string pattern = @"^(\+[0-9]{1,3})?[0-9]{9,14}$";
            var st = Regex.IsMatch(phoneNumber, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = title + " should be numeric" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidPasswordFormat(string? password, string title = "Password1")
        {
            if (password == null || password.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (password.Length < 8)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum 8 characters long" };
            }

            if (password.Length > 32)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum 32 characters long" };
            }

            //--Define a regular expression pattern to match the password format
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$";
            var st = Regex.IsMatch(password, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = title + " should contain at least one uppercase letter, one lowercase letter, one digit and one special character" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidGeneralString(string? name, string title, int min = 2, int max = 100)
        {
            if (name == null || name.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (name.Length < min)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum " + min + " character long" };
            }

            if (name.Length > max)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum " + max + " character long" };
            }

            //--Define a regular expression pattern to match the name format win a min and max length
            string pattern = @"^[a-zA-Z0-9\s\.\- ,\(\)]+$";
            var st = Regex.IsMatch(name, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = title + " should be alphanumeric" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidGeneralLongString(string? name, string title, int min = 2, int max = 5000, bool isOptional = false)
        {
            if (isOptional && (name == null || name.Trim().Length == 0))
            {
                return new StatusObj { Status = 0, Title = "Success" };
            }

            if (name == null || name.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (name.Length < min)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum " + min + " character long" };
            }

            if (name.Length > max)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum " + max + " character long" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidateOptions(string? option, List<string> validOptions, string title, string postText = "")
        {
            if (option == null || option.Length == 0)
            {
                return new StatusObj { Status = 1, Title = $"Please select {title}. {postText}" };
            }

            if (!validOptions.Contains(option))
            {
                return new StatusObj { Status = 1, Title = $"{title} is not valid. {postText}" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidateOptionsAll(List<string> options, List<string> validOptions, string title)
        {
            var validateAll = options.Select(x => IsValidateOptions(x, validOptions, title, $"({x})")).ToList();

            return ValidateAll(validateAll);
        }

        public static StatusObj IsValidDomainFormat(string domainName, string title, bool isOptional = false)
        {
            if (isOptional && (domainName == null || domainName.Length == 0))
            {
                return new StatusObj { Status = 0, Title = "Success" };
            }

            //--Verify Valid Domain Format
            if (domainName == null || domainName.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            //--Define a regular expression pattern to match the domain format
            string pattern = @"^([a-zA-Z0-9][a-zA-Z0-9\-]{0,61}[a-zA-Z0-9]\.)+[a-zA-Z]{2,}$";
            var st = Regex.IsMatch(domainName, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = title + " is not valid" };
            }

            return new StatusObj { Status = 0, Title = title + " is Valid" };
        }

        public static StatusObj IsEqual(string? val1, string? val2, string failedMessage)
        {
            if (val1 != null && val1.Length != 0 && val1 != val2)
            {
                return new StatusObj { Status = 1, Title = failedMessage };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsTrue(bool val, string failedMessage)
        {
            if (!val)
            {
                return new StatusObj { Status = 1, Title = failedMessage };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsFalse(string? val1, string? val2, string successMessage)
        {
            if (val1 != null && val1.Length != 0 && val1 == val2)
            {
                return new StatusObj { Status = 1, Title = successMessage };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidUrlFormat(string? logoUrl, string title)
        {
            //--Check is valid URL format
            if (logoUrl == null || logoUrl.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            string pattern = @"^((http|https|ftp):\/\/)?([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?$";
            var st = Regex.IsMatch(logoUrl, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = title + " is not valid" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidUserNameFormat(string userName, string title)
        {
            //--Check is Username Format is valid
            if (userName == null || userName.Trim().Length == 0)
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (userName.Length < 3)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum 3 characters long" };
            }

            if (userName.Length > 32)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum 32 characters long" };
            }

            //--Define a regular expression pattern to match the username format
            string pattern = @"^[a-zA-Z0-9\.\-\@]+$";
            var st = Regex.IsMatch(userName, pattern);
            if (!st)
            {
                return new StatusObj { Status = 1, Title = title + " should be alphanumeric or Dot(.), Hyphen(-), At the rate(@)" };
            }

            //--Check if username not starts with a letter
            if (!char.IsLetter(userName[0]))
            {
                return new StatusObj { Status = 1, Title = title + " should start with a letter" };
            }

            //--Check if username not ends with a letter or number
            if (!char.IsLetterOrDigit(userName[^1]))
            {
                return new StatusObj { Status = 1, Title = title + " should end with a letter or number" };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidateNumber(int number, string title, int? min = null, int? max = null)
        {
            if (double.IsNaN(number))
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (min != null && number < min)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum " + min };
            }

            if (max != null && number > max)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum " + max };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj IsValidateNumber(double number, string title, int? min = null, int? max = null)
        {
            if (double.IsNaN(number))
            {
                return new StatusObj { Status = 1, Title = "Please enter " + title };
            }

            if (min != null && number < min)
            {
                return new StatusObj { Status = 1, Title = title + " should be minimum " + min };
            }

            if (max != null && number > max)
            {
                return new StatusObj { Status = 1, Title = title + " should be maximum " + max };
            }

            return new StatusObj { Status = 0, Title = "Success" };
        }

        public static StatusObj ValidateAll(List<StatusObj> parameters)
        {
            var x = 0;
            foreach (var parameter in parameters)
            {
                if (parameter.Status != 0)
                {
                    return new StatusObj { Status = parameter.Status, Title = parameter.Title, ErrorIndex = x };
                }
                x++;
            }

            return new StatusObj { Status = 0, Title = "Success", ErrorIndex = -1 };
        }
    }
}
