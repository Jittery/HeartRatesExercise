namespace HeartRates.Business;

public interface IRequestTrackingService
{
    void Increment(string patientId);

    int GetCount(string patientId);
}