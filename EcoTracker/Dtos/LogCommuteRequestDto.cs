namespace EcoTracker.Dtos;

public class LogCommuteRequestDto
{
    public string TransportMode { get; set; }
    public decimal DistanceMiles { get; set; }
    public DateOnly CommuteDate { get; set; }
    
    public bool IsMocked { get; set; }

}