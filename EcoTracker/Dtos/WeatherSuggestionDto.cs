namespace EcoTracker.Dtos;

public class WeatherSuggestionDto
{
    public double Temperature { get; set; }
    public string Condition { get; set; }
    public bool IsBikingSuggested { get; set; }
    public bool IsWalkingSuggested { get; set; }
    public string Reasoning { get; set; }
    
}