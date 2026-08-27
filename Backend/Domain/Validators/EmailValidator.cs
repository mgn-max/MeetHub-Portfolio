using System.Net.Mail;

namespace MeetHub.Domain.Validators
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
