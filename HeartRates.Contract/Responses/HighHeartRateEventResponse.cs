using HeartRates.Contract.Entities;

namespace HeartRates.Contract.Responses;

public sealed class HighHeartRateEventResponse
{
    public required string PatientId { get; init; }

    public required string PatientName { get; init; }

    public IEnumerable<HeartRateEventDto> Events { get; init; }
}
