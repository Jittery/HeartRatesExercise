namespace HeartRates.Business;

public partial interface IHeartRateRepository
{
    Task IncrementAccessTelemetry(string patientId);
}
