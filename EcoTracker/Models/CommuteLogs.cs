namespace EcoTracker.Models;

public class CommuteLogs
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TransportationId { get; set; }
    public decimal DistanceMiles { get; set; }
    public decimal CarbonSaved { get; set; }
    public DateOnly DateONly { get; set; }
    public DateTime CreatedAt { get; set; }
}