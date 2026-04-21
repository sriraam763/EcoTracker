namespace EcoTracker.Dtos;

public class UserProfileDto

{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    public string HomeLocation { get; set; }
    public string OfficeLocation { get; set; }
    public Guid? GroupId { get; set; }
    public string? GroupName { get; set; }
    public decimal TotalCarbonSavedLbs { get; set; }
    public int TotalCommutes { get; set; }
    public List<UserBadgeDto> Badges { get; set; }
    public DateTime CreatedAt {  get; set; }



}
