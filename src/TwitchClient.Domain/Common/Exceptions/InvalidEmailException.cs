namespace TwitchClient.Domain.Common.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message)
    : base(message)
    {
    }
}