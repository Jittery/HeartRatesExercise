using System.Collections.Concurrent;
using System.Text.Json;

namespace HeartRates.Data;

/// <summary>
/// Basic dummy container simulating a database. It defines no indexes or assisting transactions/data-structures.
/// Concurrency and locking mechanism is not the focal point for this POC.
/// </summary>
public sealed class DbContextDataset
{
    public List<Patient> Patients { get; init; } = []; //raw
    public List<HeartRateReading> HeartRateReadings { get; init; } = []; //raw
    
    //semi-ingested data, with patient ID as key
    public Dictionary<string, Patient> IndexedPatients { get; set; } = [];
    public Dictionary<string, List<HeartRateReading>> IndexedHeartRateReadings { get; set; } = [];
    public ConcurrentDictionary<string, AccessControlTelemetry> AccessControlTelemetry { get; init; } = [];


    public static DbContextDataset Load()
    {
        const string dummyFilename = "patients.json";
        var jsonString  = File.ReadAllText(dummyFilename);
        
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var dataset = JsonSerializer.Deserialize<DbContextDataset>(jsonString, options);

        if (dataset is null)
            throw new NullReferenceException("Could not load dataset");

        dataset.IndexedPatients = dataset.Patients.ToDictionary(k => k.Id, v => v);
        dataset.IndexedHeartRateReadings = dataset.HeartRateReadings
            .GroupBy(x => x.PatientId)
            .ToDictionary(k => k.Key, v => v.ToList());
        
        return dataset;
    }
}
