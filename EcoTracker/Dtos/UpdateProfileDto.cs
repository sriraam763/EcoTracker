namespace EcoTracker.Dtos;

public class UpdateProfileDto
{
  
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string HomeLocation { get; set; }
    public string OfficeLocation { get; set; }
      
    public Guid? GroupId { get; set; }


}