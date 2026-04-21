namespace EcoTracker.Dtos;

public class DashboardSummaryDto
{
    public decimal TotalCarbonSavedLbs { get; set; }
    public int TotalCommutes {get; set; }
    public decimal CurrentMonthLbs { get; set; }
    public int streak {  get; set; }

    public List<UserBadgeDto>? Badges{  get; set; }
    public List<CommuteLogDto> RecentLogs {  get; set; }
    public WeatherSuggestionDto WeatherSuggestion { get; set; }

}