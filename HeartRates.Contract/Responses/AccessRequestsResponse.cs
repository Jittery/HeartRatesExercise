namespace HeartRates.Contract.Responses;

public sealed class AccessRequestsResponse
{
    public required string PatientId { get; init; }

    public int RequestsCount { get; init; }
}
