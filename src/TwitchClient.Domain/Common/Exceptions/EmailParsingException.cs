namespace TwitchClient.Domain.Common.Exceptions;

public class EmailParsingException : Exception
{
    private const string messageOpener = "Unable to parse email:";
    public EmailParsingException(string email, string message)
    : base($"{messageOpener} {email}. {message}")
    {
        base.Data.Add("AttemptedEmail", email);
    }

    public EmailParsingException() : base()
    {
    }

    public EmailParsingException(string? message) : base(message)
    {
    }

    public EmailParsingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}