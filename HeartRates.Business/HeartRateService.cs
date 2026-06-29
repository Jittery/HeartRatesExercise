using HeartRates.Business.Actions;
using HeartRates.Contract.Entities;
using HeartRates.Contract.Responses;

namespace HeartRates.Business;

public class HeartRateService(
    IHeartRateRepository dbAccessor,
    ITrackAccessAction trackingAction) : IHeartRateService
{
    public async Task<HeartRateAnalyticsResponse> GetHeartRateAnalytics(string patientId, DateTime from, DateTime? to = null)
    {
        var patient = await dbAccessor.GetPatient(patientId);
        if (patient == null)
            return null;
        
        var readings = await dbAccessor.GetReadings(patientId, from, to ?? DateTime.UtcNow);
        //TODO typically, these kind of statistics can be easily done through the db query itself (i.e. db server side) and yield faster results

        await trackingAction.ExecuteAsync(patientId);
        
        return new HeartRateAnalyticsResponse
        {
            PatientId = patient.Id,
            Average = readings.Average(x => x.HeartRate),
            Minimum = readings.Min(x => x.HeartRate),
            Maximum = readings.Max(x => x.HeartRate),
        };
    }

    public async Task<HighHeartRateEventResponse> GetHighHeartRateEvents(string patientId)
    {
        var patient = await dbAccessor.GetPatient(patientId);
        if (patient == null)
            return null;
        
        var readings = await dbAccessor.GetHighReadings(patientId);
        
        await trackingAction.ExecuteAsync(patientId);
        return new HighHeartRateEventResponse
        {
            PatientId = patient.Id,
            PatientName = patient.Name,
            Events = readings
                .Select(x => new HeartRateEventDto
                {
                    Timestamp = x.Timestamp,
                    HeartRate = x.HeartRate
                })
        };
    }
}