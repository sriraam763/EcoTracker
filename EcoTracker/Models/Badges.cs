namespace EcoTracker.Models;

public class Badges
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? IconUrl { get; set; }
    public decimal ThreshHoldLbs { get; set; }
}