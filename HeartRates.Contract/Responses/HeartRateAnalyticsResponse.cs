namespace HeartRates.Contract.Responses;

public sealed class HeartRateAnalyticsResponse
{
    public string PatientId { get; set; }
    
    public double Average { get; init; }

    public int Minimum { get; init; }

    public int Maximum { get; init; }
}
