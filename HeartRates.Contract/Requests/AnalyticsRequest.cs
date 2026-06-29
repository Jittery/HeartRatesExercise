namespace HeartRates.Contract.Requests;

public sealed class AnalyticsRequest
{
    public required DateTime From { get; init; }

    public required DateTime To { get; init; }
}
