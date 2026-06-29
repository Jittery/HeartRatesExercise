namespace HeartRates.Data;

public sealed class HeartRateReading
{
    public required string PatientId { get; init; }

    public required DateTime Timestamp { get; init; }

    public int HeartRate { get; init; }
}
