namespace EcoTracker.Dtos;

public class LeaderboardDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public LeaderboardEntryDto[] Entries { get; set; }
    public DateTime LastUpdated { get; set; } 
}