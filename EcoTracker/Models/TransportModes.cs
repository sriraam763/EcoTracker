namespace EcoTracker.Models;

public class TransportModes
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal  CarbonSavedPerMile { get; set; }
    public string? Icon { get; set; }
}