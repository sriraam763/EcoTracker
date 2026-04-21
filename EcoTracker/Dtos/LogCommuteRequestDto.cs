namespace EcoTracker.Dtos;

public class LogCommuteRequestDto
{
    public int TransportModeId { get; set; }
    public decimal DistanceMiles { get; set; }
    public DateOnly CommuteDate { get; set; }
    public bool IsMocked { get; set; }

}