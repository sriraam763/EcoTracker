namespace EcoTracker.Models;

public class Users
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PassWordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string HomeLocaiton { get; set; }
    public string OfficeLocation { get; set; }
    public Guid? GroupId { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpDateAt { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshToeknExpiry { get; set; }
    
    public decimal? TotalCarbonSaved { get; set; }
}