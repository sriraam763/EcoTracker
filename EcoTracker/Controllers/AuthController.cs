using System.Security.Claims;
using EcoTracker.Dtos;
using EcoTracker.Models;
using EcoTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly TokenServices _tokenServices;
    private readonly AppDbContext.AppDbContext _Db;

    public AuthController(TokenServices token, AppDbContext.AppDbContext App)
    {
        _tokenServices = token;
        _Db = App;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var userifexsist =await _Db.Users.AnyAsync(u => u.Email == request.Email);

        if (userifexsist)
        {
            return BadRequest(new { message = "user already exsist" });
        }
        
        var passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new Users
        {
            Email = request.Email,
            PassWordHash = passwordhash,
            FirstName = request.FirstName,
            LastName = request.LastName,
            HomeLocaiton = request.HomeLocation,
            OfficeLocation = request.OfficeLocation,
            GroupId = request.GroupId,
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            UpDateAt = DateTime.UtcNow
        };
        await _Db.Users.AddAsync(user);
        await _Db.SaveChangesAsync();
        return Ok(new { message = "registered" });
    }

    [HttpPost("Update")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody]UpdateProfileDto request)
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u =>
            u.Email == (User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)).Value);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.OfficeLocation = request.OfficeLocation;
        user.GroupId = request.GroupId;

        await _Db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _Db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null)
        {
            return BadRequest(new { message = "Wrong Credentials" });
        }
        
        var passwordiscorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PassWordHash);
        
        if (!passwordiscorrect)
        {
            return BadRequest(new { message = "enter the right credentials" });
        }

        var generatetoken = _tokenServices.GenerateToken(request.Email, user.Role);

        var refreshtoken = _tokenServices.GenerateRefresh();

        var refreshexpriy = _tokenServices.GetRefreshTokenExpiry();

        user.RefreshToken = refreshtoken;
        user.RefreshToeknExpiry = refreshexpriy;
        await _Db.SaveChangesAsync();

        return Ok(new AuthResponseDto
        {
            Token = generatetoken,
            Email = user.Email,
            Role = user.Role,
            RefreshToken = user.RefreshToken,
            RefreshExpiry = user.RefreshToeknExpiry
        });
    }

    [HttpPost("Refresh")]
    [Authorize]
    public async Task<IActionResult> Refresh([FromBody]RefreshRequest request)
    {
        var userdetail = await _Db.Users.FirstOrDefaultAsync(u =>
            u.RefreshToken == request.RefreshRequests);
        
        if (userdetail == null)
        {
            return Unauthorized(new { message = "sorry invalid token" });
        }
        if (userdetail.RefreshToeknExpiry < DateTime.UtcNow)
        {
            return Unauthorized(new { message = "token time expired" });
        }
        
        var newtoken = _tokenServices.GenerateToken(userdetail.Email, userdetail.Role);

        var refresh = _tokenServices.GenerateRefresh();
        var refreshtime = _tokenServices.GetRefreshTokenExpiry();

        userdetail.RefreshToken = refresh;
        userdetail.RefreshToeknExpiry = refreshtime;
        await _Db.SaveChangesAsync();
        
        return Ok(new AuthResponseDto
        {
            Token = newtoken,
            Email = userdetail.Email,
            Role = userdetail.Role,
            RefreshToken = userdetail.RefreshToken,
            RefreshExpiry = userdetail.RefreshToeknExpiry
        });
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> logout([FromBody] RefreshRequest request)
    {
        var userdetail = await _Db.Users.FirstOrDefaultAsync(u =>
        
            u.RefreshToken == request.RefreshRequests
        );

        if (userdetail == null)
        {
            return Unauthorized(new { message = "wrong token" });
        }

        userdetail.RefreshToken = null;
        userdetail.RefreshToeknExpiry = null;

        await _Db.SaveChangesAsync();

        return Ok(new { message = "logout success" });
    }
    
    [HttpPost("Add Admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequestDto request)
    {
        var userifexsist =await _Db.Users.AnyAsync(u => u.Email == request.Email);

        if (userifexsist)
        {
            return BadRequest(new { message = "user already exsist" });
        }
        
        var passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new Users
        {
            Email = request.Email,
            PassWordHash = passwordhash,
            FirstName = request.FirstName,
            LastName = request.LastName,
            HomeLocaiton = request.HomeLocation,
            OfficeLocation = request.OfficeLocation,
            GroupId = request.GroupId,
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            UpDateAt = DateTime.UtcNow
        };
        await _Db.Users.AddAsync(user);
        await _Db.SaveChangesAsync();
        return Ok(new { message = "registered" });
    }
}