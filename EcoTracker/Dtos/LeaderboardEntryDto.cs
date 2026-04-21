namespace EcoTracker.Dtos;

public class LeaderboardEntryDto
{
    public int Rank { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string? GroupName { get; set; }    
    public decimal CarbonSavedLbs { get; set; }
}