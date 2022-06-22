using TwitchClient.Domain.Common.Exceptions;
namespace TwitchClient.Domain.Common;

public struct Email
{
    public static Email Parse(string emailAddress)
    {
        var emailParts = emailAddress.Trim().Split('@');

        if (emailParts.Length != 2)
        {
            throw new EmailParsingException(emailAddress, "Email did not contain an '@domain' part.");
        }

        if(!emailParts[1].Contains('.'))
        {
            throw new EmailParsingException(emailAddress, "Email domain did not contain an extension part.");
        }

        return new Email(emailParts[0], emailParts[1]);
    }

    public string Username { get; set; }
    public string Domain { get; set; }

    public Email(string username, string domain)
    {
        ValidateDomain(domain);
        Username = username;
        Domain = domain;
    }

    public override string ToString()
    {
        return $"{Username}@{Domain}";
    }

    private static void ValidateDomain(string domain)
    {
        var lastChar = domain.Last();
        if(char.IsPunctuation(lastChar))
        {
            throw new InvalidEmailException($"The domain: {domain} ends with invalid character {lastChar}.");
        }
    }
}
