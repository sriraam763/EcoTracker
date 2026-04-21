namespace EcoTracker.Dtos;

public class UserBadgeDto
{
    public int BadgeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public decimal ThresholdLbs {  get; set; }
    public DateTime AwardedAt {  get; set; }

}