namespace EcoTracker.Dtos;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MemberCount { get; set; }
    public decimal TotalCarbonSavedLbs { get; set; }

}