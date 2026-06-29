using HeartRates.Contract.Responses;

namespace HeartRates.Business;

public interface IHeartRateService
{
    Task<HighHeartRateEventResponse> GetHighHeartRateEvents(string patientId);
    Task<HeartRateAnalyticsResponse> GetHeartRateAnalytics(string patientId, DateTime from, DateTime? to = null);
}