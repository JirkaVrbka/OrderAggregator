namespace OrderAggregator;

/// <summary>
/// Wrapper around environment variables to handle available values, loading and casting
/// </summary>
public static class EnvironmentVariable
{
    public static readonly int SecondsToSend = GetValueFrom<int>("SECONDS_TO_SEND_ORDERS");

    private static T GetValueFrom<T>(string name)
    {
        var stringVal = Environment.GetEnvironmentVariable(name);

        try
        {
            return (T) Convert.ChangeType(stringVal, typeof(T))!;
        }
        catch (Exception)
        {
            throw new MissingFieldException($"Missing environment variable '{name}'");
        }
    }
}