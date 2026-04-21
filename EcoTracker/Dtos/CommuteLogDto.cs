namespace EcoTracker.Dtos;

public class CommuteLogDto
{
    public Guid Id { get; set; }
    public string TransprtModeName { get; set; }
    public string? TransportModeIcon { get; set; }
    public decimal DistanceMiles { get; set; }
    public decimal CarbonSavedLbs { get; set; }
    public DateOnly CommuteDate { get; set; }
    public bool IsMocked { get; set; }
    public DateTime CreatedAt { get; set; }

}