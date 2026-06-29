namespace HeartRates.Data;

public class Patient
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public int Age { get; set; }

    public required string Gender { get; set; }
    
    //We may add more meta-information here, such as TimeZone, Status (active, blocked) ...
    //e.g. public DateTime CreatedAt { get; set; }
}
