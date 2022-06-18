namespace TwitchClient.Domain.Common.Exceptions;

public class EmailParsingException : Exception
{
    private static readonly string messageOpener = "Unable to parse email:";
    public EmailParsingException(string email, string message)
    : base($"{messageOpener} {email}. {message}")
    {
        base.Data.Add("AttemptedEmail", email);
    }
}