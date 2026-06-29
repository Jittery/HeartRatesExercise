using HeartRates.Data;

namespace HeartRates.Business;

public partial interface IHeartRateRepository
{
    ValueTask<Patient> GetPatient(string patientId);

    IReadOnlyCollection<Patient> GetPatients();

    ValueTask<List<HeartRateReading>> GetReadings(string patientId, DateTime from, DateTime to);
    
    ValueTask<List<HeartRateReading>> GetHighReadings(string patientId);
    
    IReadOnlyCollection<HeartRateReading> GetHighReadings();
}
