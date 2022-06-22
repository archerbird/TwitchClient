namespace TwitchClient.Domain.Common.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message)
    : base(message)
    {
    }

    public InvalidEmailException() : base()
    {
    }

    public InvalidEmailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}