namespace Mailer.Common.Models
{
    public class EmailRecipient
    {
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }

        public EmailRecipient(string emailAddress, string displayName)
        {
            EmailAddress = emailAddress;
            DisplayName = displayName;
        }
    }
}
