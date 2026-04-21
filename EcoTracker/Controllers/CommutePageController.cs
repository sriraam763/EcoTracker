using System.Security.Claims;
using System.Text.RegularExpressions;
using EcoTracker.Dtos;
using EcoTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommutePageController : Controller
{
    private readonly AppDbContext.AppDbContext _Db;

    public CommutePageController(AppDbContext.AppDbContext App)
    {
        _Db = App;
    }

    [HttpGet("Summary")]
    [Authorize]
    public async Task<IActionResult> summary()
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u =>
            u.Email == (User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)).Value);

        var commutes = _Db.CommuteLogs.Where(u => u.UserId == user.Id).OrderByDescending(u => u.DateONly).ToList();

        var currentmonth = 0.0m;

        var result = new DashboardSummaryDto
        {
            TotalCarbonSavedLbs = (decimal)user.TotalCarbonSaved,
            TotalCommutes = commutes.Count,
            CurrentMonthLbs = 0,
            streak = 0,
            Badges = null,
            RecentLogs = GetHistory1(5,user.Email),
            WeatherSuggestion = null
        };

        return Ok(result);
    }
    [HttpGet("GetHistory")]
    [Authorize]
    public async Task<IActionResult> GetHistory([FromBody]int? count)
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u =>
            u.Email == (User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)).Value);

        var commutelogs =await _Db.CommuteLogs.Where(u=>u.UserId==user.Id).OrderByDescending(u=>u.DateONly).ToListAsync();

        List<CommuteLogDto> result = new List<CommuteLogDto>();

        foreach (var n in commutelogs)
        {
            var nn = await _Db.TransportModes.FirstOrDefaultAsync(u => u.Id == n.TransportationId);
            CommuteLogDto now = new CommuteLogDto
            {
                Id = n.Id,
                TransprtModeName = nn.Name,
                TransportModeIcon = null,
                DistanceMiles = n.DistanceMiles,
                CarbonSavedLbs = n.CarbonSaved,
                CommuteDate = n.DateONly,
                IsMocked = false,
                CreatedAt = n.CreatedAt
            };
            result.Add(now);
        }

        if (count != null)
        {
            result = result.GetRange(0, (int)count);
        }

        return Ok(result);
    }

    [HttpPost("AddComute")]
    [Authorize]
    public async Task<IActionResult> AddComute([FromBody] LogCommuteRequestDto request)
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u =>
            u.Email == (User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)).Value);

        var transportation = await _Db.TransportModes.FirstOrDefaultAsync(u => u.Name == request.TransportMode);

        decimal carbonsaved = request.DistanceMiles * transportation.CarbonSavedPerMile * 0.404m;

        var result = new CommuteLogs
        {
            UserId = user.Id,
            TransportationId = transportation.Id,
            DistanceMiles = request.DistanceMiles,
            CarbonSaved = carbonsaved,
            DateONly = request.CommuteDate,
            CreatedAt = DateTime.UtcNow
        };

        user.TotalCarbonSaved += carbonsaved;

        return Ok();
    }

    [HttpPost("AddGroups")]
    [Authorize(Roles = "Leader")]
    public async Task<IActionResult> AddGroup([FromBody] CreateGroupDto request)
    {
        var neww = new Groups
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _Db.Groups.AddAsync(neww);
        await _Db.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("AddGroupMembers")]
    [Authorize(Roles = "Leader")]
    public async Task<IActionResult> Addmembre([FromBody] AddUsergroupDto request)
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u =>
            u.Email == request.Email);

        var group =await  _Db.Groups.FirstOrDefaultAsync(u => u.Name == request.GroupName);

        user.GroupId = group.Id;

        await _Db.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("getgroup")]
    [Authorize]
    public async Task<IActionResult> GetGroup()
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u =>
            u.Email == (User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)).Value);

        var group = await _Db.Groups.FindAsync(user.GroupId);

        var groupmember = _Db.Users.Where(u => u.GroupId == user.GroupId).ToList();

        decimal? totalsaved = 0.0m;
        foreach (var n in groupmember)
        {
            totalsaved += n.TotalCarbonSaved;
        }

        var result = new GroupDto
        {
            Id = (Guid)user.GroupId,
            Name = group.Name,
            Description = group.Description,
            MemberCount = groupmember.Count,
            TotalCarbonSavedLbs = (decimal)totalsaved
        };
        return Ok(result);

    }
    
    public List<CommuteLogDto> GetHistory1(int count,string eamil)
    {
        var user = _Db.Users.FirstOrDefault(u => u.Email==eamil);

        var commutelogs = _Db.CommuteLogs.Where(u=>u.UserId==user.Id).OrderByDescending(u=>u.DateONly).ToList();

        List<CommuteLogDto> result = new List<CommuteLogDto>();

        foreach (var n in commutelogs)
        {
            var nn = _Db.TransportModes.FirstOrDefault(u => u.Id == n.TransportationId);
            CommuteLogDto now = new CommuteLogDto
            {
                Id = n.Id,
                TransprtModeName = nn.Name,
                TransportModeIcon = null,
                DistanceMiles = n.DistanceMiles,
                CarbonSavedLbs = n.CarbonSaved,
                CommuteDate = n.DateONly,
                IsMocked = false,
                CreatedAt = n.CreatedAt
            };
            result.Add(now);
        }

            result = result.GetRange(0, (int)count);

        return result;
    }
}