namespace HeartRates.Business.Actions;

public interface ITrackAccessAction
{
    public Task ExecuteAsync(string patientId);
}

public class TrackAccessAction(IHeartRateRepository dbAccessor) : ITrackAccessAction
{
    public Task ExecuteAsync(string patientId)
    {
        return dbAccessor.IncrementAccessTelemetry(patientId);
    }
}