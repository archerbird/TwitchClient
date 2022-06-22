namespace TwitchClient.Infrastructure.Common;

public class Response<TRepresentation>
{
    public IList<TRepresentation> Data { get; set; } = new List<TRepresentation>();
}