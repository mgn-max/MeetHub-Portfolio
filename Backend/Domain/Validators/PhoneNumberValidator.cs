using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Domain.Validators
{
    public class PhoneNumberValidator
    {
        public static bool IsValidPhoneNumber(string? phoneNumber)
        {
            var digits = new string(phoneNumber?.Where(char.IsDigit).ToArray());

            if (digits.Length != 10 && digits.Length != 11)
                return false;

            return true;
        }
    }
}
