namespace HeartRates.Data;

/// <summary>
/// Keeps track of access requests to patients' PI/PII.
/// Potentially, instead of counting "TotalAccessRequests", you'd want to insert a new entry specifying what kind of
/// usage was requested, the time and by whom.
/// If we never want to expand this feature, we should move "TotalAccessRequests" to reside with the patient's info.
/// </summary>
public class AccessControlTelemetry
{
    public required string Id { get; set; }
    
    public long TotalAccessRequests { get; set; }
}