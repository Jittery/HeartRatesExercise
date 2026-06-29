using HeartRates.Data;

namespace HeartRates.Business;

/// <summary>
/// A repository pattern, to decouple the inner implementation of the DAL from the business logic
/// </summary>
/// <param name="dbContext"></param>
public class HeartRateRepository(DbContextDataset dbContext) : IHeartRateRepository
{
    private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

    public ValueTask<Patient> GetPatient(string patientId)
    {
        dbContext.IndexedPatients.TryGetValue(patientId, out var patient);
        return ValueTask.FromResult(patient);
    }

    public IReadOnlyCollection<Patient> GetPatients()
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<HeartRateReading>> GetReadings(string patientId, DateTime from, DateTime to)
    {
        return ValueTask.FromResult(dbContext.IndexedHeartRateReadings.TryGetValue(patientId, out var readings) 
            ? readings.Where(x => x.Timestamp  >= from && x.Timestamp < to).ToList() 
            : null);
    }

    public ValueTask<List<HeartRateReading>> GetHighReadings(string patientId)
    {
        return ValueTask.FromResult(dbContext.IndexedHeartRateReadings.TryGetValue(patientId, out var readings)
            ? readings.Where(x => x.HeartRate > BusinessConstants.HighHeartRateThreshold).ToList() 
            : null);
    }

    public IReadOnlyCollection<HeartRateReading> GetHighReadings()
    {
        throw new NotImplementedException();
    }

    public async Task IncrementAccessTelemetry(string patientId)
    {
        await semaphoreSlim.WaitAsync(CancellationToken.None); //TODO all db operations should use cancellation tokens or retry policies

        try
        {
            if (dbContext.AccessControlTelemetry.TryGetValue(patientId, out var value))
            {
                value.TotalAccessRequests += 1;
            }
            else
            {
                dbContext.AccessControlTelemetry[patientId] = new AccessControlTelemetry
                {
                    Id = patientId,
                    TotalAccessRequests = 1
                };
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
}