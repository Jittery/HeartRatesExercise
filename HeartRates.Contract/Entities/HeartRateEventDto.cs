namespace HeartRates.Contract.Entities;

public sealed class HeartRateEventDto
{
    public required DateTime Timestamp { get; init; }

    public int HeartRate { get; init; }
}
